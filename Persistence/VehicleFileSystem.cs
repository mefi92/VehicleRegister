using Core;
using Entity;
using System.IO.Enumeration;

namespace Persistence
{
    public class VehicleFileSystem : PersistentVehicleGateway
    {
        private string fileType = ".txt";

        public Vehicle loadVehicle(string carRegistrationNumber)
        {
            string fileName = carRegistrationNumber + fileType;
            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open);
                StreamReader reader = new StreamReader(stream);


                string vehicleType = reader.ReadLine().Trim();
                string engineNumber = reader.ReadLine().Trim();
                string registrationNumber = reader.ReadLine().Trim();
                reader.Close();

                Vehicle vehicle = new Vehicle(vehicleType, registrationNumber, engineNumber);
                return vehicle;
            }
            catch 
            {
                return null;
            }
        }

        public void saveVehicle(Vehicle vehicle)
        {
            string fileName = vehicle.registrationNumber + fileType;

            try
            {
                FileStream stream = File.Open(fileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(stream);
                writer.Write("Vehicle brand: " + vehicle.type);
                writer.Write("\n");
                writer.Write("Eninge number: " + vehicle.engineNumber);
                writer.Write("\n");
                writer.Write("Registration number: " + vehicle.registrationNumber.ToString());
                writer.Write("\n");
                writer.Close();
            }
            catch
            {
                throw new Exception();
            }
            
        }

        public string? GetLatestRegNumber()
        {
            string? latestPlateText = null;
            int highestWeigth = 0;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo d = new DirectoryInfo(baseDirectory);
            FileInfo[] Files = d.GetFiles("*.txt");

            foreach (FileInfo file in Files)
            {
                int weigth = GetPlateValue(file.Name);

                if (weigth > highestWeigth) { highestWeigth = weigth; latestPlateText = file.Name; }
            }

            return latestPlateText;
        }

        public static int GetPlateValue(string plateNumber)
        {
            int finalValue = 0;

            string secondPart = plateNumber.Substring(4, 3);
            secondPart = secondPart.TrimStart('0');
            finalValue += Int32.Parse(secondPart);

            string firstPart = plateNumber.Substring(0, 4);

            int[] multipliers = [10000, 1000, 100, 10];

            for (int i = 0; i < firstPart.Length; i++)
            {
                finalValue += firstPart[i] * multipliers[i];
            }

            return finalValue;
        }

    }
}
