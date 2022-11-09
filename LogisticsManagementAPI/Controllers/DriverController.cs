using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.DriverModule.Commands.CreateDriverCommand;
// using Application.CompanyModule.Queries.GetCompanyQuery;
using Application.DriverModule.Commands.UpdateDriverCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ApiControllerBase
    {
        
        [HttpPost]
        public async Task<ActionResult> create([FromForm] CreateDriverCommand command) {

            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult> change([FromBody] UpdateDriverCommand command) {

            try{
                var response = await Mediator.Send(command);
                return Ok(response);
            }catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

    }
}