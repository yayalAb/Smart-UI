
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
using Application.OperationFollowupModule.Queries.GetStatusByOperation;
using Application.OperationFollowupModule.Commands.UpdateStatus;
using Application.OperationFollowupModule.Queries.GetSingleStatus;
using Application.OperationModule.Queries.OperationDashboard;
using Application.OperationModule.Commands.DispatchECD;
using Application.OperationDocuments.SNumberUpdate;

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
            
        }

        // POST api/<OperationController>
        [HttpPost]
        [Route("dispatchECD")]
        public async Task<IActionResult> DispatchECD([FromBody] DsipatchECDCommand command)
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

        }

        [HttpPut]
        [Route("SNumberUpdate")]
        public async Task<IActionResult> snumberUpdate([FromQuery] SetSNumber command) {

            try {
                return Ok(await Mediator.Send(command));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
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
        }
        
        [HttpGet]
        [Route("lookup")]
        public async Task<IActionResult> LookUp()
        {

            try
            {
                return Ok(await Mediator.Send(new GetOperationLookupQuery()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet]
        [Route("status/{OperationId}")]
        public async Task<IActionResult> singleStatus(int OperationId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetStatusByOperation{OperationId = OperationId}));
            }catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            }catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet]
        [Route("SingleStatus/{id}")]
        public async Task<IActionResult> SingleStatus (int id) {

            try {
                return Ok(await Mediator.Send(new SingleStatus{Id = id}));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpPut]
        [Route("status/{operationId}")]
        public async Task<IActionResult> updateStatus(int operationId) {

            try {
                return Ok(await Mediator.Send(new UpdateStatus {Id = operationId}));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet]
        [Route("Dashboard/operationCount")]
        public async Task<IActionResult> operationCount() {

            try {
                return Ok(await Mediator.Send(new OperationDashboardInfo()));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet("Dashboard/graph/{year}")]
        public async Task<IActionResult> operationGrph(int year) {

            try {
                return Ok(await Mediator.Send(new OperationCountGraph{year = year}));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

    }
}
