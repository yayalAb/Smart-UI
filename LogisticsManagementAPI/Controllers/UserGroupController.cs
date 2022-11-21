
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
        public async Task<IActionResult> Get([FromQuery] int? pageCount ,[FromQuery] int? pageSize)
        {

            try{
                if(pageCount == 0 || pageCount == null || pageSize == 0 || pageSize == null ){
                    return Ok(await Mediator.Send(new GetUserGroupListQuery()));
                }
                else{
                    return Ok(
                        await Mediator.Send(
                            new GetUserGroupPaginatedListQuery{
                                PageCount = (int)pageCount,
                                PageSize = (int)pageSize
                            }
                        )
                    );
                }
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

        // GET api/<UserGroupController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroupById(int id)
        {
            
            try{
                return Ok(await Mediator.Send(new GetUserGroupByIdQuery {Id = id}));
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

        // POST api/<UserGroupController>
        [HttpPost]
        public async Task<IActionResult> CreateUserGroup([FromBody] CreateUserGroupCommand command)
        {

            try{
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

        // PUT api/<UserGroupController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateUserGroup( [FromBody] UpdateUserGroupCommand command)
        {
            
            try{
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

        // DELETE api/<UserGroupController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGroup(int id)
        {
            var command = new DeleteUserGroupCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            var responseObj = new
            {
                message = "userGroup deleted successfully"
            };
            return Ok(responseObj);
        }

        [HttpGet]
        [Route("lookup")]
        public async Task<IActionResult> LookUp() {
            
            try{
                return Ok(await Mediator.Send(new UserGroupLookup()));
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
