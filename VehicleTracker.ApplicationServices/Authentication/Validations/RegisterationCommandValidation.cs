using FluentValidation;
using VehicleTracker.ApplicationServices.Authentication.Command;

namespace VehicleTracker.ApplicationServices.Authentication.Validations
{
    public class RegisterationCommandValidation : AbstractValidator<RegisterationCommand>
    {
        public RegisterationCommandValidation()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username must be provided");
            RuleFor(x => x.Password).NotEqual("string").WithMessage("Username can not be string");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be provided");
            RuleFor(x => x.Password).NotEqual("string").WithMessage("Password can not be string");
        }
    }
}
