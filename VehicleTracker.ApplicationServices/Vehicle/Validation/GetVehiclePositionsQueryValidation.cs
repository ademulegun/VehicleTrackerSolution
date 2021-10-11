using FluentValidation;
using System;
using VehicleTracker.ApplicationServices.Vehicle.Query;

namespace VehicleTracker.ApplicationServices.Vehicle.Validation
{
    public class GetVehiclePositionsQueryValidation : AbstractValidator<GetVehiclePositionsQuery>
    {
        public GetVehiclePositionsQueryValidation()
        {
            RuleFor(x => x.VehicleId).NotNull().WithMessage("VehicleId must be provided");
            RuleFor(x => x.VehicleId).Must(vehicleId => vehicleId != default).WithMessage("Please pass in a valid vehicle id");
            RuleFor(x => x.From).NotEmpty().WithMessage("From date must be provided");
            RuleFor(x => x.From).NotEqual("string").WithMessage("From value can not be string");
            RuleFor(x => x.From).Must(BeAValidDate).WithMessage("From date must be valid");
            RuleFor(x => x.To).NotEmpty().WithMessage("To date must be provided");
            RuleFor(x => x.To).NotEqual("string").WithMessage("To value can not be string");
            RuleFor(x => x.To).Must(BeAValidDate).WithMessage("To date must be valid");
        }

        private bool BeAValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }
    }
}
