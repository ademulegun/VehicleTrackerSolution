using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.ApplicationServices.Common.Exceptions;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.ApplicationServices.Vehicle.Command;
using VehicleTracker.Core.Entities;
using VehicleTracker.Core.Enums;

namespace VehicleTracker.ApplicationServices.Vehicle.CommandHandler
{
    public class VehicleRegisterationCommandHandler : IRequestHandler<VehicleRegisterationCommand, Unit>
    {
        private readonly IVehicleTrackerDbContext _db;
        private readonly IIdentityService _identityService;
        public VehicleRegisterationCommandHandler(IVehicleTrackerDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(VehicleRegisterationCommand request, CancellationToken cancellationToken)
        {
            var doesDeviceWithIdExist = await _db.Vehicle.AnyAsync(x => x.DeviceNumber == request.DeviceNumber);
            if (doesDeviceWithIdExist) throw new DuplicateException($"Vehicle with {request.DeviceNumber} already exist");
            var result = await _identityService.CreateUserAsync(request.Username, request.Password);
            if (result.Result.Succeeded)
            {
                await _identityService.AddUserToRole(request.Username, Roles.User.ToString());
                var vehicle = Core.Entities.Vehicle.Create(request.DeviceNumber, new Core.ValueTypes.OwnerIdentity(result.UserId)).Value.SetColor(request.Color)
                    .SetModel(request.Model).SetFuelCapacity(request.FuelCapacity).SetSpeed(request.Speed);
                await _db.Vehicle.AddAsync(vehicle);
                var savedResult = await _db.SaveChangesAsync(cancellationToken);
                if (savedResult > 0) return Unit.Value;
                throw new Common.Exceptions.AuthenticationException("Unable to save vehicle owner");
            }
            //Or Notify UI using signalr about failed registration
            var error = string.Join(',', result.Result.Errors);
            throw new Common.Exceptions.AuthenticationException(error);
        }
    }
}
