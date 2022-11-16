using Microsoft.AspNetCore.Mvc;
using Application.TruckModule.Commands.CreateTruckCommand;
using Application.TruckModule.Commands.ChangeTruckImageCommand;
using Application.TruckModule.Commands.UpdateTruckCommand;
using Application.TruckModule.Queries.GetTruckQuery;
using Application.TruckModule.Queries.GetAllTruckQuery;
using Application.TruckModule.Commands.DeleteTruckCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckController : ApiControllerBase {
        

        [HttpPost]
        public async Task<ActionResult> create([FromForm] CreateTruckCommand command) {

            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        [Route("change_image")]
        public async Task<ActionResult> changeImage([FromForm] ChangeTruckImageCommand command){
            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> update([FromForm] UpdateTruckCommand command){
            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> get(int id){
            try{
                var response = await Mediator.Send(new GetTruckQuery(id));
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> getAll([FromQuery] GetAllTrucks command){
            try {
                return Ok(await Mediator.Send(command));
            } catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> deleteTruck([FromQuery] DeleteTruck command){
            try {
                return Ok(await Mediator.Send(command));
            } catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

    }
}