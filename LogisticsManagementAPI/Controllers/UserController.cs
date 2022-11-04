using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.CreateUser.Commands;
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

            return Ok(responseObj);


        }


    }
}
