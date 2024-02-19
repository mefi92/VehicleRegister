
namespace Core
{
    public interface IVehicleManagerInBoundary
    { 
        void ProcessTrafficMessage(string message);

        void LoadVehicleData(string request);

        void RegisterNewVehicle(string request);
    }
}
