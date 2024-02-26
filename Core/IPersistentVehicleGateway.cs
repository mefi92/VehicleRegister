using Entity;

namespace Core
{
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
