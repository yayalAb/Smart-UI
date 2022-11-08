
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.User.Commands.ChangePassword
{
    public record ChangePasswordCommand : IRequest<bool>
    {
        public string Email { get; init; }   
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }

    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public ChangePasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
           var response = await _identityService.ChangePassword(request.Email,request.OldPassword,request.NewPassword);
            if (!response.Succeeded)
            {
                throw new CustomBadRequestException(String.Join(" , ", response.Errors));
            }
            return true;
        }
    }
}
