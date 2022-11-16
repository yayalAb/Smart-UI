using Application.ContainerModule.Commands.UpdateContainer;
using Application.PortModule.Commands.CreatePort;
using Application.PortModule.Commands.UpdatePort;
using Microsoft.AspNetCore.Mvc;
using Application.PortModule.Queries.GetAllPortsQuery;

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
        // PUT api/<PortController>/
        [HttpPut]
        public async Task<IActionResult> UpdatePort([FromBody] UpdatePortCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = $"Port with id : {response} is updated successfully"
            };
            return Ok(responseObj);

        }

        [HttpGet]
        public async Task<ActionResult> get([FromQuery] GetAllPorts command){
            try{
                return Ok(await Mediator.Send(command));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

    }
}
