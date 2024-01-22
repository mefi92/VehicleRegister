using Core;

namespace MainUi
{
    public class ConsoleUi
    {
        private UiController uiController;

        public ConsoleUi(VehicleManagerInBoundary vehicleManagerInBoundary)
        {
            uiController = new UiController(vehicleManagerInBoundary);
        }

        public void mainLoop()
        {
            uiController.addNewVehicle("VW", "1J123456789");
        }

        public void getMessageFromApplication(string message)
        {
            Console.WriteLine(message);
        }
    }

  
}
