using Application.LookUp.Commands.CreateLookup;
using Application.LookUp.Commands.DeleteLookup;
using Application.LookUp.Commands.UpdateLookup;
using Application.LookUp.Query.GetByKey;
using Microsoft.AspNetCore.Mvc;
using Application.LookUp.Commands.CreateLookUpKey;
using Application.LookUp.Query.GetAllLookups;
using Application.LookUp.Query.GetByIdQuery;
using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;

namespace WebApi.Controllers
{
   
    public class LookupController : ApiControllerBase
    {
   
        // POST api/<LookupController>
        [HttpPost]
        public async Task<IActionResult> createLookup([FromBody] CreateLookupCommand command)
        {

            try{
                return Ok(await Mediator.Send(command));
            }catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpPost]
        [Route("category")]
        public async Task<IActionResult> createLookupKey(CreateLookUpKey command)
        {

            try{
                return Ok(await Mediator.Send(command));
            }catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        // PUT api/<LookupController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateLookupCommand command)
        {

            try{
                return Ok(await Mediator.Send(command));
            }catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
            
        }

        // DELETE api/<LookupController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteLookupCommand command)
        {

            await Mediator.Send(command);
            var responseObj = new
            {
                message = "lookup deleted successfully"
            };
            return Ok(responseObj);

        }

        [HttpGet("one/{id}")]
        public async Task<ActionResult> getById(int id){
            try{
                return Ok(await Mediator.Send(new GetById(){Id = id}));
            }catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

        [HttpGet("{type}")]
        public async Task<ActionResult> getLookup(string type){
            try{
                return Ok(await Mediator.Send(new GetLookUpByKey(type)));
            }catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult> getAll([FromQuery] GetAllLookups command) {
            try{
                return Ok(await Mediator.Send(command));
            }catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

    }
}
