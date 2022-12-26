
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.GeneratedDocumentModule.Queries;
using Application.OperationDocuments.Queries.GoodsRemoval;
using Application.OperationDocuments.Queries.Number1;
using Application.OperationDocuments.Queries.Number4;
using Application.OperationDocuments.Queries.Number9Transfer;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
public class DocumentGenerationController : ApiControllerBase
{
    [HttpPost]
    [Route("Number1")]
        [CustomAuthorizeAttribute("Print", "Add")]

    public async Task<ActionResult> generateNumber1([FromBody] GenerateDocRequest request)
    {
        try
        {
            var query = new GenerateNumber1Query
            {
                OperationId = request.OperationId,
                NameOnPermitId = request.NameOnPermitId,
                DestinationPortId = request.DestinationPortId,
                ContainerIds = request.ContainerIds,
                GoodIds = request.GoodIds,
                isPrintOnly = false
            };
            return Ok(await Mediator.Send(query));
        }
        catch (GhionException ex)
        {
            return AppdiveResponse.Response(this, ex.Response);
        }

    }
    [HttpGet]
    [Route("GoodsRemoval")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]

    public async Task<ActionResult> generateGoodsRemoval([FromQuery] GenerateGoodsRemovalQuery query)
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

    [HttpGet]
    [Route("GeneratedDocumentsList")]
    [CustomAuthorizeAttribute("Print", "ReadAll")]

    public async Task<ActionResult> generatedDocs([FromQuery] GetAllGeneratedDocumentsQuery query)
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

        [HttpGet("printDocument")]
        [CustomAuthorizeAttribute("Print", "ReadSingle")]
        public async Task<IActionResult> PrintDocument([FromQuery] int documentId, string documentType)
        {

            try
            {
                if (documentType.ToUpper() == Enum.GetName(typeof(Documents), Documents.TransferNumber9)!.ToUpper())
                {
                    return Ok(await Mediator.Send(new GenerateTransferNumber9Query { isPrintOnly = true, GeneratedDocumentId = documentId }));
                }
                if (documentType.ToUpper() == Enum.GetName(typeof(Documents), Documents.Number1)!.ToUpper())
                {
                    return Ok(await Mediator.Send(new GenerateNumber1Query { isPrintOnly = true, GeneratedDocumentId = documentId }));
                }
                if (documentType.ToUpper() == Enum.GetName(typeof(Documents), Documents.Number4)!.ToUpper())
                {
                    return Ok(await Mediator.Send(new Number4 { isPrintOnly = true, GeneratedDocumentId = documentId }));
                }
                throw new GhionException(CustomResponse.BadRequest("invalid document type"));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            

        }


}