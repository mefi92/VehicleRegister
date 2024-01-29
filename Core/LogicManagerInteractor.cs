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
            string? latestRegNumber =  persistentVehicleGateway.GetLatestRegNumber();            
            Vehicle newVehicle = new RegisterNewVehicle().addNewVehicle(vehicleType, engineNumber, latestRegNumber);            
            persistentVehicleGateway.saveVehicle(newVehicle);
            presenterManager.displayMessage("Sikeres regsztráció!\nA jármű rendszáma:\n" + latestRegNumber); 
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
