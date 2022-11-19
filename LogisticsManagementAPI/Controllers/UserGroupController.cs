﻿
using Application.UserGroupModule.Commands.CreateUserGroup;
using Application.UserGroupModule.Commands.DeleteUserGroup;
using Application.UserGroupModule.Commands.UpdateUserGroup;
using Application.UserGroupModule.Queries.GetUserGroupById;
using Application.UserGroupModule.Queries.GetUserGroupList;
using Application.UserGroupModule.Queries.GetUserGroupPaginatedList;
using Application.UserGroupModule.Queries.UserGroupLookup;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{

    public class UserGroupController : ApiControllerBase
    {
          // GET api/<UserGroupController>/
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageCount ,[FromQuery] int? pageSize)
        {

            if(pageCount == 0 || pageCount == null || pageSize == 0 || pageSize == null ){
                var query = new GetUserGroupListQuery();
                var response = await Mediator.Send(query);
                return Ok(response);

            }
            else{
                var query = new GetUserGroupPaginatedListQuery{
                    PageCount = (int)pageCount,
                    PageSize = (int)pageSize
                };
                var response = await Mediator.Send(query);
            return Ok(response);
            }

            
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
                Message = "userGroup created successfully"
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

        [HttpGet]
        [Route("lookup")]
        public async Task<IActionResult> LookUp() {
            
            try{
                return Ok(await Mediator.Send(new UserGroupLookup()));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }
    }
}
