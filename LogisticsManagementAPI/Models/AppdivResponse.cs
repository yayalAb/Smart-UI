using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;

namespace WebApi.Models;

public class AppdiveResponse {
    public static ActionResult Response(ControllerBase controller, CustomResponse response){
        
        // if(response.StatusCode == 401){
        //     return Unauthorized(response);
        // }

        // if(response.StatusCode == 400){
        //     return NotFound(response);
        // }

        // else {
        //     return BadRequest(response);
        // }

        return controller.StatusCode(response.StatusCode, response);

    }
}