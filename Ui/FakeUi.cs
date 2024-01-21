using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Persistence;

namespace Ui
{
    public class FakeUi : VehicleManagerPresenterOutBoundary
    {     
        
        static void Main(string[] args)
        {
            PersistentVehicleGateway pvg = new VehicleFileSystem();
            FakeUi fakeUi = new FakeUi();
            VehicleManagerInBoundary lmi = new LogicManagerInteractor(pvg, fakeUi);
            fakeUi.mainLoop(lmi);
        }

        public void mainLoop(VehicleManagerInBoundary vehicleManager)
        {
            vehicleManager.registerNewVehicle("Volkswagen", "1Z1961206115613");
            Console.WriteLine("Melyik autóhoz adjuk az új tulajdonost?");
            Console.WriteLine("Kérem a rendszámot:");
            //int plateNumber = Convert.ToInt32(Console.ReadLine());

        }

        public void displayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
