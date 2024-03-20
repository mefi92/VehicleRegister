using BoundaryHelper;
using Core.VerificationObjects;

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
                EngineNumber = "ab123456789123",
                VehicleType = "m1",
            };

            var result = RegisterNewVehicleRequestValidator.Validate(request);
            //Assert.
        }

    }
}
