using System;
using VehicleTracker.Common.Model;
using VehicleTracker.Core.Enums;
using VehicleTracker.Core.Exceptions;

namespace VehicleTracker.Core.Entities
{
    public class VehicleState : BaseEntity<Guid>
    {
        private VehicleState() { }

        private VehicleState(Vehicle vehicle, CarLocation location)
        {
            Status = Status.Disconnected;
            SyncDate = DateTime.Now;
            Vehicle = vehicle;
            CarLocation = location;
            Created = DateTime.Now;
        }

        private VehicleState(Status status, DateTime syncDate, CarLocation carLocation)
        {
            Status = status;
            SyncDate = syncDate;
            CarLocation = carLocation;
        }
        public Status Status { get; set; }
        public DateTime SyncDate { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public CarLocation CarLocation { get; set; }
        public static Result<VehicleState> Create(Vehicle vehicle, CarLocation location)
        {
            if (vehicle == null)
                throw new InvalidEntityException("Vehicle must be provided");
            if (location == null)
                throw new InvalidEntityException("Location must be provided");
            return Result.Ok(new VehicleState(vehicle, location));
        }

        public static Result<VehicleState> UpdateState(Status status, DateTime syncDate, CarLocation carLocation)
        {
            return Result.Ok(new VehicleState(status, syncDate, carLocation));
        }

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
