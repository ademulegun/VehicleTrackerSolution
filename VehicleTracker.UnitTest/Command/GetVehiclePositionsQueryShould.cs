using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Vehicle.Query;
using VehicleTracker.ApplicationServices.Vehicle.Validation;
using Xunit;

namespace VehicleTracker.UnitTest.Command
{
    public class GetVehiclePositionsQueryShould
    {
        [Fact]
        public void Return_error_message_given_default_value_for_vehicle_id_is_passed_and_from_and_to_are_null()
        {
            //Arrange
            var _sut = new GetVehiclePositionsQuery() { VehicleId = Guid.Empty, From = "string", To = "string" };
            var validation = new GetVehiclePositionsQueryValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Please pass in a valid vehicle id");
            validatonResult.Errors[1].ErrorMessage.Should().Be("From value can not be string"); 
            validatonResult.Errors[2].ErrorMessage.Should().Be("To value can not be string");
        }

        [Fact]
        public void Return_error_message_given_default_value_for_vehicle_id_is_passed_and_null_is_pass_to_from_and_null_is_pass_to_to()
        {
            //Arrange
            var _sut = new GetVehiclePositionsQuery() { VehicleId = Guid.Empty, From = "", To = "" };
            var validation = new GetVehiclePositionsQueryValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Please pass in a valid vehicle id");
            validatonResult.Errors[1].ErrorMessage.Should().Be("From date must be provided");
            validatonResult.Errors[2].ErrorMessage.Should().Be("To date must be provided");
        }

        [Theory]
        [InlineData("9DF7FE2E-C37F-476F-1CB9-08D95B8C096B", "2021-08-10", "2021-08-10")]
        public void Succeed_given_vehicle_id_is_passed_from_and_to(Guid vehicleId, string from, string to)
        {
            //Arrange
            var _sut = new GetVehiclePositionsQuery() { VehicleId = vehicleId, From=from, To = to };
            var validation = new GetVehiclePositionsQueryValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeTrue();
            validatonResult.Errors.Count.Should().BeLessThan(1);
        }
    }
}
