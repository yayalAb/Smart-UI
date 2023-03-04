using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Project.Commands;
using Application.Component.Queries;
using Microsoft.AspNetCore.Mvc;
using SmartUIAPI.Models;
using Domain.Entities;
using MediatR;
using Application.Button.Commands.CreateButtonCommand;
using Application.Button.Commands.DeleteButtonCommand;
using Application.Button.Commands.UpdateButtonCommand;
using Application.Button.Queries.GetAllButtons;

namespace SmartUIAPI.Controllers
{
    public class ButtonsController: ApiControllerBase
    {
          private readonly IHttpContextAccessor _httpContextAccessor;

        public ButtonsController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

             // GET api/<buttonController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllButtonQuery query)
        {

            return Ok(await Mediator.Send(query));
        }
         [HttpPost]
         public async Task<IActionResult> CreateComponent([FromBody] CreateButtonCommand command)
        {
            try
            {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = "Button created successfully"
            };

            return StatusCode(StatusCodes.Status201Created, responseObj);
                
            }
            catch (GhionException ex)
            {
             
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
       [HttpPut]
        public async Task<IActionResult> UpdateComponent([FromBody] UpdateButtonCommand command)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteButton(int id)
        {
            try
            {

                return Ok(await Mediator.Send(new DeleteButttonCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
        
    }
}