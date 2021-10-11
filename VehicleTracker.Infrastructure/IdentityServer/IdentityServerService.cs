using IdentityModel.Client;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.ApplicationServices.Common.Options;
using VehicleTracker.ApplicationServices.Common.ViewModels;
using VehicleTracker.ApplicationServices.ViewModel;
using VehicleTracker.Common.Model;
using VehicleTracker.Infrastructure.Infrastructure;

namespace VehicleTracker.Infrastructure.IdentityServer
{
    public class IdentityServerService: IIdentityServerService
    {
        private readonly IdentityHttpClient _identityHttpClient;
        private static IdentityServerConfig _IdentityServerConfig;
        private readonly IIdentityUserManagement _identityService;
        private readonly JwtDto _jwtDto;
        public IdentityServerService(IdentityHttpClient identityHttpClient, IOptionsMonitor<IdentityServerConfig> options, IOptionsMonitor<JwtDto> jwtDto, IIdentityUserManagement identityService)
        {
            _identityHttpClient = identityHttpClient;
            _IdentityServerConfig = options.CurrentValue;
            _jwtDto = jwtDto.CurrentValue;
            _identityService = identityService;
        }

        public Result<TokenResponseViewModel> GenerateToken(string userId, string username)
        {
            var payload = new Dictionary<string, object>
            {
                {"exp", DateTimeOffset.UtcNow.AddHours(24).ToUnixTimeSeconds()},
                {"username", username},
                {"userId", userId},
            };
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var tokenResult = encoder.Encode(payload, _IdentityServerConfig.TokenKey);
            return Result.Ok<TokenResponseViewModel>(new TokenResponseViewModel() { 
                Token = tokenResult
            });
        }

        public async Task<JwtSecurityToken> CreateJwtToken(string userId, string username)
        {
            var userClaims = await _identityService.GetClaimsAsync(userId);
            var roles = await _identityService.GetRolesAsync(userId);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i].ToString()));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, userId),
                new Claim(JwtRegisteredClaimNames.Email, username),
                new Claim("userId", userId),
                new Claim("ipaddress", GetLocalIPAddress())
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtDto.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtDto.Issuer,
                audience: _jwtDto.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtDto.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public async Task<Result<TokenResponseViewModel>> ValidateUser(string username, string password)
        {
            var tokenResponse = await _identityHttpClient._client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = $"{_IdentityServerConfig.Url}connect/token",
                ClientId = "VehicleTrackerAPI",
                ClientSecret = "eb300de4-add9-42f4-a3ac-abd3c60f1923",
                Scope = "VehicleTrackerAPI",
                Password = password,
                UserName = username,
            });
            if (tokenResponse.IsError)
            {
                return Result.Fail<TokenResponseViewModel>(tokenResponse.ErrorDescription);
            }
            return Result.Ok(new TokenResponseViewModel()
            {

            });
        }
    }
}
