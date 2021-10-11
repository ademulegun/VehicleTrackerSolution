using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.Common.Model;
using VehicleTracker.Core.Enums;

namespace VehicleTracker.Infrastructure.Identity
{
    public class IdentityService : IIdentityService, IIdentityUserManagement
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
                         IAuthorizationService authorizationService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _roleManager = roleManager;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return string.Empty;
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            return user.UserName;
        }

        public async Task<string> GetUserIdByUsernNameAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) return string.Empty;
            var user = await _userManager.Users.FirstAsync(u => u.Email == email);
            return user.Id;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;
            var user = await _userManager.Users.FirstAsync(u => u.Email == email);
            if(user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return true;
            }
            return false;
        }

        public async Task<(AuthenticationResult Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> AddUserToRole(string email, string roleName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(roleName))
                return false;
            var user = await _userManager.FindByNameAsync(email);
            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }

        public async Task<List<System.Security.Claims.Claim>> GetClaimsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.ToList();
        }

        public async Task<List<string>> GetRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
