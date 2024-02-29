using Entity;

namespace Core
{
    //ezt lehet, hogy külön választanám két interfészbe, mert teljesen elkülönül a két dolog
    public interface IPersistentVehicleGateway 
    {
        Vehicle LoadVehicle(string carRegistrationNumber);

        void SaveVehicle(Vehicle vehicle);

        Person LoadPerson(string hashNumber);

        void SavePerson(Person person);

        string GetLatestRegNumber();

        bool IsItemInUse(string engineNumber);
    }
}
