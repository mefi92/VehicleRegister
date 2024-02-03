using Core;
using MainUi.MessageObjects;
using MainUi.MessageObjects.Commands;
using Newtonsoft.Json;
using System;

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
            if (consoleUi != null && message != null)
            {

                var deserializedMessage = JsonConvert.DeserializeObject<dynamic>(message);

                string command = deserializedMessage.Command;

                if (deserializedMessage.Error != null)
                {
                    Console.WriteLine(deserializedMessage.Error.Message);
                    return;
                }

                switch (command)
                {
                    case "RegisterNewVehicle":
                        string registrationNumber = deserializedMessage.Data.RegistrationNumber;
                        if (registrationNumber != null)
                        {
                            Console.WriteLine("Sikeres jármű regisztráció!");
                            Console.WriteLine($"Az jármű rendszáma {registrationNumber}.");
                        }
                        break;
                }
            }
        }
    }
}
