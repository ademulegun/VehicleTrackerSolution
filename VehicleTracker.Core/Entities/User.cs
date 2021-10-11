using System;
using System.Collections.Generic;
using System.Linq;
using VehicleTracker.Common.Model;
using VehicleTracker.Core.Events;
using VehicleTracker.Core.Exceptions;
using VehicleTracker.Core.ValueTypes;

namespace VehicleTracker.Core.Entities
{
    public class User : BaseEntity<Guid>
    {
        private User() { }
        private User(string name, string email, OwnerIdentity ownerIdentity)
        {
            Name = name;
            OwnerIdentity = ownerIdentity;
            Email = email;
            Apply(new UserEvents.UserCreated() { UserId = Guid.Parse(ownerIdentity.UserId), Name = Name });
        }

        public string Name { get; set; }
        public OwnerIdentity OwnerIdentity { get; set; }
        public string Email { get; set; }
        public List<VehicleState> VehicleDetails { get; private set; } = new List<VehicleState>();
        public IEnumerable<object> GetVehicles => VehicleDetails.AsEnumerable();

        public static Result<User> Create(string name, string email, OwnerIdentity OwnerIdentity)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidEntityException("Name must be provided");
            if (string.IsNullOrEmpty(email))
                throw new InvalidEntityException("Email must be provided");
            return Result.Ok(new User(name, email, OwnerIdentity));
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case UserEvents.UserCreated e:
                    Id = e.UserId;
                    Name = e.Name;
                    break;
            };
        }
    }
}
