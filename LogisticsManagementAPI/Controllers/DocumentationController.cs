using Application.Common.Exceptions;
using Application.Common.Models;
using Application.DocumentationModule.Commands.CreateDocumentation;
using Application.DocumentationModule.Commands.DeleteDocumentation;
using Application.DocumentationModule.Commands.UpdateDocumentation;
using Application.DocumentationModule.Queries.GetDocumentationById;
using Application.DocumentationModule.Queries.GetDocumentationList;
using Application.DocumentationModule.Queries.GetDocumentationPaginatedList;
using Application.OperationDocuments.Queries.Number4;
using Application.OperationDocuments.Queries.Number9;
using Application.OperationDocuments.Queries.PackageList;
using Application.OperationDocuments.Queries.T1Document;
using Application.OperationDocuments.Queries.TruckWayBill;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class DocumentationController : ApiControllerBase
    {
        // GET api/<DocumentationController>/
        [HttpGet]
        public async Task<IActionResult> GetDocumentationList([FromQuery] GetDocumentationPaginatedListQuery query)
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






        // POST api/<DocumentationController>
        [HttpPost]
        public async Task<IActionResult> CreateDocumentation([FromBody] CreateDocumentationCommand command)
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
        // PUT api/<DocumentationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentationById(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetDocumentationByIdQuery { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        // PUT api/<DocumentationController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateDocumentation([FromBody] UpdateDocumentationCommand command)
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

        // DELETE api/<DocumentationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentation(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new DeleteDocumentationCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet("packageLists/{operationId}")]
        public async Task<IActionResult> GeneratePackageList(int operationId)
        {

            try
            {
                return Ok(await Mediator.Send(new PackageList() { operationId = operationId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }catch(Exception ex){
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet("truckWayBill")]
        public async Task<IActionResult> GenerateTrackWayBill([FromQuery] TruckWayBill command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }catch(Exception ex){
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet("t1/{operationId}")]
        public async Task<IActionResult> GenerateT1(int operationId)
        {

            try {
                return Ok(await Mediator.Send(new T1Document {OperationId = operationId}));
            }
            catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            }catch(Exception ex){
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet("CommercialInvoice/{operationId}")]
        public async Task<IActionResult> GenerateCommercialInvoice(int operationId)
        {

            try {
                return Ok(await Mediator.Send(new Application.OperationDocuments.Queries.CommercialInvoice.CommercialInvoice { operationId = operationId}));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet("Number9")]
        public async Task<IActionResult> GenerateNumber9([FromQuery] Number9 command) {

            try {
                return Ok(await Mediator.Send(command));
            } catch(GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet("Number4/{operationId}")]
        public async Task<IActionResult> GenerateNumber4(int operationId) {

            try {
                return Ok(await Mediator.Send(new Number4 { OperationId = operationId}));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

    }
}
