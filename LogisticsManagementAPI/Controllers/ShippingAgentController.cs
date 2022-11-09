using Application.LookUp.Commands.DeleteLookup;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Application.ShippingAgentModule.Commands.DeleteShippingAgent;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class ShippingAgentController : ApiControllerBase
    {
       
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
