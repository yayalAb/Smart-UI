
using Application.Common.Exceptions;
using Application.CompanyModule.Commands.CreateCompanyCommand;
using Application.CompanyModule.Commands.DeleteCompanyCommand;
using Application.CompanyModule.Commands.UpdateCompanyCommand;
using Application.CompanyModule.Queries.GetAllCompanyQuery;
using Application.CompanyModule.Queries.GetCompanyBankInformation;
using Application.CompanyModule.Queries.GetCompanyQuery;
using Application.ContactPersonModule.Queries;
using Application.DriverModule.Queries.GetCompanyLookup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpPut]
        public async Task<ActionResult> update([FromBody] UpdateCompanyCommand command)
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

        [HttpGet("{id}")]
        public async Task<ActionResult> view(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCompanyQuery(id)));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
        [HttpGet]
        [Route("lookup")]
        public async Task<ActionResult> lookup()
        {
            try
            {
                return Ok(await Mediator.Send(new GetCompanyLookupQuery()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
        [HttpGet]
        public async Task<ActionResult> list([FromQuery] GetAllCompanies command)
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
        [HttpGet]
        [Route("nameOnPermit/{companyId}")]
        public async Task<ActionResult> nameOnPermits(int companyId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetContactPeopleByCompanyIdQuery { CompanyId = companyId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
        [HttpGet]
        [Route("bankInformation/{companyId}")]
        public async Task<ActionResult> bankInformation(int companyId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCompanyBankInformationQuery { CompanyId = companyId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
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

        }

    }
}