using IdentityServer4.Models;
using System.Collections.Generic;

namespace VehicleTracker.Infrastructure.IdentityServer
{
    public static class ApiScopeConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("VehicleTrackerAPI", "Sending money for organisations")
            };
    }
}
