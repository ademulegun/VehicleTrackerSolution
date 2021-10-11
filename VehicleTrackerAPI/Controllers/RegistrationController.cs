using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.Common.Model;

namespace VehicleTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        /// <summary>
        /// This endpoint is used to register a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ///<remarks>
        ///Sample request:
        ///
        ///  POST /authenticate 
        ///  {
        ///     "UserName": "me@me.com",
        ///     "Password": "1234"
        ///  }
        ///</remarks>
        [HttpPost("registerUser")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(Result<string>), 400)]
        [ProducesResponseType(typeof(Result<string>), 500)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterationCommand model) 
            =>
                Ok(await Mediator.Send(model));
    }
}
