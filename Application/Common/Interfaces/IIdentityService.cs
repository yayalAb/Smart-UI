﻿

using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public  interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<(Result result, string tokenString)> AuthenticateUser(string email, string password);
    }
}
