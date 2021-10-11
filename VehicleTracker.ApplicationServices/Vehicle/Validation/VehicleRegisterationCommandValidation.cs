using FluentValidation;
using VehicleTracker.ApplicationServices.Vehicle.Command;

namespace VehicleTracker.ApplicationServices.Vehicle.Validation
{
    public class VehicleRegisterationCommandValidation : AbstractValidator<VehicleRegisterationCommand>
    {
        public VehicleRegisterationCommandValidation()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username must be provided");
            RuleFor(x => x.Username).NotEqual("string").WithMessage("Username value can not be string");
            RuleFor(x => x.DeviceNumber).NotEmpty().WithMessage("DeviceNumber must be provided");
            RuleFor(x => x.DeviceNumber).NotEqual("string").WithMessage("Device number value can not be string");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be provided");
            RuleFor(x => x.Password).NotEqual("string").WithMessage("Password value can not be string");
        }
    }
}
