using Core;
using Persistence;
using ConsoleUi;

namespace Starter
{
    public class UiStarter
    {

        static void Main(string[] args)
        {
            ConsoleView consoleView = new ConsoleView();
            Model model = new Model();
            Presenter presenter = new Presenter(model, consoleView);
            IPersistentVehicleGateway persistentVehicleGateway = new VehicleFilePersistenceManager();
            IVehicleManagerInBoundary vehicleManagerInBoundary = new LogicManagerInteractor(persistentVehicleGateway, presenter);

            presenter.SetVehicleManager(vehicleManagerInBoundary);            
            presenter.StartApplication();
        }
        
    }
}
