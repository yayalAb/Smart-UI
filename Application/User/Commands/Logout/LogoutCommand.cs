

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.User.Commands.Logout
{
    public record LogoutCommand : IRequest<CustomResponse>
    {
        public string TokenString { get; set; }
    }
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<LogoutCommandHandler> _logger;

        public LogoutCommandHandler(IAppDbContext context, ILogger<LogoutCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<CustomResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(request.TokenString);
            var expireDate = token.ValidTo.AddMinutes(1);
            if(await _context.Blacklists.Where(b => b.tokenString == request.TokenString).AnyAsync()){
                return CustomResponse.Succeeded("already logged out");
            }
            await _context.Blacklists.AddAsync(
                new Blacklist
                {
                    tokenString = request.TokenString,
                    ExpireDate = expireDate
                }
            );

            await _context.SaveChangesAsync(cancellationToken);


            return CustomResponse.Succeeded("Logout Successful");
        }
    }
}
