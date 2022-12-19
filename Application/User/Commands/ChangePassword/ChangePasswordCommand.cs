
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.User.Commands.ChangePassword
{
    public record ChangePasswordCommand : IRequest<CustomResponse>
    {
        public string Email { get; init; }
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }

    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, CustomResponse>
    {
        private readonly IIdentityService _identityService;

        public ChangePasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<CustomResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.ChangePassword(request.Email, request.OldPassword, request.NewPassword);
            if (!response.Succeeded)
            {
                throw new GhionException(CustomResponse.Failed(response.Errors));
            }
            return CustomResponse.Succeeded("password changed successfully");
        }
    }
}
