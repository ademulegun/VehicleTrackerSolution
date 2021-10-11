using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Common.Model;
using VehicleTracker.Core.Enums;
using VehicleTracker.Core.Exceptions;
using VehicleTracker.Core.ValueTypes;

namespace VehicleTracker.Core.Entities
{
    public partial class Vehicle: BaseEntity<Guid>
    {
        private Vehicle(){}
        private Vehicle(string deviceNumber, OwnerIdentity ownerIdentity)
        {
            DeviceNumber = deviceNumber;
            OwnerIdentity = ownerIdentity;
            CurrentStatus = Status.Disconnected;
            LastSync = DateTime.Now;
            Created = DateTime.Now;
        }

        public string DeviceNumber { get; set; }
        public OwnerIdentity OwnerIdentity { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public Status CurrentStatus { get; set; }
        public DateTime LastSync { get; set; }
        public List<VehicleState> VehicleDetails { get;private set; } = new List<VehicleState>();
        public IEnumerable<object> GetVehicles => VehicleDetails.AsEnumerable();

        public static Result<Vehicle> Create(string deviceNumber, OwnerIdentity OwnerIdentity)
        {
            if (string.IsNullOrEmpty(deviceNumber))
                throw new InvalidEntityException("Vehicle registration number must be provided");
            return Result.Ok(new Vehicle(deviceNumber, OwnerIdentity));
        }

        public static Result<VehicleState> AddVehicleState(Vehicle vehicle, CarLocation location)
        {
            return Result.Ok(VehicleState.Create(vehicle, new CarLocation(location.Longitude, location.Latitude)).Value);
        }

        public static Result<VehicleState> UpdateVehicleState(Status status, DateTime syncDate, CarLocation carLocation)
        {
            return Result.Ok(VehicleState.UpdateState(status, syncDate, new CarLocation(carLocation.Longitude, carLocation.Latitude)).Value);
        }

        public Vehicle SetColor(string color)
        {
            this.Color = color;
            return this;
        }

        public Vehicle SetModel(string model)
        {
            this.Model = model;
            return this;
        }

        public Vehicle SetFuelCapacity(string fuelCapacity)
        {
            this.FuelCapacity = fuelCapacity;
            return this;
        }
        public Vehicle SetSpeed(string speed)
        {
            this.Speed = speed;
            return this;
        }

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }

    public partial class Vehicle : BaseEntity<Guid>
    {
        public string Speed { get; set; }
        public string FuelCapacity { get; set; }
    }
}
