
using Application.ContainerModule.Commands.CreateContainer;
using Application.ContainerModule.Commands.UpdateContainer;
using Application.ContainerModule.Queries.GetAllContainersQuery;
using Application.ContainerModule.Queries.GetContainerQuery;

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


        // PUT api/<ContainerController>/
        [HttpPut]
        public async Task<IActionResult> UpdateContainer([FromForm] UpdateContainerCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = $"Container with id : {response} is updated successfully"
            };
            return Ok(responseObj);

        }

        [HttpGet]
        public async Task<ActionResult> ContainerList(){
            try{
                return Ok(await Mediator.Send(new GetAllContainers()));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetContainer(int id){
            try{
                return Ok(await Mediator.Send(new GetContainer(id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

    }
}
