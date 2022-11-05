using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.CreateUser.Commands;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

     
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                //hide sensetive data
                if (request.GetType().Equals(typeof(AuthenticateUserCommand)) || request.GetType().Equals(typeof(CreateUserCommand)))
                {
                    request = default;

                }
              
                _logger.LogError(ex, "GhionApi Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

                throw;
            }
        }
    }
    
}
