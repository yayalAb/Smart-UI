using System.Security.Claims;
using Application.Common.Exceptions;
using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.ChangePassword;
using Application.User.Commands.CreateUser;
using Application.User.Commands.DeleteUser;
using Application.User.Commands.ForgotPassword;
using Application.User.Commands.Logout;
using Application.User.Commands.ResetPassword;
using Application.User.Commands.UpdateUserCommand;
using Application.User.Queries.GetAllUsersQuery;
using Application.User.Queries.GetUserQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class UserController : ApiControllerBase
    {
    private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);

        }
        // POST api/<UserController>
        [HttpPost]
        [Route("create")]
        [CustomAuthorizeAttribute("User","Add")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            try
            {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = "user created successfully"
            };

            return StatusCode(StatusCodes.Status201Created, responseObj);
                
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
           

        }
        // POST api/<UserController>
        [HttpPost]
        [Route("forgot-password")]
        [CustomAuthorizeAttribute("User","Update")]
        public async Task<IActionResult> ForgootPassword([FromBody] ForgotPasswordCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("reset-password")]
        [CustomAuthorizeAttribute("User","Update")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("change-password")]
        [CustomAuthorizeAttribute("User","Update")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        // POST api/<UserController>
        [HttpPost]
        [Route("logout")]
        [CustomAuthorizeAttribute("User","Update")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpPut]
        [CustomAuthorizeAttribute("User","Update")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUser command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet]
        [CustomAuthorizeAttribute("User","ReadAll")]
        public async Task<ActionResult> list([FromQuery] GetAllUsers command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("single")]
        [CustomAuthorizeAttribute("User","ReadSingle")]
        public async Task<ActionResult> single([FromQuery] GetUser command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpDelete("{id}")]
        [CustomAuthorizeAttribute("User","Delete")]
        public async Task<IActionResult> deleteUser(string id)
        {
            try
            {

                return Ok(await Mediator.Send(new DeleteUserCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
        [HttpGet("Auth")]
        public async Task<ActionResult> isAuth()
        {
          string UserId =_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email); 
         
          if(UserId?.Length>0){
             var authValue = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
              try
            {
                return Ok(authValue);
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
          }
            try
            {  
               return Ok(UserId);
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

    //     [HttpGet("Auth")]
    // //    [CustomAuthorizeAttribute("User","ReadSingle")]
    //     public async Task<ActionResult> isAuth([FromQuery] GetAllUsers command)
    //     { 
    //         string UserId =_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
    //          var authValue = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
    //           if(UserId?.Length>0){
    //                 try
    //                     {
    //                         return Ok(await Mediator.Send(command));
    //                     }
    //                     catch (GhionException ex)
    //                     {
    //                         return AppdiveResponse.Response(this, ex.Response);
    //                     }
    //            }
    //            else{
    //                 return Ok(authValue);
    //             }
          
    //        }

        

    }
}
