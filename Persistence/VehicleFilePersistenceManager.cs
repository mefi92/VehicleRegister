using Core;
using Entity;
using Newtonsoft.Json;

namespace Persistence
{
    public class VehicleFilePersistenceManager : IPersistentVehicleGateway
    {
        public string GetLatestRegNumber()
        {
            string firstRegistrationNumber = "AAAA001";
            FileInfo[] Files = GetTxtFileArray();

            if (Files != null) { return Files[-1].Name.Substring(0, 7); }
            return firstRegistrationNumber;
        }

        public bool IsEngineNumberInUse(string engineNumber)
        {
            foreach (FileInfo file in GetTxtFileArray())
            {
                if (file.Name.Contains("_" + engineNumber + "."))
                    return true;                                 
            }
            return false;
        }

        public Vehicle LoadVehicle(string vehicleRegistrationNumber)
        {
            //Todo: this will return a null if loading has failed.
            vehicleRegistrationNumber = FindFileByRegistrationNumber(vehicleRegistrationNumber);
            if (vehicleRegistrationNumber != null) 
            {
                Vehicle vehicle = LoadJsonDataFromFile<Vehicle>(vehicleRegistrationNumber);
                return vehicle; 
            }
            return null;            
        }

        public void SaveVehicle(Vehicle vehicle)
        {
            string registrationNumber = FormatRegistrationNumberToRaw(vehicle.registrationNumber);
            string engineNumber = vehicle.engineNumber;
            string filePath = registrationNumber + "_" + engineNumber + ".json";
            SaveVehicleToJsonFile(filePath, vehicle);
        }

        private static string FormatRegistrationNumberToRaw(string registrationNumber)
        {
            string rawRegistrationNumber = new string(registrationNumber.Where(c => Char.IsLetterOrDigit(c)).ToArray());
            rawRegistrationNumber = rawRegistrationNumber.ToUpper();
            return rawRegistrationNumber;
        }

        private static void SaveVehicleToJsonFile(string filePath, Vehicle vehicle)
        {
            string jsonData = JsonConvert.SerializeObject(vehicle);
            File.WriteAllText(filePath, jsonData);
        }

        private T LoadJsonDataFromFile<T>(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                T result = JsonConvert.DeserializeObject<T>(jsonData);
                return result;
            }
            catch
            {
                return default(T);
            }
        }

        private static string FindFileByRegistrationNumber(string registrationNumber)
        {
            foreach (FileInfo file in GetTxtFileArray())
            {
                string fileName = file.Name;

                if (fileName != null && fileName.Length >= 7 && fileName.Substring(0, 7).Equals(registrationNumber, StringComparison.OrdinalIgnoreCase))
                {
                    return fileName;
                }
            }
            return null;
        }

        private static FileInfo[] GetTxtFileArray()
        {
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo d = new DirectoryInfo(baseDirectory);
                FileInfo[] files = d.GetFiles("*.txt").OrderBy(file => file.Name).ToArray();
                return files;
            }
            catch (Exception)
            {                
                return null;
            }
        }

    }
}
