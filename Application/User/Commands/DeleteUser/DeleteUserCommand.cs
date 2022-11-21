
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.User.Commands.DeleteUser
{
   public record DeleteUserCommand : IRequest<CustomResponse>
    {
        public string Id { get; set; } 
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CustomResponse>
    {
        private readonly IIdentityService _identityService;

        public DeleteUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<CustomResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.DeleteUser(request.Id);
            if(!response.Succeeded){
                throw new GhionException(CustomResponse.BadRequest(String.Join(" , ", response.Errors)));
            }
            return CustomResponse.Succeeded("User deleted successfully!");
        }
    }
}