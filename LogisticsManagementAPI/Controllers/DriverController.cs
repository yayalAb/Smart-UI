using Application.Common.Exceptions;
using Application.Common.Models;
using Application.DriverModule.Commands.CreateDriverCommand;
using Application.DriverModule.Commands.DeleteDriverCommand;
using Application.DriverModule.Commands.ReleaseDriver;
using Application.DriverModule.Commands.UpdateDriverCommand;
using Application.DriverModule.Queries.DriverLookUpQuery;
using Application.DriverModule.Queries.GetAllDriversQuery;
using Application.DriverModule.Queries.GetDriverQuery;
using Application.DriverModule.Queries.GetUnassignedDrivers;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ApiControllerBase
    {

        [HttpPost]
        [CustomAuthorizeAttribute("Driver", "Add")]
        
        public async Task<ActionResult> create([FromBody] CreateDriverCommand command)
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
        [CustomAuthorizeAttribute("Driver", "Update")]

        public async Task<ActionResult> change([FromBody] UpdateDriverCommand command)
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
        [CustomAuthorizeAttribute("Driver", "Update")]

        public async Task<ActionResult> release(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new ReleaseDriverCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet("{id}")]
        [CustomAuthorizeAttribute("Driver", "ReadSingle")]

        public async Task<ActionResult> getDriver(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetDriver { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet]
        [CustomAuthorizeAttribute("Driver", "ReadAll")]

        public async Task<ActionResult> get([FromQuery] GetAllDrivers command)
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

        [HttpGet("lookup")]
        public async Task<ActionResult> driverLookup()
        {
            try
            {
                return Ok(await Mediator.Send(new DriverLookUp()));
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
        [Route("unassigned")]
        [CustomAuthorizeAttribute("Driver", "ReadAll")]

        public async Task<ActionResult> getUnassigned()
        {
            try
            {
                return Ok(await Mediator.Send(new GetUnassignedDriversQuery()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet]
        [Route("dashboard")]
        [CustomAuthorizeAttribute("Dashboard", "ReadAll")]

        public async Task<ActionResult> driversListDashboard([FromQuery] AllDrivers command)
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
        [CustomAuthorizeAttribute("Driver", "Delete")]

        public async Task<ActionResult> delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteDriver() { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

    }
}