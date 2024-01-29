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
            OperationHandler operationHandler = new OperationHandler(uiController);
            operationHandler.OperationSelection();
        }

        public void getMessageFromApplication(string message)
        {
            Console.WriteLine(message);
        }
    }

  
}
