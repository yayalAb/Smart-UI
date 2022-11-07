using Application.LookUp.Commands.CreateLookup;
using Application.LookUp.Commands.DeleteLookup;
using Application.LookUp.Commands.UpdateLookup;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
   
    public class LookupController : ApiControllerBase
    {
   
        // POST api/<LookupController>
        [HttpPost]
        public async Task<IActionResult> createLookup([FromBody] CreateLookupCommand command)
        {
            var response =  await Mediator.Send(command);
            var responseObj = new
            {
                Id = response,
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);
        }

        // PUT api/<LookupController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateLookupCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = $"lookup with id {response} is updated successfully"
            };
            return Ok(responseObj);
        }

        // DELETE api/<LookupController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLookupCommand
            {
                Id = id
            };
             await Mediator.Send(command);
            var responseObj = new
            {
                message = "lookup deleted successfully"
            };
            return Ok(responseObj);

        }
    }
}
