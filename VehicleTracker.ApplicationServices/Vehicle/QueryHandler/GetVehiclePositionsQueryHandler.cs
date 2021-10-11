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
    public class GetVehiclePositionsQueryHandler : IRequestHandler<GetVehiclePositionsQuery, Result<List<VehicleStatesDto>>>
    {
        private readonly IVehicleTrackerDbContext _db;
        public GetVehiclePositionsQueryHandler(IVehicleTrackerDbContext db)
        {
            _db = db;
        }

        public async Task<Result<List<VehicleStatesDto>>> Handle(GetVehiclePositionsQuery request, CancellationToken cancellationToken)
        {
            var from = Convert.ToDateTime(request.From).Date;
            var to = Convert.ToDateTime(request.To).Date;
            var vehicleStates = await _db.VehicleState.Select(VehicleStatesDto.Projection).ToListAsync();
            var searchedLocations = vehicleStates.Search(() => vehicleStates.Where(x => x.Created.Date >= from && x.Created <= to).ToList())
                .Search(() => vehicleStates.Where(x => x.VehicleId == request.VehicleId).ToList());

            return Result.Ok(searchedLocations);
        }
    }
}
