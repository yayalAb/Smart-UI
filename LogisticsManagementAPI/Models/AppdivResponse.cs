using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;

namespace WebApi.Models;

public class AppdiveResponse {
    public static ActionResult Response(ControllerBase controller, CustomResponse response){
        
        return controller.StatusCode(response.StatusCode, response);

    }
}