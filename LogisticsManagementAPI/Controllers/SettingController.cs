using Application.Common.Exceptions;
using Application.Common.Models;
using Application.CompanyModule.Queries.GetAllCompanyQuery;
using Application.DocumentationModule.Queries.GetDocumentationPaginatedList;
using Application.DriverModule.Queries.GetAllDriversQuery;
using Application.Image.GetImage;
using Application.LookUp.Query.GetAllLookups;
using Application.OperationModule.Queries.GetOperationPaginatedList;
using Application.PaymentModule.Queries.GetPaymentList;
using Application.PortModule.Queries.GetAllPortsQuery;
using Application.SettingModule.Command.CreateTodayCurrencyRate;
using Application.SettingModule.Command.UpdateSettingCommand;
using Application.SettingModule.Queries.DefaultCompany;
using Application.ShippingAgentModule.Queries.GetShippingAgentPaginatedList;
using Application.TruckAssignmentModule.Queries.GetTruckAssignmentPaginatedList;
using Application.TruckModule.Queries.GetAllTruckQuery;
using Application.User.Queries.GetAllUsersQuery;
using Application.UserGroupModule.Queries.GetUserGroupPaginatedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

public class SettingController : ApiControllerBase
{
    [HttpPut]
    public async Task<ActionResult> update(UpdateSetting command)
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
    public async Task<ActionResult> read()
    {
        try
        {
            return Ok(await Mediator.Send(new GetDefaultCompany()));
        }
        catch (GhionException ex)
        {
            return AppdiveResponse.Response(this, ex.Response);
        }
    }

    [HttpPost]
    [Route("addCurrency")]
    public async Task<ActionResult> addCurrency(AddCurrencyRate command) {
        try {
            return Ok(await Mediator.Send(command));
        } catch (GhionException ex) {
            return AppdiveResponse.Response(this, ex.Response);
        }
    }

    [HttpGet("gridSearch/{gridName}/{word}")]
    public async Task<ActionResult> gridSearch(string gridName, string word)
    {
        try {

            switch (gridName.ToLower()) {
                case "user":
                    return Ok(await Mediator.Send(new UserListSearch {Word = word}));
                case "operation":
                    return Ok(await Mediator.Send(new SearchOperation {Word = word}));
                case "shippingAgent":
                    return Ok(await Mediator.Send(new SearchShippingagent {Word = word}));
                case "payment":
                    return Ok(await Mediator.Send(new PaymentListSearch {Word = word}));
                case "driver":
                    return Ok(await Mediator.Send(new DriverListSearch {Word = word}));
                case "truck":
                    return Ok(await Mediator.Send(new TruckListSearch {Word = word}));
                case "port":
                    return Ok(await Mediator.Send(new PortListSearch {Word = word}));
                case "company":
                    return Ok(await Mediator.Send(new CompanyListSearch {Word = word}));
                case "getpass":
                    return Ok(await Mediator.Send(new TruckAssignmentListSearch {Word = word}));
                case "usergroup":
                    return Ok(await Mediator.Send(new UserGroupListSearch {Word = word}));
                case "lookup":
                    return Ok(await Mediator.Send(new LookupListSearch {Word = word}));
                case "documentation":
                    return Ok(await Mediator.Send(new DocumentationListSearch {Word = word}));
                default:
                    return AppdiveResponse.Response(this, CustomResponse.NotFound("Grid not found"));
            }

        }
        catch (GhionException ex)
        {
            return AppdiveResponse.Response(this, ex.Response);
        }
    }

    [HttpGet("image/{type}/{id}")]
    public async Task<ActionResult> getImage(string type, int id)
    {
        try
        {
            return Ok(await Mediator.Send(new GetImageById { Id = id, Type = type }));
        }
        catch (GhionException ex)
        {
            return AppdiveResponse.Response(this, ex.Response);
        }
    }

}