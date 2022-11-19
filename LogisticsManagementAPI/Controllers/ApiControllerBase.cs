using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public abstract class ApiControllerBase :ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        // public ActionResult AppdivResponse(CustomResponse response){
            
        //     // if(response.StatusCode == 401){
        //     //     return Unauthorized(response);
        //     // }

        //     // if(response.StatusCode == 400){
        //     //     return NotFound(response);
        //     // }

        //     // else {
        //     //     return BadRequest(response);
        //     // }

        //     return StatusCode(response.StatusCode, response);

        // }
    }
}
