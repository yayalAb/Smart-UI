using Application.Common.Exceptions;
using Application.Common.Models;
using Application.DocumentationModule.Commands.CreateDocumentation;
using Application.DocumentationModule.Commands.DeleteDocumentation;
using Application.DocumentationModule.Commands.UpdateDocumentation;
using Application.DocumentationModule.Queries.GetDocumentationById;
using Application.DocumentationModule.Queries.GetDocumentationPaginatedList;
// using Application.OperationDocuments.Number9Transfer;
// using Application.OperationDocuments.Number9Transfer;
using Application.OperationDocuments.Queries.CertificateOfOrigin;
using Application.OperationDocuments.Queries.CommercialInvoice;
using Application.OperationDocuments.Queries.Gatepass;
using Application.OperationDocuments.Queries.Number1;
using Application.OperationDocuments.Queries.Number4;
using Application.OperationDocuments.Queries.Number9;
using Application.OperationDocuments.Queries.Number9Transfer;
using Application.OperationDocuments.Queries.PackageList;
using Application.OperationDocuments.Queries.T1Document;
using Application.OperationDocuments.Queries.TruckWayBill;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class DocumentationController : ApiControllerBase
    {
        // GET api/<DocumentationController>/
        [HttpGet]
        [CustomAuthorizeAttribute("Documentation", "ReadAll")]

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
        [CustomAuthorizeAttribute("Documentation", "Add")]

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
        [CustomAuthorizeAttribute("Documentation", "ReadSingle")]

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
        [CustomAuthorizeAttribute("Documentation", "Update")]
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
        [CustomAuthorizeAttribute("Documentation", "Delete")]
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

        [HttpGet("packageLists")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]
        public async Task<IActionResult> GeneratePackageList([FromQuery] PackageList command)
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

        [HttpGet("certificateOfOrigin/{operationId}")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]

        public async Task<IActionResult> GenerateCertificateOfOrigin(int operationId)
        {

            try
            {
                return Ok(await Mediator.Send(new CertificateOfOrigin() { operationId = operationId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            

        }

        [HttpGet("truckWayBill")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]

        public async Task<IActionResult> GenerateTrackWayBill([FromQuery] TruckWayBill command)
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

        [HttpGet("t1/{operationId}")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]

        public async Task<IActionResult> GenerateT1(int operationId)
        {

            try
            {
                return Ok(await Mediator.Send(new T1Document { OperationId = operationId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
          
        }

        [HttpGet("CommercialInvoice")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]

        public async Task<IActionResult> GenerateCommercialInvoice([FromQuery] CommercialInvoice command)
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

        [HttpGet("Number9")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]

        public async Task<IActionResult> GenerateNumber9([FromQuery] Number9 command)
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
        [HttpPost("TransferNumber9")]
        [CustomAuthorizeAttribute("Print", "Add")]

        public async Task<IActionResult> GenerateTransferNumber9([FromBody] GenerateDocRequest request)
        {

            try
            {
                var command = new GenerateTransferNumber9Query
                {
                    OperationId = request.OperationId,
                    NameOnPermitId = request.NameOnPermitId,
                    DestinationPortId = request.DestinationPortId,
                    ContainerIds = request.ContainerIds,
                    GoodIds = request.GoodIds,
                    isPrintOnly = false
                };
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
           
        }

        [HttpPost("Number4")]
        [CustomAuthorizeAttribute("Print", "Add")]

        public async Task<IActionResult> GenerateNumber4([FromBody] GenerateDocRequest request)
        {

            try
            {
                var command = new Number4
                {
                    OperationId = request.OperationId,
                    NameOnPermitId = request.NameOnPermitId,
                    DestinationPortId = request.DestinationPortId,
                    ContainerIds = request.ContainerIds,
                    GoodIds = request.GoodIds,
                    isPrintOnly = false
                };
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
           

        }


        [HttpGet("Gatepass/{truckAssignmentId}")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]

        public async Task<IActionResult> PrintGatepass(int truckAssignmentId)
        {

            try
            {
                return Ok(await Mediator.Send(new PrintGatepassQuery { TruckAssignmentId = truckAssignmentId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
           

        }




    }
}
