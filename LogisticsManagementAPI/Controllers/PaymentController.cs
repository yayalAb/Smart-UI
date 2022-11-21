
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.PaymentModule.Commands.CreatePayment;
using Application.PaymentModule.Commands.DeletePayment;
using Application.PaymentModule.Commands.UpdatePayment;
using Application.PaymentModule.Queries.GetPaymentById;
using Application.PaymentModule.Queries.GetPaymentList;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class PaymentController : ApiControllerBase
    {
    
  // GET api/<PaymentController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPaymentListQuery query)
        {

            var response = await Mediator.Send(query);
            return Ok(response);
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetPaymentByIdQuery
            {
                Id = id
            };
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        // POST api/<PaymentController>
        [HttpPost]
        public async Task<IActionResult> CreatePayment( [FromBody] CreatePaymentCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = "payment created successfully"
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);
        }


        // PUT api/<PaymentController>/
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


        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
         try{

                return Ok( await Mediator.Send(new DeletePaymentCommand{Id = id})) ;
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
