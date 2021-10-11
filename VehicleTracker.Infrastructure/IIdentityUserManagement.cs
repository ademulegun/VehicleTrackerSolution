using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Infrastructure.Identity;

namespace VehicleTracker.Infrastructure
{
    public interface IIdentityUserManagement
    {
        Task<List<System.Security.Claims.Claim>> GetClaimsAsync(string userId);
        Task<List<string>> GetRolesAsync(string userId);
    }
}
