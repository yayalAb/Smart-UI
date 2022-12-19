using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models;

public class AppdiveResponse
{
    public static ActionResult Response(ControllerBase controller, CustomResponse response)
    {

        return controller.StatusCode(response.StatusCode, response);

    }
}