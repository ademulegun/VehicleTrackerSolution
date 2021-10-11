using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.ApplicationServices.Extensions;
using VehicleTracker.ApplicationServices.Vehicle.Query;
using VehicleTracker.ApplicationServices.ViewModel;
using VehicleTracker.Common.Model;

namespace VehicleTracker.ApplicationServices.Vehicle.QueryHandler
{
    public class GetVehicleCurrentPositionsQueryHandler : IRequestHandler<GetVehicleCurrentPositionsQuery, Result<VehicleStatesDto>>
    {
        private readonly IVehicleTrackerDbContext _db;
        public GetVehicleCurrentPositionsQueryHandler(IVehicleTrackerDbContext db)
        {
            _db = db;
        }

        public async Task<Result<VehicleStatesDto>> Handle(GetVehicleCurrentPositionsQuery request, CancellationToken cancellationToken)
        {
            var vehicleStates = await _db.VehicleState.OrderByDescending(x=>x.Created).Where(x=>x.VehicleId == request.VehicleId)
                                    .Select(VehicleStatesDto.Projection).FirstOrDefaultAsync();
            return Result.Ok(vehicleStates);
        }
    }
}
