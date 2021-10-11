using FluentValidation;
using VehicleTracker.ApplicationServices.Vehicle.Query;

namespace VehicleTracker.ApplicationServices.Vehicle.Validation
{
    public class GetVehicleCurrentPositionsQueryValidation : AbstractValidator<GetVehicleCurrentPositionsQuery>
    {
        public GetVehicleCurrentPositionsQueryValidation()
        {
            RuleFor(x => x.VehicleId).NotNull().WithMessage("VehicleId must be provided");
            RuleFor(x => x.VehicleId).Must(vehicleId => vehicleId != default).WithMessage("Please pass in a valid vehicle id");
        }
    }
}
