using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Vehicle.Command;

namespace VehicleTracker.ApplicationServices.Vehicle.Validation
{
    public class AddVehicleLocationCommandValidation: AbstractValidator<AddVehicleLocationCommand>
    {
        public AddVehicleLocationCommandValidation()
        {
            RuleFor(x => x.Latitude).NotEmpty().WithMessage("Latitude must be provided");
            RuleFor(x => x.Latitude).NotEqual("string").WithMessage("Latitude can not be string");
            RuleFor(x => x.Longitude).NotEmpty().WithMessage("Longitude must be provided");
            RuleFor(x => x.Longitude).NotEqual("string").WithMessage("Longitude can not be string");
            RuleFor(x => x.UserId).NotEqual("string").WithMessage("User id can not be string");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be null or empty");
        }
    }
}
