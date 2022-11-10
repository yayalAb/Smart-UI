using Application.BillOfLoadingModule.Commands.CreateBillOfLoading;
using Application.BillOfLoadingModule.Commands.DeleteBillOfLoading;
using Application.BillOfLoadingModule.Commands.UpdateBillOfLoading;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class BillOfLoadingController : ApiControllerBase
    {
      
        // POST api/<BillOfLoadingController>
        [HttpPost]
        public async Task<IActionResult> CreateBillOfLoading([FromForm] CreateBillOfLoadingCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };
            return StatusCode(StatusCodes.Status201Created,responseObj);    

        }

        // PUT api/<BillOfLoadingController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateBillOfLoading( [FromForm] UpdateBillOfLoadingCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = $"Bill of Loading with id : {response} is updated successfully"
            };
            return Ok(responseObj);

        }

        // DELETE api/<BillOfLoadingController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillOfLoading(int id)
        {
            var command = new DeleteBillOfLoadingCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            var responseObj = new
            {
                message = "BillOfLoading deleted successfully"
            };
            return Ok(responseObj);
        }
    }
}
