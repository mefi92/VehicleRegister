using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Persistence;

namespace Ui
{
    public class FakeUi : IVehicleManagerPresenterOutBoundary
    {     
        
        static void Main(string[] args)
        {
            IPersistentVehicleGateway pvg = new VehicleFileSystem();
            FakeUi fakeUi = new FakeUi();
            IVehicleManagerInBoundary lmi = new LogicManagerInteractor(pvg, fakeUi);
            fakeUi.mainLoop(lmi);
        }

        public void mainLoop(IVehicleManagerInBoundary vehicleManager)
        {
            vehicleManager.RegisterNewVehicle("Volkswagen", "1Z1961206115613");           
           

        }

        public void displayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
