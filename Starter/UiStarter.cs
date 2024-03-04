using Core;
using Persistence;
using ConsoleApplication;

namespace Starter
{
    public class UiStarter
    {

        static void Main(string[] args)
        {
            ConsoleView consoleView = new ConsoleView();

            IPersistentVehicleGateway persistentVehicleGateway = new VehicleFilePersistenceManager();
            IPersistentPersonGateway persistentPersonGateway = new PersonFilePersistenceManager();
            Presenter presenter = new Presenter(consoleView);            
            IVehicleManagerInBoundary vehicleManagerInBoundary = new LogicManagerInteractor(persistentVehicleGateway, persistentPersonGateway, presenter);  

            //itt valami nem stimmel, oda-vissza kapcsolat van az interactor és a presenter között
            presenter.SetVehicleManager(vehicleManagerInBoundary);            
            presenter.StartApplication();

            //a controller hiányzik a buliból!
        }
        
    }
}
