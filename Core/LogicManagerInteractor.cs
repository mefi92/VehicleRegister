using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class LogicManagerInteractor : VehicleManagerInBoundary
    {
        private PersistentVehicleGateway persistentVehicleGateway;
        private VehicleManagerPresenterOutBoundary presenterOutBoundary;

        public LogicManagerInteractor(PersistentVehicleGateway persistentVehicleGateway, VehicleManagerPresenterOutBoundary presenterOutBoundary)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;

            this.presenterOutBoundary = presenterOutBoundary;
        }

        public void registerNewVehicle(string vehicleType, string engineNumber)
        {
            
        }
    }
}
