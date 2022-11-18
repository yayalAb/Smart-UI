using Application.DocumentationModule.Commands.CreateDocumentation;
using Application.DocumentationModule.Commands.DeleteDocumentation;
using Application.DocumentationModule.Commands.UpdateDocumentation;
using Application.DocumentationModule.Queries.GetDocumentationById;
using Application.DocumentationModule.Queries.GetDocumentationList;
using Application.DocumentationModule.Queries.GetDocumentationPaginatedList;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class DocumentationController : ApiControllerBase
    {
         // GET api/<DocumentationController>/
        [HttpGet]
        public async Task<IActionResult> GetDocumentationList([FromQuery] int? pageNumber ,[FromQuery] int? pageSize)
        {

            if(pageNumber == 0 || pageNumber == null || pageSize == 0 || pageSize == null ){
                var query = new GetDocumentationListQuery();
                var response = await Mediator.Send(query);
                return Ok(response);

            }
            else{
                var query = new GetDocumentationPaginatedListQuery{
                    pageNumber = (int)pageNumber,
                    pageSize = (int)pageSize
                };
                var response = await Mediator.Send(query);
            return Ok(response);
            }

            
        }



        // POST api/<DocumentationController>
        [HttpPost]
        public async Task<IActionResult> CreateDocumentation([FromBody] CreateDocumentationCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);
        }
        // PUT api/<DocumentationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentationById(int id)
        {
            var command = new GetDocumentationByIdQuery
            {
                Id = id
            };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<DocumentationController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateDocumentation([FromBody] UpdateDocumentationCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = $"Documentation with id : {response} is updated successfully"
            };
            return Ok(responseObj);

        }

        // DELETE api/<DocumentationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentation(int id)
        {
            var command = new DeleteDocumentationCommand{
                Id = id
            };
            var response = await Mediator.Send(command);
            var responseObj = new {
                Message = "Documentation deleted successfully"
            };
            return Ok(responseObj);
        }
    }
}
