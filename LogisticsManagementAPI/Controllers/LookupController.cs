using Application.LookUp.Commands.CreateLookup;
using Application.LookUp.Commands.DeleteLookup;
using Application.LookUp.Commands.UpdateLookup;
using Application.LookUp.Query.GetByKey;
using Microsoft.AspNetCore.Mvc;
using Application.LookUp.Commands.CreateLookUpKey;


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

        [HttpPost("{lookup_key}")]
        // [Route("lookup_key")]
        public async Task<IActionResult> createLookupKey(string lookup_key)
        {
            var response =  await Mediator.Send(new CreateLookUpKey(lookup_key));
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

        [HttpGet("{type}")]
        public async Task<ActionResult> getLookup(string type){
            try{
                return Ok(Mediator.Send(new GetLookUpByKey(type)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }
    }
}
