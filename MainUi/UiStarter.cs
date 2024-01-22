using Core;
using Persistence;

namespace MainUi
{
    public class UiStarter
    {
        static void Main(string[] args)
        {
            UiPresenter uiPresenter = new UiPresenter();            
            PersistentVehicleGateway persistentVehicleGateway = new VehicleFileSystem();
            VehicleManagerInBoundary vehicleManagerInBoundary = new LogicManagerInteractor(persistentVehicleGateway, uiPresenter);
            ConsoleUi consoleUi = new ConsoleUi(vehicleManagerInBoundary);
            uiPresenter.setConsoleUI(consoleUi);
            consoleUi.mainLoop();

        }
    }
}
