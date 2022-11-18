
using Application.OperationModule.Commands.CreateOperation;
using Application.OperationModule.Commands.DeleteOperation;
using Application.OperationModule.Queries.GetOperationById;
using Application.OperationModule.Queries.GetOperationList;
using Application.OperationModule.Queries.GetOperationPaginatedList;
using Application.OperationModule.Commands.DeleteOperation;
using Microsoft.AspNetCore.Mvc;
using Application.OperationModule.Commands.UpdateOperation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class OperationController : ApiControllerBase
    {
      
      // GET api/<OperationController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageCount ,[FromQuery] int? pageSize)
        {

            if(pageCount == 0 || pageCount == null || pageSize == 0 || pageSize == null ){
                var query = new GetOperationListQuery();
                var response = await Mediator.Send(query);
                return Ok(response);

            }
            else{
                var query = new GetOperationPaginatedListQuery{
                    PageCount = (int)pageCount,
                    PageSize = (int)pageSize
                };
                var response = await Mediator.Send(query);
            return Ok(response);
            }

            
        }

        // GET api/<OperationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetOperationByIdQuery
            {
                Id = id
            };
            var response = await Mediator.Send(query);
            return Ok(response);
        }


        // POST api/<OperationController>
        [HttpPost]
        public async Task<IActionResult> CreateOperation([FromForm] CreateOperationCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = $"operation created successfully"
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);
        }

        // POST api/<OperationController>
        [HttpPut]
        public async Task<IActionResult> UpdateOperation([FromForm] UpdateOperationCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = $"operation with id = {response} updated successfully"
            };
            return Ok(responseObj);
        }


        // DELETE api/<OperationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            var command = new DeleteOperationCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            var responseObj = new
            {
                message = "Operation deleted successfully"
            };
            return Ok(responseObj);
        }
    }
}
