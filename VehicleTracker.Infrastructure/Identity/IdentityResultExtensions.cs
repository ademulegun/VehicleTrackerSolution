using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Common.Model;

namespace VehicleTracker.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static AuthenticationResult ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? AuthenticationResult.Success()
                : AuthenticationResult.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
