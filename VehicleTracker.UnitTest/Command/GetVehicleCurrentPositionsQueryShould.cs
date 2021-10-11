using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Vehicle.Command;
using VehicleTracker.ApplicationServices.Vehicle.Query;
using VehicleTracker.ApplicationServices.Vehicle.Validation;
using Xunit;

namespace VehicleTracker.UnitTest.Commands
{
    public class GetVehicleCurrentPositionsQueryShould
    {
        [Fact]
        public void Should_return_error_message_given_default_value_for_vehicle_id_is_default()
        {
            //Arrange
            var _sut = new GetVehicleCurrentPositionsQuery() { VehicleId = Guid.Empty };
            var validation = new GetVehicleCurrentPositionsQueryValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeFalse();
            validatonResult.Errors.Should().NotBeEmpty();
            validatonResult.Errors[0].ErrorMessage.Should().Be("Please pass in a valid vehicle id");
        }

        [Theory]
        [InlineData("9DF7FE2E-C37F-476F-1CB9-08D95B8C096B")]
        public void Should_succeed_given_vehicle_id_is_passed(Guid vehicleId)
        {
            //Arrange

            var _sut = new GetVehicleCurrentPositionsQuery() { VehicleId = vehicleId };
            var validation = new GetVehicleCurrentPositionsQueryValidation();
            //Act
            var validatonResult = validation.Validate(_sut);
            //Assert
            validatonResult.IsValid.Should().BeTrue();
            validatonResult.Errors.Count.Should().BeLessThan(1);
        }
    }
}
