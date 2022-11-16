using Application.LookUp.Commands.DeleteLookup;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Application.ShippingAgentModule.Commands.DeleteShippingAgent;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using Application.ShippingAgentModule.Queries.GetShippingAgentById;
using Application.ShippingAgentModule.Queries.GetShippingAgentList;
using Application.ShippingAgentModule.Queries.GetShippingAgentPaginatedList;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class ShippingAgentController : ApiControllerBase
    {
       // GET api/<ShippingAgentController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber ,[FromQuery] int? pageSize)
        {

            if(pageNumber == 0 || pageNumber == null || pageSize == 0 || pageSize == null ){
                var query = new GetShippingAgentListQuery();
                var response = await Mediator.Send(query);
                return Ok(response);

            }
            else{
                var query = new GetShippingAgentPaginatedListQuery{
                    pageNumber = (int)pageNumber,
                    pageSize = (int)pageSize
                };
                var response = await Mediator.Send(query);
            return Ok(response);
            }

            
        }

        // GET api/<ShippingAgentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetShippingAgentByIdQuery
            {
                Id = id
            };
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        // POST api/<ShippingAgentController>
        [HttpPost]
        public async Task<IActionResult> CreateShippingAgent ([FromForm] CreateShippingAgentCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);

        }

        // PUT api/<ShippingAgentController>/
        [HttpPut()]
        public async Task<IActionResult> updateShippingAgent ( [FromForm] UpdateShippingAgentCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = $"shippingAgent with id {response} is updated successfully"
            };
            return Ok(responseObj);
        }

        // DELETE api/<ShippingAgentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteShippingAgentCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            var responseObj = new
            {
                message = "shippingAgent deleted successfully"
            };
            return Ok(responseObj);
        }
    }
}
