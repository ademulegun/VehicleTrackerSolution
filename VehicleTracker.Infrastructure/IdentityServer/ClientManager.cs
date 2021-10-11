using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Infrastructure.IdentityServer
{
    public static class ClientManager
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                    new Client
                    {
                         ClientName = "VehicleTracker",
                         ClientId = "VehicleTrackerAPI",
                         AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                         ClientSecrets = { new Secret("eb300de4-add9-42f4-a3ac-abd3c60f1923".Sha256()) },
                         AllowedScopes = new List<string> { "VehicleTrackerAPI" }
                    }
            };
    }
}
