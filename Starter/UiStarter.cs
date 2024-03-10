using Core;
using Persistence;
using ConsoleApplication;
using Core.Interfaces;

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
            Controller controller = new Controller(consoleView, vehicleManagerInBoundary);
          
            controller.StartApplication();

        }
        
    }
}
