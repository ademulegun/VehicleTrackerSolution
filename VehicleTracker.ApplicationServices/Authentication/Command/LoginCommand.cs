using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.ViewModels;
using VehicleTracker.Common.Model;

namespace VehicleTracker.ApplicationServices.Authentication.Command
{
    public class LoginCommand: IRequest<Result<TokenResponseViewModel>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
