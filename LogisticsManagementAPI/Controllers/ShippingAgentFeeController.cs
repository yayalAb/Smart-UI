
using Application.ShippingAgentFeeModule.Commands.CreateShippingAgentFee;
using Application.ShippingAgentFeeModule.Commands.UpdateShippingAgentFee;
using Application.ShippingAgentFeeModule.Queries.GetShippingAgentFeeById;
using Microsoft.AspNetCore.Mvc;



namespace WebApi.Controllers
{

    public class ShippingAgentFeeController : ApiControllerBase
    {
    

        // GET api/<ShippingAgentFeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var command = new GetShippingAgentFeeByIdQuery
            {
                Id = id
            };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        // POST api/<ShippingAgentFeeController>
        [HttpPost]
        public async Task<IActionResult> CreateShippingAgentFee( [FromBody] CreateShippingAgentFeeCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);
        }


        // PUT api/<ShippingAgentFeeController>/
        [HttpPut]
        public async Task<IActionResult> UpdateShippingAgentFee([FromBody] UpdateShippingAgentFeeCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = $"Shipping Agent Fee with id : {response} is updated successfully"
            };
            return Ok(responseObj);
        }

    }
}
