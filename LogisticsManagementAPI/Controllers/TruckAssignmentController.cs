using Microsoft.AspNetCore.Mvc;

using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;
using Application.TruckAssignmentModule.Commands.CreateTruckAssignment;

namespace WebApi.Controllers
{

    public class TruckAssignmentController : ApiControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> create([FromBody] CreateTruckAssignmentCommand command)
        {

            try{
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
          
        }

    }
}