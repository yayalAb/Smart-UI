using Application.Common.Exceptions;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Application.ShippingAgentModule.Commands.DeleteShippingAgent;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using Application.ShippingAgentModule.Queries.GetShippingAgentById;
using Application.ShippingAgentModule.Queries.GetShippingAgentPaginatedList;
using Application.UserGroupModule.Queries.GetShippingAgentLookupQuery;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class ShippingAgentController : ApiControllerBase
    {
        // GET api/<ShippingAgentController>/
        [HttpGet]
        [CustomAuthorizeAttribute("Shipping_agent","ReadAll")]
        public async Task<IActionResult> Get([FromQuery] GetShippingAgentPaginatedListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }




        // GET api/<ShippingAgentController>/5
        [HttpGet("{id}")]
        [CustomAuthorizeAttribute("Shipping_Agent","ReadSingle")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetShippingAgentByIdQuery { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        // POST api/<ShippingAgentController>
        [HttpPost]
        [CustomAuthorizeAttribute("Shipping_agent","Add")]
        public async Task<IActionResult> CreateShippingAgent([FromBody] CreateShippingAgentCommand command)
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

        // PUT api/<ShippingAgentController>/
        [HttpPut()]
        [CustomAuthorizeAttribute("Shipping_agent","Update")]
        public async Task<IActionResult> updateShippingAgent([FromBody] UpdateShippingAgentCommand command)
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

        // DELETE api/<ShippingAgentController>/5
        [HttpDelete("{id}")]
        [CustomAuthorizeAttribute("Shipping_agent","Delete")]
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
        }
        [HttpGet]
        [Route("lookup")]
        [CustomAuthorizeAttribute("Shipping_agent","ReadAll")]
        public async Task<IActionResult> LookUp()
        {

            try
            {
                return Ok(await Mediator.Send(new GetShippingAgentLookupQuery()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
    }
}
