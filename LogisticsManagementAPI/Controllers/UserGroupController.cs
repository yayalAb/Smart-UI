using Application.LookUp.Commands.DeleteLookup;
using Application.UserGroupModule.Commands.CreateUserGroup;
using Application.UserGroupModule.Commands.DeleteUserGroup;
using Application.UserGroupModule.Commands.UpdateUserGroup;
using Application.UserGroupModule.Queries.GetAllUserGroups;
using Application.UserGroupModule.Queries.GetUserGroupById;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{

    public class UserGroupController : ApiControllerBase
    {
        // GET: api/<UserGroupController>
        [HttpGet]
        public async Task<IActionResult> GetAllUserGroups()
        {
            var userGroups = await Mediator.Send(new GetAllUserGroupsQuery());
            return Ok(userGroups);
        }

        // GET api/<UserGroupController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroupById(int id)
        {
            var query = new GetUserGroupByIdQuery
            {
                Id = id
            };
            var response = await Mediator.Send(query);  
            return Ok(response);    
        }

        // POST api/<UserGroupController>
        [HttpPost]
        public async Task<IActionResult> CreateUserGroup([FromBody] CreateUserGroupCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Id = response
            };
            return StatusCode(StatusCodes.Status201Created, responseObj);

        }

        // PUT api/<UserGroupController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateUserGroup( [FromBody] UpdateUserGroupCommand command)
        {
            var response = await Mediator.Send(command);
            var responseObj = new
            {
                Message = $"userGroup with id : {response} is updated successfully"
            };
            return Ok(responseObj);
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
    }
}
