
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.User.Commands.CreateUser.Commands {
    
    public record CreateUserCommand : IRequest<string>
    {
        public string FullName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }    
        public int GroupId { get; init; }
        public IEnumerable<AppUserRole>? UserRoles { get; init; }

    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IIdentityService identityService , IAppDbContext context , ILogger<CreateUserCommandHandler> logger)
        {
            _identityService = identityService;
            _context = context;
           _logger = logger;
        }
        public async  Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
            IEnumerable<AppUserRole> userRoles = request.UserRoles;

            using var transaction = _context.database.BeginTransaction();
            var response = await _identityService.createUser(request.FullName, request.UserName, request.Email, request.Password, request.GroupId);
           
            if (!response.result.Succeeded)
            {
                throw new CantCreateUserException(response.result.Errors.ToList());
            }

            if( userRoles == null || userRoles.ToArray().Length == 0)
            {
                userRoles = AppUserRole.createDefaultRoles(response.userId);
            }

            try
            {
                await _context.AddRangeAsync(userRoles);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
                return response.userId;

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new CantCreateUserException(new List<string> { e.Message });
            }
            
        }
    }
}
