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

            //itt valami nem stimmel, oda-vissza kapcsolat van az interactor és a presenter között
            presenter.SetVehicleManager(vehicleManagerInBoundary);            
            presenter.StartApplication();

            //a controller hiányzik a buliból!
        }
        
    }
}
