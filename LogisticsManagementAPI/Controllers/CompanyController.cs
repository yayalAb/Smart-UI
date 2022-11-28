using Microsoft.AspNetCore.Mvc;
using Application.CompanyModule.Commands.CreateCompanyCommand;
using Application.CompanyModule.Queries.GetCompanyQuery;
using Application.CompanyModule.Commands.UpdateCompanyCommand;
using Application.CompanyModule.Queries.GetAllCompanyQuery;
using Application.CompanyModule.Commands.DeleteCompanyCommand;
using Application.Common.Models;
using Application.Common.Exceptions;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiControllerBase
    {
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> create([FromBody] CreateCompanyCommand command)
        {

            try{
                return Ok(await Mediator.Send(command));
            }catch(GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            }


        }

        [HttpPut]
        public async Task<ActionResult> update([FromBody] UpdateCompanyCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }catch(GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult> view(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCompanyQuery(id)));
            }catch(GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet]
        public async Task<ActionResult> list([FromQuery] GetAllCompanies command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }catch(GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> delete([FromQuery] DeleteCompany command) {
            try{
                return Ok(await Mediator.Send(command));
            }catch(GhionException ex) {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

    }
}