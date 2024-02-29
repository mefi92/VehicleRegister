using Core;
using Entity;
using Newtonsoft.Json;

namespace Persistence
{
    public class VehicleFilePersistenceManager : IPersistentVehicleGateway
    {
        public string GetLatestRegNumber()
        {
            string firstRegistrationNumber = "AAAA000";
            FileInfo[] Files = GetTxtFileArray();

            if (Files != null)
            {
                for (int i = 1; i < Files.Length; i++)
                {
                    if (Files[Files.Length - i].Name.Length == 26)
                    {
                        return Files[Files.Length - i].Name.Substring(0, 7);
                    }
                }
            }
            return firstRegistrationNumber;
        }

        public bool IsItemInUse(string item)
        {
            foreach (FileInfo file in GetTxtFileArray())
            {
                if (item.Length == 14) // Eninge number length 14
                {
                    if (file.Name.Contains("_" + item + "."))
                        return true;
                }
                else if (item.Length == 32) // Name hash length 32
                {
                    if (file.Name == $"{item}.txt")
                        return true;
                }
                                                 
            }
            return false;
        }

        public Vehicle LoadVehicle(string vehicleRegistrationNumber)
        {            
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
            string registrationNumber = new RegistrationNumberFormatter().CleanRegistrationNumber(vehicle.RegistrationNumber);
            string engineNumber = vehicle.EngineNumber;
            string filePath = registrationNumber + "_" + engineNumber + ".txt";
            SaveObjectToTextFile(filePath, vehicle);
        }

        private static void SaveObjectToTextFile<T>(string filePath, T inputObject)
        {
            // itt esetleg dobni egy exceptiont, ha sikertelen a mentés.
            // igen, a hibakezelés biztos hiányzik
            string jsonData = JsonConvert.SerializeObject(inputObject);
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

        public Person LoadPerson(string hashNumber)
        {
            if (hashNumber != null)
            {
                Person person = LoadJsonDataFromFile<Person>($"{hashNumber}.txt");
                return person;
            }
            return null;
        }

        public void SavePerson(Person person)
        {
            string filePath = person.Hash + ".txt";
            SaveObjectToTextFile<Person>(filePath, person);
        }
    }
}
