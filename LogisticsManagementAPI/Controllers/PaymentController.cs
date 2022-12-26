
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.PaymentModule.Commands.CreatePayment;
using Application.PaymentModule.Commands.DeletePayment;
using Application.PaymentModule.Commands.UpdatePayment;
using Application.PaymentModule.Queries.GetPaymentById;
using Application.PaymentModule.Queries.GetPaymentByOperation;
using Application.PaymentModule.Queries.GetPaymentList;
using Application.PaymentModule.Queries.TotalPayments;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{

    public class PaymentController : ApiControllerBase
    {

        // GET api/<PaymentController>/
        [HttpGet]
        [CustomAuthorizeAttribute("Payment","ReadAll")]
        public async Task<IActionResult> Get([FromQuery] GetPaymentListQuery query)
        {

            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        [CustomAuthorizeAttribute("Payment","ReadSingle")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetPaymentByIdQuery { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("Dashboard")]
        [CustomAuthorizeAttribute("Dashboard","ReadAll")]
        public async Task<IActionResult> TotalPayment()
        {
            try
            {
                return Ok(await Mediator.Send(new TotalPayments()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpGet("ByOperation/{operationId}")]
        [CustomAuthorizeAttribute("Payment","ReadAll")]
        public async Task<IActionResult> paymentByOperation(int operationId)
        {
            try
            {
                return Ok(await Mediator.Send(new PaymentByOperation { OperationId = operationId }));
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

        // POST api/<PaymentController>
        [HttpPost]
        [CustomAuthorizeAttribute("Payment","Add")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }


        // PUT api/<PaymentController>/
        [HttpPut]
        [CustomAuthorizeAttribute("Payment","Update")]
        public async Task<IActionResult> UpdatePayment([FromBody] UpdatePymentCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }


        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        [CustomAuthorizeAttribute("Payment","Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeletePaymentCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

    }
}
