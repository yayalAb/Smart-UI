using Microsoft.AspNetCore.Mvc;
using Application.TruckModule.Commands.CreateTruckCommand;
using Application.TruckModule.Commands.ChangeTruckImageCommand;
using Application.TruckModule.Commands.UpdateTruckCommand;
using Application.TruckModule.Queries.GetTruckQuery;
using Application.TruckModule.Queries.GetAllTruckQuery;
using Application.TruckModule.Commands.DeleteTruckCommand;
using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteTruck(int id){
          try{

                return Ok( await Mediator.Send(new DeleteTruck{Id = id})) ;
            }
            catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message ));
            }
        }

    }
}