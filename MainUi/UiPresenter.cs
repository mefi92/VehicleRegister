using Core;
using MainUi.MessageObjects;
using MainUi.MessageObjects.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.InteropServices.JavaScript;

namespace MainUi
{
    public class UiPresenter : IVehicleManagerPresenterOutBoundary
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
                            Console.WriteLine("\tSikeres jármű regisztráció!");
                            Console.WriteLine($"\tA jármű rendszáma {registrationNumber}.");
                        }
                        break;
                    case "load_vehicle_data":
                        
                        string a = deserializedMessage.Data.GetType().Name;
                        JObject data = deserializedMessage.Data;
                        if (data != null)
                        {
                            Console.WriteLine("\tJármű adatai");
                            Console.WriteLine("\tTípus: " +  deserializedMessage.Data.VehicleType);
                            Console.WriteLine("\tMotorszám: " +  deserializedMessage.Data.EngineNumber);
                            Console.WriteLine("\tRendszám: " +  deserializedMessage.Data.RegistrationNumber);
                        }
                        break;
                }
            }
        }
    }
}
