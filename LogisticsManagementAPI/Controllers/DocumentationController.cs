using Application.Common.Exceptions;
using Application.Common.Models;
using Application.DocumentationModule.Commands.CreateDocumentation;
using Application.DocumentationModule.Commands.DeleteDocumentation;
using Application.DocumentationModule.Commands.UpdateDocumentation;
using Application.DocumentationModule.Queries.GetDocumentationById;
using Application.DocumentationModule.Queries.GetDocumentationList;
using Application.DocumentationModule.Queries.GetDocumentationPaginatedList;
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
                
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
            }

            
        



        // POST api/<DocumentationController>
        [HttpPost]
        public async Task<IActionResult> CreateDocumentation([FromBody] CreateDocumentationCommand command)
        {

            try {
                return Ok(await Mediator.Send(command));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }
        // PUT api/<DocumentationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentationById(int id)
        {

            try{
                return Ok(await Mediator.Send(new GetDocumentationByIdQuery { Id = id }));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
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
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        // DELETE api/<DocumentationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentation(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new DeleteDocumentationCommand {Id = id}));
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
