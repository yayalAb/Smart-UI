using Application.LookUp.Commands.DeleteLookup;
using Application.OperationModule.Commands.CreateOperation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class OperationController : ApiControllerBase
    {
      

        // POST api/<OperationController>
        [HttpPost]
        public async Task<IActionResult> CreateOperation([FromForm] CreateOperationCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = $"operation created successfully ==== {response}"
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);
        }



        // DELETE api/<OperationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            var command = new DeleteLookupCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            var responseObj = new
            {
                message = "Operation deleted successfully"
            };
            return Ok(responseObj);
        }
    }
}
