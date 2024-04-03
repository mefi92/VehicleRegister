using BoundaryHelper;
using Core.Mappers;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test
{
    [TestClass]
    public class VehicleMapperTest
    {
        [TestMethod]
        public void MapVehicleToResponse_ValidVehicleInput_ReturnsNoError()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                RegistrationNumber = "AAAA001",
                VehicleType = "M1",
                Make = "VW",
                Model = "GOLF",
                EngineNumber = "AA012345678910",
                MotorEmissionType = "EURO 6",
                FirstRegistrationDate = "2024.04.03",
                NumberOfSeats = 5,
                Color = "BLACK",
                MassInService = 1100,
                MaxMass = 1300,
                BrakedTrailer = 1600,
                UnbrakedTrailer = 1500,
                OwnerHash = "RANDOMHASH"
            };

            var response = new LoadVehicleDataResponse();

            // Act
            VehicleMapper.MapVehicleToResponse(vehicle, response);

            // Assert
            Assert.AreEqual(vehicle.RegistrationNumber, response.RegistrationNumber);
            Assert.AreEqual(vehicle.VehicleType, response.VehicleType);
            Assert.AreEqual(vehicle.Make, response.Make);
            Assert.AreEqual(vehicle.Model, response.Model);
            Assert.AreEqual(vehicle.EngineNumber, response.EngineNumber);
            Assert.AreEqual(vehicle.MotorEmissionType, response.MotorEmissionType);
            Assert.AreEqual(vehicle.FirstRegistrationDate, response.FirstRegistrationDate);
            Assert.AreEqual(vehicle.NumberOfSeats, response.NumberOfSeats);
            Assert.AreEqual(vehicle.Color, response.Color);
            Assert.AreEqual(vehicle.MassInService, response.MassInService);
            Assert.AreEqual(vehicle.MaxMass, response.MaxMass);
            Assert.AreEqual(vehicle.BrakedTrailer, response.BrakedTrailer);
            Assert.AreEqual(vehicle.UnbrakedTrailer, response.UnbrakedTrailer);
        }

        [TestMethod]
        public void MapResponseToVehicle_ValidResponseInput_ReturnsNoError()
        {
            // Arrange
            var response = new RegisterNewVehicleRequest
            {                
                VehicleType = "M1",
                Make = "VW",
                Model = "GOLF",
                EngineNumber = "AA012345678910",
                MotorEmissionType = "EURO 6",
                FirstRegistrationDate = "2024.04.03",
                NumberOfSeats = 5,
                Color = "BLACK",
                MassInService = 1100,
                MaxMass = 1300,
                BrakedTrailer = 1600,
                UnbrakedTrailer = 1500,                
            };

            string registrationNumber = "AAAA001";
            string ownerHash = "RANDOMHASH";
            var vehicle = new Vehicle();

            // Act
            VehicleMapper.MapResponseToVehicle(response, vehicle, registrationNumber, ownerHash);

            // Assert
            Assert.AreEqual(vehicle.RegistrationNumber, registrationNumber);
            Assert.AreEqual(vehicle.VehicleType, response.VehicleType);
            Assert.AreEqual(vehicle.Make, response.Make);
            Assert.AreEqual(vehicle.Model, response.Model);
            Assert.AreEqual(vehicle.EngineNumber, response.EngineNumber);
            Assert.AreEqual(vehicle.MotorEmissionType, response.MotorEmissionType);
            Assert.AreEqual(vehicle.FirstRegistrationDate, response.FirstRegistrationDate);
            Assert.AreEqual(vehicle.NumberOfSeats, response.NumberOfSeats);
            Assert.AreEqual(vehicle.Color, response.Color);
            Assert.AreEqual(vehicle.MassInService, response.MassInService);
            Assert.AreEqual(vehicle.MaxMass, response.MaxMass);
            Assert.AreEqual(vehicle.BrakedTrailer, response.BrakedTrailer);
            Assert.AreEqual(vehicle.UnbrakedTrailer, response.UnbrakedTrailer);
            Assert.AreEqual(vehicle.OwnerHash, ownerHash);
        }
    }
}
