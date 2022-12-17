using Application.Common.Exceptions;
using Application.PortModule.Commands.CreatePort;
using Application.PortModule.Commands.DeletePort;
using Application.PortModule.Commands.UpdatePort;
using Application.PortModule.Queries.GetAllPortsQuery;
using Application.PortModule.Queries.GetPort;
using Application.UserGroupModule.Queries.GetPortLookupQuery;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class PortController : ApiControllerBase
    {

        // POST api/<PortController>
        [HttpPost]
        public async Task<IActionResult> CreatePort([FromBody] CreatePortCommand command)
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
        // PUT api/<PortController>/
        [HttpPut]
        public async Task<IActionResult> UpdatePort([FromBody] UpdatePortCommand command)
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

        [HttpGet]
        public async Task<ActionResult> get([FromQuery] GetAllPorts command)
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

        [HttpGet("{id}")]
        public async Task<ActionResult> getPort(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetPort { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeletePort { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
        [HttpGet]
        [Route("lookup")]
        public async Task<IActionResult> LookUp()
        {

            try
            {
                return Ok(await Mediator.Send(new GetPortLookupQuery()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
    }
}
