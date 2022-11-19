using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase :ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        public ActionResult AppdivResponse(CustomResponse response){
            
            return StatusCode(response.StatusCode, response);

        }
    }
}
