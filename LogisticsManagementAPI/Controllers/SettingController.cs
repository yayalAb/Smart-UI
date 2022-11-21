using Application.SettingModule.Command.UpdateSettingCommand;
using Application.SettingModule.Command.CreateSettingCommand;
using Application.SettingModule.Queries.GetSettingQuery;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;
using Application.Common.Exceptions;
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
        catch (Exception ex)
        {
            return AppdiveResponse.Response(this, CustomResponse.Failed(ex.Message));
        }

    }

    [HttpPost]
    public async Task<ActionResult> create(CreateSetting command)
    {
        try
        {
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
    public async Task<ActionResult> read()
    {
        try
        {
            return Ok(await Mediator.Send(new GetSettings()));
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