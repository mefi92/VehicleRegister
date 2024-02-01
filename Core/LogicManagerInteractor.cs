using Core.MessageObjects;
using Entity;

namespace Core
{
    public class LogicManagerInteractor : VehicleManagerInBoundary
    {
        private PersistentVehicleGateway persistentVehicleGateway;
        private VehicleManagerPresenterOutBoundary presenterManager;

        public LogicManagerInteractor(PersistentVehicleGateway persistentVehicleGateway, VehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;

            this.presenterManager = presenterManager;
        }

        public void registerNewVehicle(string vehicleType, string engineNumber)
        {
            RegisterNewVehicleManager registerManager = new RegisterNewVehicleManager(persistentVehicleGateway, presenterManager);
            registerManager.RegisterNewVehicle(vehicleType, engineNumber);
        }

        

        public void LoadVehicle(string registrationNumber)
        {
            
            Vehicle vehicle = persistentVehicleGateway.loadVehicle(registrationNumber);

            if (vehicle == null)
            {
                presenterManager.displayMessage("A megadott rendszám nem létezik!\nPróbálja újra.");
                return;
            }

            presenterManager.displayMessage("Jármű márkája: " + vehicle.type + "\nMotor száma: " + vehicle.engineNumber + "\nRendszáma: " + vehicle.registrationNumber);

        }
    }
}
