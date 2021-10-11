using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Vehicle.Command;
using VehicleTracker.ApplicationServices.Vehicle.Validation;
using Xunit;

namespace VehicleTracker.UnitTest.Commands
{
    public class AddVehicleLocationCommandShould
    {
        [Fact]
        public void Should_return_error_message_given_default_value_for_latitude_and_longitude_is_string()
        {
            //Arrange
            var _sut = new AddVehicleLocationCommand() { Latitude = "string", Longitude = "string" };
            var validation = new AddVehicleLocationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Latitude can not be string");
            validatonResult.Errors[1].ErrorMessage.Should().Be("Longitude can not be string");
        }

        [Fact]
        public void Should_return_error_message_given_null_or_empty_is_passed_for_latitude_and_longitude()
        {
            //Arrange
            var _sut = new AddVehicleLocationCommand() { Longitude = "", Latitude = "" };
            var validation = new AddVehicleLocationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Latitude must be provided");
            validatonResult.Errors[1].ErrorMessage.Should().Be("Longitude must be provided");
        }

        [Fact]
        public void Should_return_error_message_given_user_id_is_null()
        {
            //Arrange
            var _sut = new AddVehicleLocationCommand() { UserId = "" };
            var validation = new AddVehicleLocationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Latitude must be provided");
            validatonResult.Errors[1].ErrorMessage.Should().Be("Longitude must be provided");
            validatonResult.Errors[2].ErrorMessage.Should().Be("User id can not be null or empty");
        }

        [Theory]
        [InlineData("1.23456", "1.234", "9DF7FE2E-C37F-476F-1CB9-08D95B8C096B")]
        public void Should_succeed_given_username_and_passsword_is_passed(string longitude, string latitude, string userId)
        {
            //Arrange

            var _sut = new AddVehicleLocationCommand() { Longitude = longitude, Latitude = latitude, UserId = userId };
            var validation = new AddVehicleLocationCommandValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeTrue();
            validatonResult.Errors.Count.Should().BeLessThan(1);
        }
    }
}
