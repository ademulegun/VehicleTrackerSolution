using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.ViewModels;
using VehicleTracker.Common.Model;

namespace VehicleTracker.ApplicationServices.Common.Interfaces
{
    public interface IIdentityServerService
    {
        Result<TokenResponseViewModel> GenerateToken(string userId, string username);
        Task<System.IdentityModel.Tokens.Jwt.JwtSecurityToken> CreateJwtToken(string userId, string username);
        Task<Result<TokenResponseViewModel>> ValidateUser(string username, string password);
    }
}
