using Entity;

namespace Core
{
    //ezt lehet, hogy külön választanám két interfészbe, mert teljesen elkülönül a két dolog
    public interface IPersistentVehicleGateway 
    {
        Vehicle LoadVehicle(string carRegistrationNumber);

        void SaveVehicle(Vehicle vehicle);

        string GetLatestRegNumber();

        bool IsItemInUse(string engineNumber);
    }
}
