using Application.Common.Exceptions;
using Application.GoodModule.Commands.AssignGoodsCommand;
using Application.GoodModule.Commands.DeleteGood;
using Application.GoodModule.Commands.UpdateGoodCommand;
using Application.GoodModule.Queries.GetAllGoodQuery;
using Application.GoodModule.Queries.GetGoodQuery;
using Application.GoodModule.Queries.GetGoodsByLocation;
using Application.GoodModule.Queries.GoodByContainer;
using Application.GoodModule.Queries.UnstafedGoodByOperation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class GoodsController : ApiControllerBase
    {

        // POST api/<GoodsController>
        [HttpPost]
        [Route("assign")]
        [CustomAuthorizeAttribute("assign_goods", "Add")]

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
        [CustomAuthorizeAttribute("reassign_goods", "Add")]

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
        [CustomAuthorizeAttribute("assign_goods", "ReadAll")]
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
        [CustomAuthorizeAttribute("assign_goods", "ReadAll")]

        public async Task<ActionResult> Unstafed(int operationId)
        {
            try
            {
                return Ok(await Mediator.Send(new OperationUnstafedGood { OperationId = operationId, Type = "unstaffed" }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("contained/{operationId}")]
        [CustomAuthorizeAttribute("assign_goods", "ReadAll")]

        public async Task<ActionResult> Contained(int operationId)
        {
            try
            {
                return Ok(await Mediator.Send(new OperationUnstafedGood { OperationId = operationId, Type = "contained" }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("documentSelection/{operationId}")]
        [CustomAuthorizeAttribute("assign_goods", "ReadAll")]
        public async Task<ActionResult> documentSelection(int operationId)
        {
            try
            {
                return Ok(await Mediator.Send(new OperationUnstafedGood { OperationId = operationId, Type = "document" }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet]
        [CustomAuthorizeAttribute("assign_goods", "ReadAll")]

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

        [HttpGet("byContainer/{containerId}")]
        [CustomAuthorizeAttribute("assign_goods", "ReadAll")]

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
        [CustomAuthorizeAttribute("assign_goods", "ReadSingle")]

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
        [CustomAuthorizeAttribute("assign_goods", "Update")]

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
        [HttpDelete("{id}")]
        [CustomAuthorizeAttribute("assign_goods", "Delete")]

        public async Task<ActionResult> delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteGoodCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
    }
}
