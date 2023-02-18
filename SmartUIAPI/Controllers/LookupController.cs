using Application.Common.Exceptions;
using Application.LookUp.Commands.CreateLookup;
using Application.LookUp.Commands.CreateLookUpKey;
using Application.LookUp.Commands.DeleteLookup;
using Application.LookUp.Commands.UpdateLookup;
using Application.LookUp.Query.GetAllLookups;
using Application.LookUp.Query.GetByIdQuery;
using Application.LookUp.Query.GetByKey;
using Microsoft.AspNetCore.Mvc;
using SmartUIAPI.Models;
using SmartUIAPI.Services;

namespace SmartUIAPI.Controllers
{

    public class LookupController : ApiControllerBase
    {

        // POST api/<LookupController>
        [HttpPost]
        [CustomAuthorizeAttribute("Lookup","Add")]
        public async Task<IActionResult> createLookup([FromBody] CreateLookupCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpPost]
        [Route("category")]
        [CustomAuthorizeAttribute("Lookup","Add")]
        public async Task<IActionResult> createLookupKey(CreateLookUpKey command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        // PUT api/<LookupController>/5
        [HttpPut]
        [CustomAuthorizeAttribute("Lookup","Update")]
        public async Task<IActionResult> Put([FromBody] UpdateLookupCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }


        }

        // DELETE api/<LookupController>/5
        [HttpDelete]
        [CustomAuthorizeAttribute("Lookup","Delete")]
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
        [CustomAuthorizeAttribute("Lookup","ReadSingle")]
        public async Task<ActionResult> getById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetById() { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet("{type}")]
        public async Task<ActionResult> getLookup(string type)
        {
            try
            {
                return Ok(await Mediator.Send(new GetLookUpByKey(type)));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet]
        [CustomAuthorizeAttribute("Lookup","ReadAll")]
        public async Task<ActionResult> getAll([FromQuery] GetAllLookups command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

    }
}
