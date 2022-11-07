using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.ChangePassword;
using Application.User.Commands.CreateUser.Commands;
using Application.User.Commands.ForgotPassword;
using Application.User.Commands.ResetPassword;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        // POST api/<UserController>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command)
        {
           var response = await Mediator.Send(command);
          
          
            return Ok(response);    


        }
        // POST api/<UserController>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };

            return StatusCode(StatusCodes.Status201Created, responseObj);


        }
        // POST api/<UserController>
        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgootPassword([FromBody] ForgotPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response)
            {
                return BadRequest();
            }
            var responseObj = new
            {
                message = "successfully sent password reset link by email"
            };
            return Ok(responseObj);


        }
        // POST api/<UserController>
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
             await Mediator.Send(command);
            var responseObj = new
            {
                message = "password reset successful"
            };
            return Ok(responseObj);


        }
        // POST api/<UserController>
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            await Mediator.Send(command);
            var responseObj = new
            {
                message = "password changed successfully"
            };
            return Ok(responseObj);


        }
    }
}
