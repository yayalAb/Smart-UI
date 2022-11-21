using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.ChangePassword;
using Application.User.Commands.CreateUser;
using Application.User.Commands.ForgotPassword;
using Application.User.Commands.ResetPassword;
using Application.User.Commands.UpdateUserCommand;
using Application.User.Queries.GetAllUsersQuery;
using Application.User.Queries.GetUserQuery;
using Application.User.Commands.DeleteUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.User.Commands.Logout;
using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;

namespace WebApi.Controllers
{
    public class UserController : ApiControllerBase
    {
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
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = "user created successfully"
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
        // POST api/<UserController>
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> ChangePassword([FromBody] LogoutCommand command)
        {
            await Mediator.Send(command);
            var responseObj = new
            {
                message = "logout successfull"
            };
            return Ok(responseObj);

        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUser command){
            try{
                return Ok(await Mediator.Send(command));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> list([FromQuery] GetAllUsers command){
            try{
                return Ok(await Mediator.Send(command));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

        [HttpGet("single")]
        public async Task<ActionResult> single([FromQuery] GetUser command){
            try{
                return Ok(await Mediator.Send(command));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteUser(string id){
            try{

                return Ok( await Mediator.Send(new DeleteUserCommand{Id = id})) ;
            }
            catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message ));
            }
          
        }
    }
}
