using Application.DocumentationModule.Commands.CreateDocumentation;
using Application.DocumentationModule.Commands.UpdateDocumentation;
using Application.DocumentationModule.Queries.GetDocumentationById;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    public class DocumentationController : ApiControllerBase
    {


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
        public async Task<IActionResult> GetDocumentation(int id)
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
        public void Delete(int id)
        {
        }
    }
}
