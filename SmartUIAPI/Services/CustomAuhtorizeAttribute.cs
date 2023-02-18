using System.Security.Claims;
using Application.Common.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmartUIAPI.Services;
public class CustomAuthorizeAttribute : TypeFilterAttribute
{
    public CustomAuthorizeAttribute(string ControllerName, string ControllerAction)
    : base(typeof(AuthorizeActionFilter))
    {
        Arguments = new object[] { ControllerName, ControllerAction };
    }
}

public class AuthorizeActionFilter : IAuthorizationFilter
{
    private readonly string _ControllerName;
    private readonly string _ControllerAction;
    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _dbContext;

    public AuthorizeActionFilter(string ControllerName, string ControllerAction, IIdentityService identityService, IAppDbContext dbContext)
    {
        _ControllerName = ControllerName;
        _ControllerAction = ControllerAction;
        _identityService = identityService;
        _dbContext = dbContext;
    }



    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // bool isAuthorized = MumboJumboFunction(context.HttpContext.User, _ControllerName, _ControllerAction); // :)

        var userId = context.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            context.Result = new ForbidResult();
            return;


        }
        var groupId = _identityService.GetUserGroupId(userId);
        var roles = _dbContext.AppUserRoles.Where(r => r.UserGroupId == groupId);
        var role = roles.Where(r => r.Page.ToLower() == _ControllerName.ToLower()).FirstOrDefault();
        bool isAuthorized = false;

        if (role == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        switch (_ControllerAction)
        {
            case "ReadAll":
                isAuthorized = role.CanView; break;
            case "ReadSingle":
                isAuthorized = role.CanViewDetail; break;
            case "Update":
                isAuthorized = role.CanUpdate; break;
            case "Delete":
                isAuthorized = role.CanDelete; break;
            case "Add":
                isAuthorized = role.CanAdd; break;
        }
        if(!isAuthorized ){
            context.Result = new ForbidResult();
        }


    }
}