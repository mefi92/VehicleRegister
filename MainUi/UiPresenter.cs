using Core;

namespace MainUi
{
    public class UiPresenter : VehicleManagerPresenterOutBoundary
    {
        private ConsoleUi consoleUi;

        public void setConsoleUI(ConsoleUi consoleUi)
        {
            this.consoleUi = consoleUi;
        }

        public void displayMessage(string message)
        {
            if (consoleUi != null)
            {
                if (message != null)
                {
                    consoleUi.getMessageFromApplication(message);
                }
            }
        }
    }
}
