using Application.LookUp.Commands.CreateLookup;
using Application.LookUp.Commands.DeleteLookup;
using Application.LookUp.Commands.UpdateLookup;
using Application.LookUp.Query.GetByKey;
using Microsoft.AspNetCore.Mvc;
using Application.LookUp.Commands.CreateLookUpKey;
using Application.LookUp.Query.GetAllLookups;

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

        [HttpPost]
        [Route("category")]
        public async Task<IActionResult> createLookupKey(CreateLookUpKey command)
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

        [HttpGet("{type}")]
        public async Task<ActionResult> getLookup(string type){
            try{
                return Ok(await Mediator.Send(new GetLookUpByKey(type)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> getAll([FromQuery] GetAllLookups command){
            try{
                return Ok(await Mediator.Send(command));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

    }
}
