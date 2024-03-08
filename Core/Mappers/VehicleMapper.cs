using BoundaryHelper;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappers
{
    public static class VehicleMapper
    {
        public static void MapVehicleToResponse(Vehicle vehicle, LoadVehicleDataResponse response)
        {
            response.VehicleType = vehicle.VehicleType;
            response.RegistrationNumber = vehicle.RegistrationNumber;
            response.EngineNumber = vehicle.EngineNumber;
            response.FirstRegistrationDate = vehicle.FirstRegistrationDate;
            response.Make = vehicle.Make;
            response.Model = vehicle.Model;
            response.NumberOfSeats = vehicle.NumberOfSeats;
            response.Color = vehicle.Color;
            response.MassInService = vehicle.MassInService;
            response.MaxMass = vehicle.MaxMass;
            response.BrakedTrailer = vehicle.BrakedTrailer;
            response.UnbrakedTrailer = vehicle.UnbrakedTrailer;
            response.MotorEmissionType = vehicle.MotorEmissionType;
        }

        public static void MapResponseToVehicle(RegisterNewVehicleRequest response, Vehicle vehicle, string registrationNumber, string ownerHash)
        {
            vehicle.VehicleType = response.VehicleType;            
            vehicle.EngineNumber = response.EngineNumber;
            vehicle.FirstRegistrationDate = response.FirstRegistrationDate;
            vehicle.Make = response.Make;
            vehicle.Model = response.Model;
            vehicle.NumberOfSeats = response.NumberOfSeats;
            vehicle.Color = response.Color;
            vehicle.MassInService = response.MassInService;
            vehicle.MaxMass = response.MaxMass;
            vehicle.BrakedTrailer = response.BrakedTrailer;
            vehicle.UnbrakedTrailer = response.UnbrakedTrailer;
            vehicle.MotorEmissionType = response.MotorEmissionType;
            vehicle.RegistrationNumber = registrationNumber;
            vehicle.OwnerHash = ownerHash;
        }
    }
}
