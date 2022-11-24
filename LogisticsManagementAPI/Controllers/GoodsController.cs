using Microsoft.AspNetCore.Mvc;
using Application.Common.Exceptions;
using WebApi.Models;
using Application.GoodModule.Commands.AssignGoodsCommand;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class GoodsController : ApiControllerBase
    {

        // POST api/<GoodsController>
        [HttpPost]
        [Route("assign")]
        public async Task<IActionResult> AssignGoods([FromBody] AssignGoodsCommand command) {

            try {
                return Ok(await Mediator.Send(command));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } 

        }
    
    }
}