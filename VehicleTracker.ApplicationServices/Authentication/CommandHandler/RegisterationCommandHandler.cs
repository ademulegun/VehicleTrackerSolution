using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.Core.Entities;
using VehicleTracker.Core.Enums;
using IPublisher = VehicleTracker.ApplicationServices.Common.Interfaces.IPublisher;

namespace VehicleTracker.ApplicationServices.Authentication.CommandHandler
{
    public class RegisterationCommandHandler : IRequestHandler<RegisterationCommand, Unit>
    {
        private readonly IVehicleTrackerDbContext _db;
        private readonly IIdentityService _identityService;
        private readonly IPublisher _publisher;
        public RegisterationCommandHandler(IVehicleTrackerDbContext db, IIdentityService identityService,
            IPublisher publisher)
        {
            _db = db;
            _identityService = identityService;
            _publisher = publisher;
        }

        public async Task<Unit> Handle(RegisterationCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateUserAsync(request.Username, request.Password);
            if (result.Result.Succeeded)
            {
                await _identityService.AddUserToRole(request.Username, Roles.Administrator.ToString());
                var user = User.Create(request.Username, request.Username, new Core.ValueTypes.OwnerIdentity(result.UserId));
                await _db.User.AddAsync(user.Value);
                var savedResult = await _db.SaveChangesAsync(cancellationToken);
                if (savedResult > 0)
                {
                    var events = user.Value.GetChanges();
                    foreach (var @event in events)
                    {
                        // publish using signalr - websocket
                    }
                    return Unit.Value;
                }
                throw new AuthenticationException("Unable to save user");
            }
            var error = string.Join(',', result.Result.Errors);
            throw new AuthenticationException(error);
        }
    }
}
