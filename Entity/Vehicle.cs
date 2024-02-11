using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Vehicle
    {
        public string RegistrationNumber { get; }
        public string VehicleType { get; }
        public string Make { get; }
        public string Model { get; }
        public string EngineNumber { get; }
        public string MotorEmissionType { get; }
        public string FirstRegistrationDate { get; }
        public int NumberOfSeats { get; }
        public string Color { get; }
        public int MassInService { get; }
        public int MaxMass { get; }
        public int BrakedTrailer { get; }
        public int UnbrakedTrailer { get; }        
        public string OwnerHash { get; set; }

        public Vehicle(string registrationNumber, string vehicleType, string make, string model, string engineNumber, string motorEmissionType,
                        string firstRegistrationDate, int numberOfSeats, string color, int massInService, int maxMass, int brakedTrailer,
                            int unbrakedTrailer, string ownerHash)
        {
            RegistrationNumber = registrationNumber;
            VehicleType = vehicleType;
            Make = make;
            Model = model;
            EngineNumber = engineNumber;
            MotorEmissionType = motorEmissionType;
            FirstRegistrationDate = firstRegistrationDate;
            NumberOfSeats = numberOfSeats;
            Color = color;
            MassInService = massInService;
            MaxMass = maxMass;
            BrakedTrailer = brakedTrailer;
            UnbrakedTrailer = unbrakedTrailer;
            OwnerHash = ownerHash; 
        }

    }
}
