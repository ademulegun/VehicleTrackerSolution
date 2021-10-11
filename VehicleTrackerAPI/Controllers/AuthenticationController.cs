using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.ApplicationServices.Common.ViewModels;
using VehicleTracker.Common.Model;

namespace VehicleTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        /// <summary>
        /// This endpoint is used to register a vehicle location pertime. The username and pwd is for test case
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
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(Result<TokenResponseViewModel>), 200)]
        [ProducesResponseType(typeof(Result<string>), 400)]
        [ProducesResponseType(typeof(Result<string>), 500)]
        public async Task<IActionResult> Authenticate([FromBody]LoginCommand model) 
            =>
                Ok(await Mediator.Send(model));
    }
}
