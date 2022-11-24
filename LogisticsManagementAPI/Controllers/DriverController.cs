using Microsoft.AspNetCore.Mvc;
using Application.DriverModule.Commands.CreateDriverCommand;
using Application.DriverModule.Commands.UpdateDriverCommand;
using Application.DriverModule.Commands.ChangeDriverImageCommand;
using Application.DriverModule.Queries.GetAllDriversQuery;
using Application.DriverModule.Queries.GetDriverQuery;
using Application.DriverModule.Commands.DeleteDriverCommand;
using Application.Common.Models;
using WebApi.Models;
using Application.Common.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ApiControllerBase
    {
        
        [HttpPost]
        public async Task<ActionResult> create([ FromBody] CreateDriverCommand command) {

            try{
                return Ok(await Mediator.Send(command));
            }catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpPut]
        public async Task<ActionResult> change([FromBody] UpdateDriverCommand command) {

            try{
                return Ok(await Mediator.Send(command));
            }catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getDriver(int id){
            try{
                return Ok(await Mediator.Send(new GetDriver{Id = id}));
            }catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet]
        public async Task<ActionResult> get([FromQuery] GetAllDrivers command){
            try{
                return Ok(await Mediator.Send(command));
            }catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(int id){
            try{
                return Ok(await Mediator.Send(new DeleteDriver(){Id = id}));
            }catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

    }
}