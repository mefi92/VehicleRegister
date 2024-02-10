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

        public VehicleRegistrationInfo(JObject vehicle)
        {
            RegistrationNumber = vehicle["RegistrationNumber"].ToString();
            VehicleType = vehicle["VehicleType"].ToString();
            Make = vehicle["Make"].ToString();
            Model = vehicle["Model"].ToString();            
            EngineNumber = vehicle["EngineNumber"].ToString();
            MotorEmissionType = vehicle["MotorEmissionType"].ToString();
            FirstRegistrationDate = vehicle["FirstRegistrationDate"].ToString();
            NumberOfSeats = Convert.ToInt32(vehicle["NumberOfSeats"].ToString());
            Color = vehicle["Color"].ToString();
            MassInService = Convert.ToInt32(vehicle["MassInService"].ToString());
            MaxMass = Convert.ToInt32(vehicle["MaxMass"].ToString());
            BrakedTrailer = Convert.ToInt32(vehicle["BrakedTrailer"].ToString());
            UnbrakedTrailer = Convert.ToInt32(vehicle["UnbrakedTrailer"].ToString());
            FirstName = vehicle["FirstName"].ToString();
            LastName = vehicle["LastName"].ToString();
            AdPostalCode = vehicle["AdPostalCode"].ToString();
            AdCity = vehicle["AdCity"].ToString();
            AdStreet = vehicle["AdStreet"].ToString();
            AdStreetNumber = vehicle["AdStreetNumber"].ToString();
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

