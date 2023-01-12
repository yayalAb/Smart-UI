
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<IdentityService> _logger;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, ILogger<IdentityService> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<(Result result, string tokenString, IApplicationUser? user)> AuthenticateUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);


            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {

                var tokenString = await generateToken(user);
                return (Result.Success(), tokenString, user);


            }
            string[] errors = new string[] { "Invalid login" };
            return (Result.Failure(errors), string.Empty, null);

        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);


            return user.UserName;
        }
        public int GetUserGroupId(string userId){
            return  _userManager.Users.First(u => u.Id == userId).UserGroupId;
        }
        public async Task<(Result, string)> createUser(string fullName, string userName, string email, byte state, int addressId, int groupId)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return (Result.Failure(new string[] { "user with the given email already exists" }), string.Empty);
            }
            existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return (Result.Failure(new string[] { "username is already taken" }), string.Empty);
            }
            var newUser = new ApplicationUser()
            {
                FullName = fullName,
                UserName = userName,
                Email = email,
                State = state,
                AddressId = addressId,
                UserGroupId = groupId
            };
            string password = GeneratePassword();
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
            {
                return (result.ToApplicationResult(), string.Empty);
            }
            return (Result.Success(), password);
        }

        public async Task<(Result, string)> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (Result.Failure(new string[] { "could not find user with the given email" }), string.Empty);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return (Result.Success(), token);
        }
        public async Task<Result> ResetPassword(string email, string password, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure(new string[] { "user not found" });
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                throw new Exception($"password reset failed! \n {resetPassResult.Errors}");
            }
            return Result.Success();
        }

        public async Task<Result> ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure(new string[] { "could not find user with the given email" });
            }
            var response = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!response.Succeeded)
            {
                throw new Exception($"Change password failed! \n {response.Errors}");
            }
            return Result.Success();
        }

        public async Task<Result> UpdateUser(string id, string fullName, string userName, string email, byte state, int groupId)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return Result.Failure(new string[] { "could not find user with the given id" });
            }

            user.FullName = fullName;
            user.UserName = userName;
            user.Email = email;
            user.UserGroupId = groupId;
            user.State = state;

            var response = await _userManager.UpdateAsync(user);

            if (!response.Succeeded)
            {
                throw new Exception($"User Updating failed! \n {response.Errors}");
            }

            return Result.Success();

        }
        public async Task<Result> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Result.Failure(new string[] { "could not find user with the given id" });
            }
            var response = await _userManager.DeleteAsync(user);

            if (!response.Succeeded)
            {
                throw new Exception($"User Deleting failed! \n {response.Errors}");
            }

            return Result.Success();
        }
        private async Task<string> generateToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {

               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(ClaimTypes.Email, user.Email)
           };
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

            }
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                // AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    algorithm: SecurityAlgorithms.HmacSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        private string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));


            return password.ToString();
        }
        public IQueryable<IApplicationUser> AllUsers()
        {
            return _userManager.Users;
        }

    }
}
