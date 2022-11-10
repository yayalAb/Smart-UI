using Application.ContainerModule.Commands.CreateContainer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class ContainerController : ApiControllerBase
    {
      
        // POST api/<ContainerController>
        [HttpPost]
        public async Task<IActionResult> CreateContainer([FromForm] CreateContainerCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);
        }

      
    }
}
