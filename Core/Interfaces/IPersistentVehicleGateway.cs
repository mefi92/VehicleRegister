using Entity;

namespace Core.Interfaces
{
    public interface IPersistentVehicleGateway
    {
        Vehicle LoadVehicle(string carRegistrationNumber);

        void SaveVehicle(Vehicle vehicle);

        string GetLatestRegNumber();

        bool IsItemInUse(string engineNumber);
    }
}
