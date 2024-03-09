namespace Core.Interfaces
{
    public interface IVehicleManagerInBoundary
    {
        void LoadVehicleData(string request);

        void RegisterNewVehicle(string request);
    }
}
