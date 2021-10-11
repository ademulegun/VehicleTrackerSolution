using IdentityServer4.Models;
using System.Collections.Generic;

namespace VehicleTracker.Infrastructure.IdentityServer
{
    public static class ResourceManager
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("VehicleTrackerAPI", "Vehicle Tracker API"),
            };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("roles", "My role(s)", new List<string>(){"role"})
            };
        }
    }
}
