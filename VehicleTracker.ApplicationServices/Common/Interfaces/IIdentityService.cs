using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Common.Model;

namespace VehicleTracker.ApplicationServices.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<string> GetUserIdByUsernNameAsync(string email);
        Task<bool> LoginUser(string email, string password);

        Task<(AuthenticationResult Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<bool> AddUserToRole(string email, string roleName);
    }
}
