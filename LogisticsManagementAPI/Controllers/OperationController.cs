
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.OperationDocuments.SNumberUpdate;
using Application.OperationFollowupModule.Commands.UpdateStatus;
using Application.OperationFollowupModule.Queries.GetSingleStatus;
using Application.OperationFollowupModule.Queries.GetStatusByOperation;
using Application.OperationModule.Commands.CreateOperation;
using Application.OperationModule.Commands.DeleteOperation;
using Application.OperationModule.Commands.DispatchECD;
using Application.OperationModule.Commands.UpdateOperation;
using Application.OperationModule.Queries.GetOperationById;
using Application.OperationModule.Queries.GetOperationList;
using Application.OperationModule.Queries.GetOperationLookup;
using Application.OperationModule.Queries.GetOperationPaginatedList;
using Application.OperationModule.Queries.OperationDashboard;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

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
        [CustomAuthorizeAttribute("Operation","ReadSingle")]
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
        [CustomAuthorizeAttribute("Operation","Add")]
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
        [CustomAuthorizeAttribute("Operation","Update")]
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
        [CustomAuthorizeAttribute("Operation","Update")]
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
        [CustomAuthorizeAttribute("Operation","Update")]
        public async Task<IActionResult> snumberUpdate([FromQuery] SetSNumber command)
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
        [CustomAuthorizeAttribute("Operation","Delete")]
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
        [CustomAuthorizeAttribute("Operation_Followup","ReadAll")]
        public async Task<IActionResult> singleStatus(int OperationId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetStatusByOperation { OperationId = OperationId }));
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
        [Route("SingleStatus/{id}")]
        [CustomAuthorizeAttribute("Operation_Followup","ReadSingle")]
        public async Task<IActionResult> SingleStatus(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new SingleStatus { Id = id }));
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

        [HttpPut]
        [Route("status/{operationId}")]
        [CustomAuthorizeAttribute("Operation_Followup","Update")]
        public async Task<IActionResult> updateStatus(int operationId)
        {

            try
            {
                return Ok(await Mediator.Send(new UpdateStatus { Id = operationId }));
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
        [Route("Dashboard/operationCount")]
        [CustomAuthorizeAttribute("Dashboard","ReadAll")]
        public async Task<IActionResult> operationCount()
        {

            try
            {
                return Ok(await Mediator.Send(new OperationDashboardInfo()));
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
        [Route("Dashboard/opeartionList")]
        [CustomAuthorizeAttribute("Dashboard","ReadAll")]
        public async Task<IActionResult> opeartionList([FromQuery] DashboardOperationList command) {

            try {
                return Ok(await Mediator.Send(command));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet("Dashboard/graph/{year}")]
        [CustomAuthorizeAttribute("Dashboard","ReadAll")]
        public async Task<IActionResult> operationGrph(int year)
        {

            try
            {
                return Ok(await Mediator.Send(new OperationCountGraph { year = year }));
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

    }
}
