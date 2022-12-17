using Application.Common.Exceptions;
using Application.GoodModule.Commands.AssignGoodsCommand;
using Application.GoodModule.Commands.UpdateGoodCommand;
using Application.GoodModule.Queries.GetAllGoodQuery;
using Application.GoodModule.Queries.GetGoodQuery;
using Application.GoodModule.Queries.GetGoodsByLocation;
using Application.GoodModule.Queries.GoodByContainer;
using Application.GoodModule.Queries.UnstafedGoodByOperation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class GoodsController : ApiControllerBase
    {

        // POST api/<GoodsController>
        [HttpPost]
        [Route("assign")]
        public async Task<IActionResult> AssignGoods([FromBody] AssignGoodsCommand command)
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

        [HttpPut]
        [Route("reassign")]
        public async Task<IActionResult> ReassignGoods([FromBody] ReassignGoodsCommand command)
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
        [Route("Bylocation")]
        public async Task<ActionResult> GoodListByLocation([FromQuery] GetGoodsByLocationQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("unstafed/{operationId}")]
        public async Task<ActionResult> Unstafed(int operationId)
        {
            try
            {
                return Ok(await Mediator.Send(new OperationUnstafedGood { OperationId = operationId, Type = true }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("contained/{operationId}")]
        public async Task<ActionResult> Contained(int operationId)
        {
            try
            {
                return Ok(await Mediator.Send(new OperationUnstafedGood { OperationId = operationId, Type = false }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GoodList([FromQuery] GetAllGoodQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("/byContainer/{containerId}")]
        public async Task<ActionResult> byContainer(int containerId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetByContainer { ContainerId = containerId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("{operationId}")]
        public async Task<ActionResult> SingleGood(int operationId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetAssignedGoodQuery { OperationId = operationId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpPut]
        public async Task<ActionResult> update([FromBody] UpdateGoodCommand command)
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
