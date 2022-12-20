
using Application.Common.Exceptions;
using Application.GeneratedDocumentModule.Queries;
using Application.OperationDocuments.Queries.GoodsRemoval;
using Application.OperationDocuments.Queries.Number1;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;
public class DocumentGenerationController : ApiControllerBase
{
    [HttpPost]
    [Route("Number1")]
    public async Task<ActionResult> generateNumber1([FromBody] GenerateNumber1Query query)
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
    [Route("GoodsRemoval")]
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


}