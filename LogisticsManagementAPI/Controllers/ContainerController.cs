
using Application.ContainerModule.Commands.CreateContainer;
using Application.ContainerModule.Commands.UpdateContainer;
using Application.ContainerModule.Queries.GetAllContainersQuery;
using Application.ContainerModule.Queries.GetContainerQuery;
using Application.ContainerModule.Commands.ContainerDelete;

using Microsoft.AspNetCore.Mvc;
using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class ContainerController : ApiControllerBase
    {

        // POST api/<ContainerController>
        [HttpPost]
        public async Task<IActionResult> CreateContainer([FromForm] CreateContainerCommand command) {

            try {
                return Ok(await Mediator.Send(command));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex){
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }


        // PUT api/<ContainerController>/
        [HttpPut]
        public async Task<IActionResult> UpdateContainer([FromForm] UpdateContainerCommand command) {

            try{
                return Ok(await Mediator.Send(command));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex){
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }

        }

        [HttpGet]
        public async Task<ActionResult> ContainerList([FromQuery] GetAllContainers command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex){
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetContainer(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetContainer(id)));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex){
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new ContainerDelete() { Id = id }));
            } catch (GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            } catch (Exception ex){
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

    }
}
