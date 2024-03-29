﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.User.Commands.CreateUser
{

    public record CreateUserCommand : IRequest<bool>
    {
        public string FullName { get; init; }
        public string UserName { get; init; }
        public byte State { get; init; } = 1!;
        public int UserGroupId { get; init; }
        public AddressDto Address { get; init; }

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        // private readonly IEmailService _emailService;

        public CreateUserCommandHandler(IIdentityService identityService, IAppDbContext context, ILogger<CreateUserCommandHandler> logger, IMapper mapper)
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
            // _emailService = emailService;
        }
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(
               async () =>
               {

                   using (var transaction = _context.database.BeginTransaction())
                   {
                       try
                       {
                           Address new_address = _mapper.Map<Address>(request.Address);
                           _context.Addresses.Add(new_address);
                           await _context.SaveChangesAsync(cancellationToken);

                           // GENERATE USER PASS AND SEND IT BY EMAIL

                           var response = await _identityService.createUser(request.FullName, request.UserName, request.Address.Email, request.State, new_address.Id, request.UserGroupId);

                           if (!response.result.Succeeded)
                           {
                               throw new CantCreateUserException(response.result.Errors.ToList());
                           }

                           var emailContent = $"{response.password} is your default password you can login and change it ";
                           var mailrequest = new MailRequest()
                           {
                               Subject = "Welcome",
                               Body = emailContent,
                               ToEmail = request.Address.Email,
                           };
                        //    await _emailService.SendEmailAsync(mailrequest);
                           await _context.database.CommitTransactionAsync();
                           return true;

                       }
                       catch (System.Exception)
                       {
                           await _context.database.RollbackTransactionAsync();
                           throw;
                       }

                   }

               }
            );

        }

    }

}

