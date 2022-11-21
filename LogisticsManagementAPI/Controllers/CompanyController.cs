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

            try
            {
                var response = await Mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult> update([FromBody] UpdateCompanyCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> view(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCompanyQuery(id)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> list([FromQuery] GetAllCompanies command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException gx)
            {
                return NotFound(gx.Response);
            }
            catch (Exception ex)
            {
                return NotFound(CustomResponse.Failed(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(int id)
        {
            try
            {

                return Ok(await Mediator.Send(new DeleteCompany { Id = id }));
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