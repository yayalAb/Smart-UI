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
using Application.UserGroupModule.Queries.GetTruckLookupQuery;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TruckController : ApiControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> create([FromBody] CreateTruckCommand command)
        {

<<<<<<< HEAD
            try{
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
=======
            try
            {
                var response = await Mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
>>>>>>> dd25959 (feat: dropdown lookups done)
            }

        }

        // [HttpPut]
        // [Route("change_image")]
        // public async Task<ActionResult> changeImage([FromForm] ChangeTruckImageCommand command){
        //     try{
        //         return Ok(await Mediator.Send(command));
        //     }
        //     catch (GhionException ex)
        //     {
        //         return AppdiveResponse.Response(this, ex.Response);
        //     }
        //     catch (Exception ex)
        //     {
        //         return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
        //     }
        // }

        [HttpPut]
<<<<<<< HEAD
        public async Task<ActionResult> update([ FromBody] UpdateTruckCommand command){
            try{
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
=======
        public async Task<ActionResult> update([FromBody] UpdateTruckCommand command)
        {
            try
            {
                var response = await Mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
>>>>>>> dd25959 (feat: dropdown lookups done)
            }
        }

        [HttpGet("{id}")]
<<<<<<< HEAD
        public async Task<ActionResult> get(int id){
            try{
                return Ok(await Mediator.Send(new GetTruckQuery(id)));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
=======
        public async Task<ActionResult> get(int id)
        {
            try
            {
                var response = await Mediator.Send(new GetTruckQuery(id));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
>>>>>>> dd25959 (feat: dropdown lookups done)
            }
        }

        [HttpGet]
        public async Task<ActionResult> getAll([FromQuery] GetAllTrucks command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
<<<<<<< HEAD
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
=======
            catch (Exception ex)
            {
                return NotFound(ex.Message);
>>>>>>> dd25959 (feat: dropdown lookups done)
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
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
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
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }
    }
}