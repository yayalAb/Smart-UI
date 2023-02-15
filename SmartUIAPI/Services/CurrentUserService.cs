using Application.Common.Interfaces;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string? tokenString()
        {
            var authValue = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
            if (AuthenticationHeaderValue.TryParse(authValue, out var headerValue))
            {
                var scheme = headerValue.Scheme;
                var parameter = headerValue.Parameter;
                return parameter?.ToString();
            }
            return null;

        }
    }
}