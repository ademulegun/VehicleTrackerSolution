using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Authentication.Command;
using VehicleTracker.ApplicationServices.Authentication.Validations;
using Xunit;

namespace VehicleTracker.UnitTest.Commands
{
    public class RegisterationCommandShould
    {
        [Fact]
        public void Should_return_error_message_given_default_value_for_username_and_passsword_is_string()
        {
            //Arrange
            var _sut = new RegisterationCommand() { Username = "string", Password = "string" };
            var validation = new RegisterationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Username can not be string");
        }

        [Fact]
        public void Should_return_error_message_given_null_or_empty__is_passed_for_username_and_passsword()
        {
            //Arrange
            var _sut = new RegisterationCommand() { Username = "", Password = "" };
            var validation = new RegisterationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Username must be provided");
            validatonResult.Errors[1].ErrorMessage.Should().Be("Password must be provided");
        }

        [Theory]
        [InlineData("babafemi.ibitolu@yahoo.com", "1234")]
        public void Should_succeed_given_username_and_passsword_is_passed(string username, string password)
        {
            //Arrange

            var _sut = new RegisterationCommand() { Username = username, Password = password };
            var validation = new RegisterationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeTrue();
            validatonResult.Errors.Count.Should().BeLessThan(1);
        }
    }
}
