using Application.ContainerModule.Commands.UpdateContainer;
using Application.PortModule.Commands.CreatePort;
using Application.PortModule.Commands.UpdatePort;
using Microsoft.AspNetCore.Mvc;
using Application.PortModule.Queries.GetAllPortsQuery;
using Application.PortModule.Queries.GetPort;
using Application.PortModule.Commands.DeletePort;
using Application.Common.Exceptions;
using WebApi.Models;
using Application.Common.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class PortController : ApiControllerBase
    {
      
        // POST api/<PortController>
        [HttpPost]
        public async Task<IActionResult> CreatePort([FromBody] CreatePortCommand command)
        {
            
            try{
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
        // PUT api/<PortController>/
        [HttpPut]
        public async Task<IActionResult> UpdatePort([FromBody] UpdatePortCommand command)
        {
            try{
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

        [HttpGet]
        public async Task<ActionResult> get([FromQuery] GetAllPorts command){
            try{
                return Ok(await Mediator.Send(command));
            }catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getPort(int id){
            try{
                return Ok(await Mediator.Send(command));
            }catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch (Exception ex)
            {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
                return Ok(await Mediator.Send(new GetPort{Id=id}));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(int id) {
            try{

                return Ok( await Mediator.Send(new DeletePort{Id = id})) ;
            }
            catch(GhionException ex){
                return AppdiveResponse.Response(this, ex.Response);
            }
            catch(Exception ex) {
                return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message ));
            }
        }

    }
}
