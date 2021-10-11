using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ApplicationServices.Vehicle.Command
{
    public class AddVehicleLocationCommand: IRequest<Unit>
    {
        public string UserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
