using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ApplicationServices.Vehicle.Command
{
    public class VehicleRegisterationCommand: IRequest<Unit>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceNumber { get; set; }
        public string FuelCapacity { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string Speed { get; set; }
    }
}
