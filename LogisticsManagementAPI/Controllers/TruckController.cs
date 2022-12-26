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
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckController : ApiControllerBase
    {


        [HttpPost]
        [CustomAuthorizeAttribute("Truck","Add")]
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
        [CustomAuthorizeAttribute("Truck","Update")]
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
        [CustomAuthorizeAttribute("Truck","Update")]
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
        [CustomAuthorizeAttribute("Truck","ReadSingle")]
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
        [CustomAuthorizeAttribute("Truck","ReadAll")]
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
        [CustomAuthorizeAttribute("Truck","ReadAll")]
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
        [CustomAuthorizeAttribute("Truck","Delete")]
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
        [CustomAuthorizeAttribute("Truck","ReadAll")]
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