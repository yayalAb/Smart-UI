using Application.SettingModule.Command.UpdateSettingCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class SettingController : ApiControllerBase {
    [HttpPut]
    public async Task<ActionResult> update(UpdateSetting command){
        try{
            return Ok(await Mediator.Send(command));
        }catch(Exception ex){
            return NotFound(ex.Message);
        }
    }
}