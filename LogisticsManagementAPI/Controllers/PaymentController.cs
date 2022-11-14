
using Application.PaymentModule.Commands.CreatePayment;
using Application.PaymentModule.Commands.UpdatePayment;
using Application.ShippingAgentFeeModule.Queries.GetShippingAgentFeeById;
using Microsoft.AspNetCore.Mvc;



namespace WebApi.Controllers
{

    public class PaymentController : ApiControllerBase
    {
    

        // GET api/<ShippingAgentFeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var command = new GetPaymentByIdQuery
            {
                Id = id
            };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        // POST api/<ShippingAgentFeeController>
        [HttpPost]
        public async Task<IActionResult> CreatePayment( [FromBody] CreatePaymentCommand command)
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
        public async Task<IActionResult> UpdatePayment([FromBody] UpdatePymentCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = $"Payment with id : {response} is updated successfully"
            };
            return Ok(responseObj);
        }

    }
}
