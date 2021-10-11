using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.ApplicationServices.Vehicle.Command;

namespace VehicleTracker.ApplicationServices.Authentication.Validations
{
    public class LoginCommandValidation: AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username must be provided");
            RuleFor(x => x.UserName).NotEqual("string").WithMessage("Username can not be string");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be provided");
            RuleFor(x => x.Password).NotEqual("string").WithMessage("Password can not be string");
        }
    }
}
