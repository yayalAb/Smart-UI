
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<AuthorizationBehaviour<TRequest, TResponse>> _logger;
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehaviour(ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger, IAppDbContext context, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var tokenString = _currentUserService.tokenString();
            if (_context.Blacklists.Any(b => b.tokenString == tokenString))
            {
                throw new ForbiddenAccessException();
            }


            return await next();
        }
    }

}
