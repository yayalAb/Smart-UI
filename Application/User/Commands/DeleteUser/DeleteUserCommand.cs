
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.User.Commands.DeleteUser
{
   public record DeleteUserCommand : IRequest<bool>
    {
        public string Id { get; set; } 
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public DeleteUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.DeleteUser(request.Id);
            if(!response.Succeeded){
                throw new CustomBadRequestException(String.Join(" , ", response.Errors));
            }
            return true;
        }
    }
}