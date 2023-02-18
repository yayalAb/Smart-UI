
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Project.Commands;
using Application.Project.Commands.ProjectDeleteCommand;
using Application.Project.Commands.ProjectCreateCommand;
using  Application.Project.Commands.ProjectUpdateCommands;
using Application.Project.Query.GetAllProjects;
using Microsoft.AspNetCore.Mvc;
using SmartUIAPI.Models;
using Domain.Entities;
using MediatR;

namespace SmartUIAPI.Controllers
{
    public class ProjectController: ApiControllerBase
    {
          private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

             // GET api/<UserGroupController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProjects query)
        {

            return Ok(await Mediator.Send(query));
        }
         [HttpPost]
         public async Task<IActionResult> CreateProjects([FromBody] ProjectCreateCommand command)
        {
            try
            {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                message = "Project created successfully"
            };

            return StatusCode(StatusCodes.Status201Created, responseObj);
                
            }
            catch (GhionException ex)
            {
             
                return AppdiveResponse.Response(this, ex.Response);
            }
           

        }
       [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectUpdateCommands command)
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
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {

                return Ok(await Mediator.Send(new ProjectDeleteCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
        
    }
}