using Microsoft.AspNetCore.Mvc;
using Application.DriverModule.Commands.CreateDriverCommand;
using Application.DriverModule.Commands.UpdateDriverCommand;
using Application.DriverModule.Commands.ChangeDriverImageCommand;
using Application.DriverModule.Queries.GetAllDriversQuery;
using Application.DriverModule.Queries.GetDriverQuery;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ApiControllerBase
    {
        
        [HttpPost]
        public async Task<ActionResult> create([FromForm] CreateDriverCommand command) {

            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult> change([FromBody] UpdateDriverCommand command) {

            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        [Route("change_image")]
        public async Task<ActionResult> changeImage([FromForm] ChangeDriverImage command){
            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> getDriver([FromQuery] GetDriver command){
            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> get([FromQuery] GetAllDrivers command){
            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

    }
}