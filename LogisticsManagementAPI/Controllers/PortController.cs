using Application.PortModule.Commands.CreatePort;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class PortController : ApiControllerBase
    {
      
        // POST api/<PortController>
        [HttpPost]
        public async Task<IActionResult> CreatePort([FromBody] CreatePortCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };
            return StatusCode(StatusCodes.Status201Created,responseObj);    

        }


    }
}
