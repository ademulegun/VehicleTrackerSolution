using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.ApplicationServices.Authentication.CommandHandler;
using VehicleTracker.ApplicationServices.Common.Interfaces;
using Xunit;

namespace VehicleTracker.IntegrationTests.CommandsHandlers
{
    public class LoginCommandHandlerShould : IntegrationTestBase
    {
        private readonly LoginCommandHandler commandHandler;
        public LoginCommandHandlerShould(BaseTest fixture): base(fixture)
        {
            var identityService = fixture.Services.GetRequiredService<IIdentityService>();
            var identityServerService = fixture.Services.GetRequiredService<IIdentityServerService>();
            commandHandler = new LoginCommandHandler(identityService, identityServerService);
        }

        [Theory]
        [InlineData("james@me.com", "1234jk")]
        public async Task Fail_given_username_password_is_grong(string username, string password)
        {
            //Arrange
            var _sut = new LoginCommand();
            _sut.UserName = username;
            _sut.Password = password;
            //Act
            var result = await commandHandler.Handle(_sut, new System.Threading.CancellationToken());
            //Assert
            result.Error.Should().Be("Invalid username or password");
        }
    }
}
