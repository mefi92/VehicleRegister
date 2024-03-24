using BoundaryHelper;
using Core.Resources;
using Core.VerificationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test
{
    [TestClass]
    public class LoadVehicleDataRequestValidatorTest
    {
        [TestMethod]
        public void Validate_WithUpperCaseValidInput_ReturnsNoErros()
        {
            // arrange
            var request = new LoadVehicleDataRequest
            {                
                RegistrationNumber = "AAAA001"                
            };
            
            int expected = 0;

            // act
            var result = LoadVehicleDataRequestValidator.Validate(request);

            // assert
            Assert.AreEqual(expected, result.Errors.Count);
        }

        [TestMethod]
        public void Validate_WithLowerCaseValidInput_ReturnsNoErros()
        {
            var request = new LoadVehicleDataRequest
            {
                RegistrationNumber = "aaaa001"
            };
            int expected = 0;

            var result = LoadVehicleDataRequestValidator.Validate(request);

            Assert.AreEqual(expected, result.Errors.Count);
        }

        [TestMethod]
        public void Validate_WithEmptyString_ReturnsInvalidRegistrationNumber()
        {
            var request = new LoadVehicleDataRequest
            {
                RegistrationNumber = ""
            };

            var result = LoadVehicleDataRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidRegistrationNumber.ErrorCode, result.Errors[0].ErrorCode);
        }

        [TestMethod]
        public void Validate_WithNullInput_ReturnsInvalidRegistrationNumber()
        {
            var request = new LoadVehicleDataRequest
            {
                RegistrationNumber = null
            };

            var result = LoadVehicleDataRequestValidator.Validate(request);

            Assert.AreEqual(ErrorCollection.InvalidRegistrationNumber.ErrorCode, result.Errors[0].ErrorCode);
        }
    }
}
