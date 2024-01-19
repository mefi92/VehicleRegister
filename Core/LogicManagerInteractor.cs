using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Vehicle newVehicle = new RegisterNewVehicle().addNewVehicle(vehicleType, engineNumber);
            persistentVehicleGateway.saveVehicle(newVehicle);
            presenterManager.displayMessage("Car is registered with plate number:" + newVehicle.registrationNumber); 
        }
    }
}
