

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Application.User.Commands.Logout
{
    public record LogoutCommand : IRequest<bool>
    {
        public string TokenString {get; set;}  
    }
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<LogoutCommandHandler> _logger;

        public LogoutCommandHandler(IAppDbContext context , ILogger<LogoutCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
          await _context.Blacklists.AddAsync(
            new Blacklist{
                tokenString = request.TokenString
           });
       
           await _context.SaveChangesAsync(cancellationToken);
        

            return true;
        }
    }
}
