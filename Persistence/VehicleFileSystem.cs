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
                int registrationNumber = int.Parse(reader.ReadLine().Trim());
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
                
    }
}
