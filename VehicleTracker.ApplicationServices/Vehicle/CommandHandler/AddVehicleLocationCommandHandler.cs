using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Exceptions;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.ApplicationServices.Vehicle.Command;

namespace VehicleTracker.ApplicationServices.Vehicle.CommandHandler
{
    public class AddVehicleLocationCommandHandler : IRequestHandler<AddVehicleLocationCommand, Unit>
    {
        private readonly IVehicleTrackerDbContext _db;
        private readonly IIdentityService _identityService;
        public AddVehicleLocationCommandHandler(IVehicleTrackerDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(AddVehicleLocationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId)) throw new AuthenticationException("Device is not authenticated");
            var vehicle = await _db.Vehicle.Where(x => x.OwnerIdentity.UserId == request.UserId).Select(x => x).FirstOrDefaultAsync(cancellationToken);
            var vehicleState = Core.Entities.Vehicle.AddVehicleState(vehicle, location: new Core.Entities.CarLocation(request.Longitude, request.Latitude)).Value;
            await _db.VehicleState.AddAsync(vehicleState);
            var savedResult = await _db.SaveChangesAsync(cancellationToken);
            if (savedResult > 0) return Unit.Value;
            throw new PersistenceException("Unable to save vehicle");
        }
    }
}
