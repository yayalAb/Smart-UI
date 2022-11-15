
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
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

        public async Task<(Result, string)> createUser(string fullName, string userName, string email, string password, int groupId)
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
                UserGroupId = groupId
            };

            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
            {
                return (result.ToApplicationResult(), string.Empty);
            }
            return (Result.Success(), newUser.Id);
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
                return Result.Failure(errors);
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
                return Result.Failure(new string[] { "change password failed " });
            }
            return Result.Success();
        }

        public async Task<Result> UpdateUser(int id, string fullName, string userName, string email, int groupId) {

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) {
                return Result.Failure(new string[] { "could not find user with the given id" });
            }

            user.FullName = fullName;
            user.UserName = userName;
            user.Email = email;
            user.UserGroupId = groupId;

            var response = await _userManager.UpdateAsync(user);

            if(!response.Succeeded) {
                return Result.Failure(new string[] { "User Updating failed!" });
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
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    algorithm: SecurityAlgorithms.HmacSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }


    }
}
