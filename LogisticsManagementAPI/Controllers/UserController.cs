using Application.User.Commands.AuthenticateUser;
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
        public async Task<IActionResult> Post([FromBody] AuthenticateUserCommand command)
        {
           var response = await Mediator.Send(command);
          
            return Ok(response);    


        }


    }
}
