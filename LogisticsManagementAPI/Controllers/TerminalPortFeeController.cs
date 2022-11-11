using Application.ShippingAgentFeeModule.Commands.UpdateShippingAgentFee;
using Application.ShippingAgentFeeModule.Queries.GetShippingAgentFeeById;
using Application.TerminalPortFeeModule.Commands.CreateTerminalPortFee;
using Application.TerminalPortFeeModule.Commands.UpdateTerminalPortFee;
using Application.TerminalPortFeeModule.Queries.GetTerminalPortFeeById;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class TerminalPortFeeController : ApiControllerBase
    {


        // GET api/<TerminalPortFeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var command = new GetTerminalPortFeeByIdQuery
            {
                Id = id
            };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        // POST api/<TerminalPortFeeController>
        [HttpPost]
        public async Task<IActionResult> CreateTerminalPortFee([FromBody] CreateTerminalPortFeeCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);
        }

        // PUT api/<TerminalPortFeeController>/
        [HttpPut]
        public async Task<IActionResult> UpdateTerminalPortFee([FromBody] UpdateTerminalPortFeeCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = $"Terminal Port Fee with id : {response} is updated successfully"
            };
            return Ok(responseObj);
        }

     
    }
}
