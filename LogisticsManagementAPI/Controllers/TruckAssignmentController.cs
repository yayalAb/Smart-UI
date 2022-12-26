using Application.Common.Exceptions;
using Application.TruckAssignmentModule.Commands.CreateTruckAssignment;
using Application.TruckAssignmentModule.Commands.UpdateTruckAssignment;
using Application.TruckAssignmentModule.Queries;
using Application.TruckAssignmentModule.Queries.GetTruckAssignmentPaginatedList;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{

    public class TruckAssignmentController : ApiControllerBase
    {


        [HttpPost]
        [CustomAuthorizeAttribute("get_pass","Add")]
        public async Task<ActionResult> create([FromBody] CreateTruckAssignmentCommand command)
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
        [CustomAuthorizeAttribute("get_pass","Update")]
        public async Task<ActionResult> update([FromBody] UpdateTruckAssignmentCommand command)
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
        [Route("byOperation")]
        [CustomAuthorizeAttribute("get_pass","ReadAll")]
        public async Task<ActionResult> GetTruckAssignmentByOperationId([FromQuery] GetTruckAssignmentsByOperationIdQuery query)
        {

            try {
                return Ok(await Mediator.Send(query));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
        [HttpGet]
        [CustomAuthorizeAttribute("get_pass","ReadAll")]
        public async Task<ActionResult> GetTruckAssignmentList([FromQuery] GetTruckAssignmentPaginatedListQuery query)
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
        [HttpGet("{id}")]
        [CustomAuthorizeAttribute("get_pass","ReadSingle")]
        public async Task<ActionResult> GetTruckAssignmentById(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetTruckAssignmentByIdQuery { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
    }
}