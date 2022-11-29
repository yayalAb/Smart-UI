
using Application.Common.Exceptions;
using Application.OperationDocuments.Queries.Number1;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;
public class DocumentGenerationController : ApiControllerBase
{
        [HttpGet]
        [Route("Number1")]
        public async Task<ActionResult> get([FromQuery] GenerateNumber1Query query)
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