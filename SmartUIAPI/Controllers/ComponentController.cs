using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Project.Commands;
using Application.Component.Commands.createComponent;
using Application.Component.Commands.UpdateComponent;
using Application.Component.Commands.DeleteComponent;
using Application.Component.Queries;
using Microsoft.AspNetCore.Mvc;
using SmartUIAPI.Models;
using Domain.Entities;
using MediatR;

namespace SmartUIAPI.Controllers
{
    public class ComponentController: ApiControllerBase
    {
          private readonly IHttpContextAccessor _httpContextAccessor;

        public ComponentController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

             // GET api/<UserGroupController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllComponetsQuery query)
        {

            return Ok(await Mediator.Send(query));
        }
         [HttpPost]
         public async Task<IActionResult> CreateComponent([FromBody] CreateComponentCommand command)
        {
            try
            {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = "Componet created successfully"
            };

            return StatusCode(StatusCodes.Status201Created, responseObj);
                
            }
            catch (GhionException ex)
            {
             
                return AppdiveResponse.Response(this, ex.Response);
            }
           

        }
       [HttpPut]
        public async Task<IActionResult> UpdateComponent([FromBody] UpdateComponentCommand command)
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
        public async Task<IActionResult> DeleteComponet(int id)
        {
            try
            {

                return Ok(await Mediator.Send(new DeleteComponentCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
        
    }
}