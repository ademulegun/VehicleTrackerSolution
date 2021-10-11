using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.ApplicationServices.Vehicle.Command;
using VehicleTracker.ApplicationServices.Vehicle.Query;
using VehicleTracker.ApplicationServices.ViewModel;
using VehicleTracker.Common.Model;

namespace VehicleTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : BaseController
    {
        /// <summary>
        /// This endpoint is used to register a vehicle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ///<remarks>
        ///Sample request:
        ///
        ///  POST /registerVehicleLocation 
        ///  {
        ///     "Username": "1.23456",
        ///     "Password": "1.2345",
        ///     "DeviceNumber": "123456789",
        ///     "FuelCapacity": "123456789",
        ///     "Color": "12Red3456789",
        ///     "Model": "Toyottttiii",
        ///     "Speed": "25km"
        ///  }
        ///</remarks>
        [HttpPost("registerVehicle")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(Result<string>), 400)]
        [ProducesResponseType(typeof(Result<string>), 500)]
        public async Task<IActionResult> RegisterVehicle([FromBody] VehicleRegisterationCommand model) => Ok(await Mediator.Send(model));

        /// <summary>
        /// this endpoint is used to get a vehicle's position for a specified period in time        
        /// </summary>
        /// <returns></returns>
        [HttpGet("getVehiclePositions/{from}/{to}/{vehicleId}")]
        [ProducesResponseType(typeof(Result<List<VehicleStatesDto>>), 200)]
        [ProducesResponseType(typeof(Result<string>), 400)]
        [ProducesResponseType(typeof(Result<string>), 500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetVehiclePositions(string from, string to, Guid vehicleId) => 
            Ok(await Mediator.Send(new GetVehiclePositionsQuery()
            { 
                From = from, To = to, VehicleId = vehicleId
            }));

        /// <summary>
        /// this endpoint is used to get a vehicle's current position        
        /// </summary>
        /// <returns></returns>
        [HttpGet("getVehiclePosition/{vehicleId}")]
        [ProducesResponseType(typeof(Result<VehicleStatesDto>), 200)]
        [ProducesResponseType(typeof(Result<string>), 400)]
        [ProducesResponseType(typeof(Result<string>), 500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetVehiclePosition(Guid vehicleId) => Ok(await Mediator.Send(new GetVehicleCurrentPositionsQuery()
        {
            VehicleId = vehicleId
        }));


        /// <summary>
        /// This endpoint is used to register a vehicle location pertime
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ///<remarks>
        ///Sample request:
        ///
        ///  POST /registerVehicleLocation 
        ///  {
        ///     "Latitude": "1.23456",
        ///     "Longitude": "1.2345"
        ///  }
        ///</remarks>
        [HttpPost("registerVehicleLocation")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(Result<string>), 400)]
        [ProducesResponseType(typeof(Result<string>), 500)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RegisterVehicleLocation([FromBody] AddVehicleLocationDto model) => Ok(await Mediator.Send(new AddVehicleLocationCommand()
        {
            UserId = User.Claims.Where(x=>x.Type == "userId").Select(x=>x.Value).FirstOrDefault(),
            Latitude = model.Latitude,
            Longitude = model.Longitude
        }));
    }
}
