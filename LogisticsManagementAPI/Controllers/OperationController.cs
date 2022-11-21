
using Application.OperationModule.Commands.CreateOperation;
using Application.OperationModule.Commands.DeleteOperation;
using Application.OperationModule.Queries.GetOperationById;
using Application.OperationModule.Queries.GetOperationPaginatedList;
using Microsoft.AspNetCore.Mvc;
using Application.OperationModule.Commands.UpdateOperation;
using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;
using Application.UserGroupModule.Queries.GetOperationLookupQuery;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class OperationController : ApiControllerBase
    {

        // GET api/<OperationController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetOperationPaginatedListQuery query)
        {

            return Ok(await Mediator.Send(query));
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
        public async Task<IActionResult> CreateOperation([FromBody] CreateOperationCommand command)
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
        public async Task<IActionResult> UpdateOperation([FromBody] UpdateOperationCommand command)
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
            try
            {

                return Ok(await Mediator.Send(new DeleteOperationCommand { Id = id }));
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
          [HttpGet]
        [Route("lookup")]
        public async Task<IActionResult> LookUp()
        {

            try
            {
                return Ok(await Mediator.Send(new GetOperationLookupQuery()));
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }
    }
}
