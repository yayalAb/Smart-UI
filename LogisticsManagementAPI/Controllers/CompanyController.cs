using Microsoft.AspNetCore.Mvc;
using Application.CompanyModule.Commands.CreateCompanyCommand;
using Application.CompanyModule.Queries.GetCompanyQuery;
using Application.CompanyModule.Commands.UpdateCompanyCommand;
using Application.CompanyModule.Queries.GetAllCompanyQuery;
using Application.Common.Models;
using Application.Common.Exceptions;

namespace WebApi.Controllers {
    public class CompanyController : ApiControllerBase
    {
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> create([FromBody] CreateCompanyCommand command) {

            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult> update([FromBody] UpdateCompanyCommand command) {

            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> view(int id){
            try{
                GetCompanyQuery command = new GetCompanyQuery(id);
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> list([FromQuery] GetAllCompanies command){
            try{
                return Ok(await Mediator.Send(command));
            }catch(GhionException gx){
                return NotFound(gx.Response);
            }catch(Exception ex){
                return NotFound(CustomResponse.Failed(ex.Message));
            }
        }

    }
}