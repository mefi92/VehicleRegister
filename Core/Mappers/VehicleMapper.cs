using BoundaryHelper;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappers
{
    public class VehicleMapper
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
    }
}
