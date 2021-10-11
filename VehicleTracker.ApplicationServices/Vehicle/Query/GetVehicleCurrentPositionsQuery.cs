using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.ViewModel;
using VehicleTracker.Common.Model;

namespace VehicleTracker.ApplicationServices.Vehicle.Query
{
    public class GetVehicleCurrentPositionsQuery : IRequest<Result<VehicleStatesDto>>
    {
        public Guid VehicleId { get; set; }
    }
}
