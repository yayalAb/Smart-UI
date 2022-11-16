using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.User.Commands.AuthenticateUser;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.AddressModule.Commands.AddressCreateCommand;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Commands.CreateUser
{

    public record CreateUserCommand : IRequest<string>
    {
        public string FullName { get; init; }
        public string UserName { get; init; }
        public byte State {get; init;} = 1!;
        public int GroupId { get; init; }
        public AddressDto Address {get; init;}

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IIdentityService identityService, IAppDbContext context, ILogger<CreateUserCommandHandler> logger, IMapper mapper)
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
           var executionStrategy = _context.database.CreateExecutionStrategy();
         return  await executionStrategy.ExecuteAsync(
            async ()=>{


      using (var transaction =    _context.database.BeginTransaction()){
    try
    {
         Address new_address = _mapper.Map<Address>(request.Address);
            _context.Addresses.Add(new_address);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: GENERATE USER PASS AND SEND IT BY EMAIL
            var defaultUserPassword = "pass123#@A";

            var response = await _identityService.createUser(request.FullName, request.UserName, request.Address.Email,defaultUserPassword, request.State, new_address.Id, request.GroupId);

            if (!response.result.Succeeded)
            {
                throw new CantCreateUserException(response.result.Errors.ToList());

           
            }

           await  _context.database.CommitTransactionAsync();
            return response.userId;


    }
    catch (System.Exception)
    {
        await _context.database.RollbackTransactionAsync();
        throw;
    }
          
         }


            });

      
              }
    }
}
