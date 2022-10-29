

using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public  interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
    }
}
