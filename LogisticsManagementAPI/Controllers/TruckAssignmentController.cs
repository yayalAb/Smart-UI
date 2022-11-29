using Microsoft.AspNetCore.Mvc;

using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;
using Application.TruckAssignmentModule.Commands.CreateTruckAssignment;
using Application.TruckAssignmentModule.Commands.UpdateTruckAssignment;
using Application.TruckAssignmentModule.Queries;
using Application.OperationDocuments.Queries.GenerateGatepass;
using Application.TruckAssignmentModule.Queries.GetTruckAssignmentPaginatedList;

namespace WebApi.Controllers
{

    public class TruckAssignmentController : ApiControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> create([FromBody] CreateTruckAssignmentCommand command)
        {

            try
            {
                var createResponse = await Mediator.Send(command);
                var command2 = new GenerateGatepassCommand
                {
                    OperationId = createResponse.operationId,
                    TruckAssignmentId = createResponse.truckAssignmentId
                };

                return Ok(await Mediator.Send(command2));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
        [HttpPut]
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
        public async Task<ActionResult> GetTruckAssignmentByOperationId([FromQuery] GetTruckAssignmentsByOperationIdQuery query)
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
        [HttpGet]
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
    }
}