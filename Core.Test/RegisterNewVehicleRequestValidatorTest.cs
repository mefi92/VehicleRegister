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
        public static IEnumerable<object[]> ValidVehicleTypes
        {
            get
            {
                return new[]
                {
                    // "M1", "N1", "N2", "N3", "O1", "O2", "O3", "L3E"
                    new object[] { "m1", 0 },
                    new object[] { "M1", 0 },
                    new object[] { "n1", 0 },
                    new object[] { "N1", 0 },
                    new object[] { "n2", 0 },
                    new object[] { "N2", 0 },
                    new object[] { "n3", 0 },
                    new object[] { "N3", 0 },
                    new object[] { "o1", 0 },
                    new object[] { "O1", 0 },
                    new object[] { "o2", 0 },
                    new object[] { "O2", 0 },
                    new object[] { "o3", 0 },
                    new object[] { "O3", 0 },                    
                    new object[] { "l3e", 0 },
                    new object[] { "L3E", 0 }
                };
            }
        }

        [DynamicData(nameof(ValidVehicleTypes))]
        [TestMethod]
        public void Validate_AllValidVehiclesTypeFromCollection_ReturnsNoError(string vehicleType, int expected)
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "AB123456789123",
                VehicleType = vehicleType,
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(expected, result.Errors.Count);

        }

        [TestMethod]
        public void Validate_ValidRequest_ReturnsNoError()
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
        public void Validate_InvalidFirstName_ReturnsInvalidFirstNameNull()
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

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(ErrorCollection.InvalidFirstNameNull.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_ToLongFirstName_ReturnsInvalidFirstNameLengthError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "aaaaabbbbbcccccdddddeeeeefffffgggg",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "AB123456789123",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(ErrorCollection.InvalidFirstNameLength.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_NonAlphabeticFirstName_ReturnsFirstNameError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "123%+",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "AB123456789123",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(ErrorCollection.InvalidFirstNameChar.ErrorCode, result.Errors[0].ErrorCode);
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

            Assert.AreEqual(1, result.Errors.Count);
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

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(ErrorCollection.InvalidPostalCode.ErrorCode, result.Errors[0].ErrorCode);            
        }

        [TestMethod]
        public void Validate_InvalidPostalCodeStartsWithZero_ReturnsPostalCodeError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "0100",
                EngineNumber = "AB123456789123",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(ErrorCollection.InvalidPostalCode.ErrorCode, result.Errors[0].ErrorCode);            
        }

        [TestMethod]
        public void Validate_EmptyStringEngineNumber_ReturnsEngineNumberError()
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
        public void Validate_EngineNumberStartsWithThreeLetters_ReturnsEngineNumberError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "ABC12345678910",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidEngineNumber.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_EngineNumberOnlyNumbers_ReturnsEngineNumberError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "00123456789123",
                VehicleType = "M1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidEngineNumber.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_EngineNumberCorrectLengthButCharsAtTheEnd_ReturnsEngineNumberError()
        {
            var request = new RegisterNewVehicleRequest
            {
                FirstName = "teszt",
                LastName = "kalman",
                AdPostalCode = "1000",
                EngineNumber = "123456789123BB",
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
