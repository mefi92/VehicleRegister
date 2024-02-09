using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.VerificationObjects
{
    internal class UnvalidatedVehicle
    {
        public string VehicleType { get; set; }
        public string RegistrationNumber { get; set; }
        public string EngineNumber { get; set; }

        private List<int> errorCodes = new List<int>();

        public UnvalidatedVehicle(JObject vehicle)
        {
            VehicleType = vehicle["VehicleType"].ToString();
            RegistrationNumber = vehicle["RegistrationNumber"].ToString();
            EngineNumber = vehicle["EngineNumber"].ToString();
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
            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.AppendLine("Kérlek javítsd a következőket:");
            bool valid = true;

            if (!IsRegistrationNumber(RegistrationNumber))
            {

                valid = false;
                errorCodes.Add(203);                
                //stringBuilder.AppendLine("\t-Hibás rendszám formátum!");
                //stringBuilder.AppendLine("\t Példa: ABCD012\n");
            }

            /*if (valid)
            {
                return string.Empty;
            }
            return stringBuilder.ToString();*/

            return errorCodes;
        }
    }
}

