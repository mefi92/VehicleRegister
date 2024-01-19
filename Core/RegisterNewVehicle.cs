using Entity;

namespace Core
{
    public class RegisterNewVehicle
    {
        public Vehicle addNewVehicle(string vehicleType, string engineNumber)
        {
            return new Vehicle(vehicleType: vehicleType, vehicleEngineNumber: engineNumber, vehicleRegistrationNumber: getNextRegistrationNumber());
        }

        private int getNextRegistrationNumber() // Todo: generate genuine plate numbers 
        {
            Random rnd = new Random();
            return rnd.Next();
        }
            
    }
}
