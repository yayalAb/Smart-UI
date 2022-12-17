using Application.Common.Exceptions;
using Application.TruckModule.Commands.CreateTruckCommand;
using Application.TruckModule.Commands.DeleteTruckCommand;
using Application.TruckModule.Commands.ReleaseTruck;
using Application.TruckModule.Commands.UpdateTruckCommand;
using Application.TruckModule.Queries.GetAllTruckQuery;
using Application.TruckModule.Queries.GetTruckQuery;
using Application.TruckModule.Queries.GetUnassignedTrucks;
using Application.UserGroupModule.Queries.GetTruckLookupQuery;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckController : ApiControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> create([FromBody] CreateTruckCommand command)
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
        [Route("release/{id}")]
        public async Task<ActionResult> Release(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new ReleaseTruckCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpPut]
        public async Task<ActionResult> update([FromBody] UpdateTruckCommand command)
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
        public async Task<ActionResult> get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetTruckQuery(id)));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet]
        public async Task<ActionResult> getAll([FromQuery] GetAllTrucks command)
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
        [Route("unassigned")]
        public async Task<ActionResult> getUnassigned()
        {
            try
            {
                return Ok(await Mediator.Send(new GetUnassignedTrucksQuery()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteTruck(int id)
        {
            try
            {

                return Ok(await Mediator.Send(new DeleteTruck { Id = id }));
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
                return Ok(await Mediator.Send(new GetTruckLookupQuery()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
    }
}