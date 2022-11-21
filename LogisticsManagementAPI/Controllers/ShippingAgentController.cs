using Application.Common.Exceptions;
using Application.Common.Models;
using Application.LookUp.Commands.DeleteLookup;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Application.ShippingAgentModule.Commands.DeleteShippingAgent;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using Application.ShippingAgentModule.Queries.GetShippingAgentById;
using Application.ShippingAgentModule.Queries.GetShippingAgentList;
using Application.ShippingAgentModule.Queries.GetShippingAgentPaginatedList;
using Application.UserGroupModule.Queries.GetShippingAgentLookupQuery;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class ShippingAgentController : ApiControllerBase
    {
        // GET api/<ShippingAgentController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetShippingAgentPaginatedListQuery query)
        {

            return Ok(await Mediator.Send(query));
        }




        // GET api/<ShippingAgentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try{
                return Ok(await Mediator.Send(new GetShippingAgentByIdQuery {Id = id}));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

        // POST api/<ShippingAgentController>
        [HttpPost]
        public async Task<IActionResult> CreateShippingAgent([FromBody] CreateShippingAgentCommand command)
        {
            try{
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

        // PUT api/<ShippingAgentController>/
        [HttpPut()]
        public async Task<IActionResult> updateShippingAgent([FromBody] UpdateShippingAgentCommand command)
        {
            try{
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        // DELETE api/<ShippingAgentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                return Ok(await Mediator.Send(new DeleteShippingAgentCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }
        [HttpGet]
        [Route("lookup")]
        public async Task<IActionResult> LookUp()
        {

            try
            {
                return Ok(await Mediator.Send(new GetShippingAgentLookupQuery()));
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }
    }
}
