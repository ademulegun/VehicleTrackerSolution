using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using VehicleTracker.ApplicationServices.Common.Options;
using VehicleTracker.ApplicationServices.Common.ViewModels;
using VehicleTracker.Common.Model;

namespace VehicleTracker.ApplicationServices.Authentication.CommandHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponseViewModel>>
    {
        private readonly IIdentityService _identityService;
        private readonly IIdentityServerService _identityServerService;
        public LoginCommandHandler(IIdentityService identityService, IIdentityServerService identityServerService)
        {
            _identityService = identityService;
            _identityServerService = identityServerService;
        }

        public async Task<Result<TokenResponseViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            TokenResponseViewModel token = new TokenResponseViewModel();
            var result = await _identityService.LoginUser(request.UserName, request.Password);
            if (result)
            {
                var userId = await _identityService.GetUserIdByUsernNameAsync(request.UserName);
                var jwtSecurityToken = await _identityServerService.CreateJwtToken(userId, request.UserName);
                token.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return Result.Ok(token);
            }
            return Result.Fail<TokenResponseViewModel>("Invalid username or password");
        }
    }
}
