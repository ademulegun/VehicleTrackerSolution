using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Vehicle.Command;
using VehicleTracker.ApplicationServices.Vehicle.Validation;
using Xunit;

namespace VehicleTracker.UnitTest.Command
{
    public class VehicleRegisterationCommandShould
    {
        [Fact]
        public void Should_return_error_message_given_empty_string_is_passed_for_username_password_and_device_number()
        {
            //Arrange
            var _sut = new VehicleRegisterationCommand() { Username = "string", Password = "string", DeviceNumber = "string" };
            var validation = new VehicleRegisterationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Username value can not be string");
            validatonResult.Errors[1].ErrorMessage.Should().Be("Device number value can not be string");
            validatonResult.Errors[2].ErrorMessage.Should().Be("Password value can not be string");
        }

        [Fact]
        public void Should_return_error_message_given_null_or_empty__is_passed_for_username_and_passsword()
        {
            //Arrange
            var _sut = new VehicleRegisterationCommand() { Username = "", Password = "", DeviceNumber = "" };
            var validation = new VehicleRegisterationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Username must be provided");
            validatonResult.Errors[1].ErrorMessage.Should().Be("DeviceNumber must be provided");
            validatonResult.Errors[2].ErrorMessage.Should().Be("Password must be provided");
        }

        [Theory]
        [InlineData("babafemi.ibitolu@yahoo.com", "1234", "123456789")]
        public void Should_succeed_given_username_and_passsword_is_passed(string username, string password, string deviceNumber)
        {
            //Arrange

            var _sut = new VehicleRegisterationCommand() { Username = username, Password = password, DeviceNumber = deviceNumber };
            var validation = new VehicleRegisterationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeTrue();
            validatonResult.Errors.Count.Should().BeLessThan(1);
        }
    }
}
