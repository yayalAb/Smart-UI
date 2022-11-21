
using Application.OperationModule.Commands.CreateOperation;
using Application.OperationModule.Commands.DeleteOperation;
using Application.OperationModule.Queries.GetOperationById;
using Application.OperationModule.Queries.GetOperationList;
using Application.OperationModule.Queries.GetOperationPaginatedList;
using Microsoft.AspNetCore.Mvc;
using Application.OperationModule.Commands.UpdateOperation;
using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class OperationController : ApiControllerBase
    {

        // GET api/<OperationController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageCount, [FromQuery] int? pageSize)
        {


            try
            {
                if (pageCount == 0 || pageCount == null || pageSize == 0 || pageSize == null)
                {
                    return Ok(await Mediator.Send(new GetOperationListQuery()));
                }
                else
                {
                    return Ok(
                        await Mediator.Send(
                            new GetOperationPaginatedListQuery
                            {
                                PageCount = (int)pageCount,
                                PageSize = (int)pageSize
                            }
                        )
                    );
                }
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

        // GET api/<OperationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetOperationByIdQuery { Id = id }));
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


        // POST api/<OperationController>
        [HttpPost]
        public async Task<IActionResult> CreateOperation([FromForm] CreateOperationCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
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

        // POST api/<OperationController>
        [HttpPut]
        public async Task<IActionResult> UpdateOperation([FromForm] UpdateOperationCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
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
