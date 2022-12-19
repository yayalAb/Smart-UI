using Application.Common.Interfaces;
using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.CreateUser;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _identityService.GetUserNameAsync(userId);
            }
            //Hide sensetive informataion 
            if (request.GetType().Equals(typeof(AuthenticateUserCommand)) || request.GetType().Equals(typeof(CreateUserCommand)))
            {
                request = default;

            }
            _logger.LogInformation("GhionApi Request: {Name} {@UserId} {@UserName} {@Request} {@Time}",
                                 requestName, userId, userName, request, DateTime.Now);



        }
    }
}
