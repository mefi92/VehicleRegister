using BoundaryHelper;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.VerificationObjects
{
    public class VehicleRegistrationInfo
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
        public string FirstName { get; }
        public string LastName { get; }
        public string AdPostalCode { get; }
        public string AdCity { get; }
        public string AdStreet { get; }
        public string AdStreetNumber { get; }

        private List<int> errorCodes = new List<int>();

        public VehicleRegistrationInfo(RegisterNewVehicleRequest vehicle)
        {
            VehicleType = vehicle.VehicleType;
            Make = vehicle.Make;
            Model = vehicle.Model;            
            EngineNumber = vehicle.EngineNumber;
            MotorEmissionType = vehicle.MotorEmissionType;
            FirstRegistrationDate = vehicle.FirstRegistrationDate;
            NumberOfSeats = vehicle.NumberOfSeats;
            Color = vehicle.Color;
            MassInService = vehicle.MassInService;
            MaxMass = vehicle.MaxMass;
            BrakedTrailer = vehicle.BrakedTrailer;
            UnbrakedTrailer = vehicle.UnbrakedTrailer;
            FirstName = vehicle.FirstName;
            LastName = vehicle.LastName;
            AdPostalCode = vehicle.AdPostalCode;
            AdCity = vehicle.AdCity;
            AdStreet = vehicle.AdStreet;
            AdStreetNumber = vehicle.AdStreetNumber;
        }       

        private static bool IsVehicleType(string vehicleType)
        {
            //"M1", "N1", "N2", "N3", "O1", "O2", "O3", "L3E"
            string[] validInputs = { "M1", "N1", "N2", "N3", "O1", "O2", "O3", "L3E" };
            return Array.Exists(validInputs, validInput => string.Equals(validInput, vehicleType, StringComparison.OrdinalIgnoreCase));           
        }

        private static bool IsRegistrationNumber(string registrationNumber)
        {
            //ABCD012
            string pattern = @"^[A-Z]{4}\d{3}$";
            return PatternChecker(pattern, registrationNumber);
        }

        private static bool IsEngineNumber(string engineNumber)
        {
            //IK260220055445
            string pattern = @"^[A-Z]{2}\d{12}$";
            return PatternChecker(pattern, engineNumber);
        }

        private static bool PatternChecker(string pattern, string data)
        {
            return !string.IsNullOrEmpty(data) && Regex.IsMatch(data, pattern);
        }

        public List<int> ValidateVehiceDataFormat()
        {
            if (!IsVehicleType(VehicleType))
            {                
                errorCodes.Add(201);
                
            }
            if (!IsEngineNumber(EngineNumber))
            {
                errorCodes.Add(202);                
            }

            return errorCodes;
            }

        public List<int> ValidateRegistrationNumberFormat()
        {
            if (!IsRegistrationNumber(RegistrationNumber))
            {
                errorCodes.Add(203);
            }

            return errorCodes;
        }
    }
}

