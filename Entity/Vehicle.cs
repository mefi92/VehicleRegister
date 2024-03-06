
namespace Entity
{
    public class Vehicle
    {
        public string RegistrationNumber { set; get; }
        public string VehicleType { set; get; }
        public string Make { set; get; }
        public string Model { set; get; }
        public string EngineNumber { set; get; }
        public string MotorEmissionType { set; get; }
        public string FirstRegistrationDate { set; get; }
        public int NumberOfSeats { set; get; }
        public string Color { set; get; }
        public int MassInService { set; get; }
        public int MaxMass { set; get; }
        public int BrakedTrailer { set; get; }
        public int UnbrakedTrailer { set; get; }        
        public string OwnerHash { set; get;  }

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
