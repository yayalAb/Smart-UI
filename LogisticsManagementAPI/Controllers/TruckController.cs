using Microsoft.AspNetCore.Mvc;
using Application.TruckModule.Commands.CreateTruckCommand;

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

    }
}