using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.User.Commands.AuthenticateUser;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.AddressModule.Commands.AddressCreateCommand;

namespace Application.User.Commands.CreateUser
{

    public record CreateUserCommand : IRequest<string>
    {
        public string FullName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public byte State {get; init;} = 1!;
        public int GroupId { get; init; }
        public AddressCreateCommand Address {get; init;}

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
            
            _context.database.BeginTransaction();

            Address new_address = _mapper.Map<Address>(request.Address);
            _context.Addresses.Add(new_address);
            await _context.SaveChangesAsync(cancellationToken);

            var response = await _identityService.createUser(request.FullName, request.UserName, request.Email, request.Password, request.State, new_address.Id, request.GroupId);

            if (!response.result.Succeeded)
            {
                _context.database.RollbackTransaction();
                throw new CantCreateUserException(response.result.Errors.ToList());
            }

            _context.database.CommitTransaction();
            return response.userId;

        }
    }
}
