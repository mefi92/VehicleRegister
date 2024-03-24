using BoundaryHelper;
using Core.Resources;
using Core.VerificationObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace Core.Test
{
    [TestClass]
    public class RegisterNewVehicleRequestValidatorTest
    {
        [TestMethod]
        public void Validate_ValidRequest_ReturnsNoErros()
        {           
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "AB123456789123",
                VehicleType = "M1",
            };
            int expected = 0;

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(expected, result.Errors.Count);
        }

        [TestMethod]
        public void Validate_WithLowerCaseStringInputs_ShouldReturnNoErrors()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "teszt",
                AdPostalCode = "4000",
                EngineNumber = "ab123456789123",
                VehicleType = "m1",
            };

            int expected = 0;

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(expected, result.Errors.Count);
        }

        [TestMethod]
        public void Validate_InvalidFirstName_ReturnsFirstNameError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "AB123456789123",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidFirstNameNull.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_InvalidLastName_ReturnsLastNameError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "",
                AdPostalCode = "1000",
                EngineNumber = "AB123456789123",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidLastNameNull.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_InvalidPostalCode_ReturnsPostalCodeError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "10000",
                EngineNumber = "AB123456789123",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidPostalCode.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_InvalidEngineNumber_ReturnsEngineNumberError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidEngineNumber.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_InvalidVehicleType_ReturnsVehicleTypeError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "AB123456789123",
                VehicleType = "C1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidVehicleType.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_WithEmptyStringInputs_ShouldReturnFiveErrors()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "",
                LastName = "",
                AdPostalCode = "",
                EngineNumber = "",
                VehicleType = "",
            };

            int expected = 5;

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(expected, result.Errors.Count);
        }
    }
}
