

using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public  interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<(Result result, string tokenString , IApplicationUser? user)> AuthenticateUser(string email, string password);
        Task<(Result result, string userId)> createUser(string fullName, string userName, string email, string password, byte state, int addressId, int groupId);
        Task<(Result result , string resetToken)> ForgotPassword(string email);
        Task<Result> ResetPassword(string email, string password, string token);
        Task<Result> ChangePassword(string email , string oldPassword ,string newPassword);
        Task<Result> UpdateUser(int id, string fullName, string userName, string email, byte status, int groupId);
    }
}
