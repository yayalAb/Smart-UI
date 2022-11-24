
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UserGroupModule.Commands.CreateUserGroup;
using Application.UserGroupModule.Commands.DeleteUserGroup;
using Application.UserGroupModule.Commands.UpdateUserGroup;
using Application.UserGroupModule.Queries.GetUserGroupById;
using Application.UserGroupModule.Queries.GetUserGroupList;
using Application.UserGroupModule.Queries.GetUserGroupPaginatedList;
using Application.UserGroupModule.Queries.UserGroupLookup;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class UserGroupController : ApiControllerBase
    {
        // GET api/<UserGroupController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserGroupPaginatedListQuery query)
        {

            return Ok(await Mediator.Send(query));
        }


        // GET api/<UserGroupController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroupById(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetUserGroupByIdQuery { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        // POST api/<UserGroupController>
        [HttpPost]
        public async Task<IActionResult> CreateUserGroup([FromBody] CreateUserGroupCommand command)
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

        // PUT api/<UserGroupController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateUserGroup([FromBody] UpdateUserGroupCommand command)
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

        // DELETE api/<UserGroupController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGroup(int id)
        {
            try
            {

                return Ok(await Mediator.Send(new DeleteUserGroupCommand { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet]
        [Route("lookup")]
        public async Task<IActionResult> LookUp()
        {

            try
            {
                return Ok(await Mediator.Send(new UserGroupLookup()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
    }
}
