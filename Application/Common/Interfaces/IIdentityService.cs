﻿

using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public  interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<(Result result, string tokenString , IApplicationUser user)> AuthenticateUser(string email, string password);
        Task<(Result result, string userId)> createUser(string fullName, string userName, string email, string password, int groupId);
        
        }
}
