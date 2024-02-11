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
            if (consoleUi == null && message == null)
            {
                return;
            }

            dynamic deserializedMessage = JsonConvert.DeserializeObject<dynamic>(message);

            HandleError(deserializedMessage);
            ProcessCommand(deserializedMessage);
            
        }

        private void HandleError(dynamic deserializedMessage)
        {
            if (deserializedMessage.Error != null)
            {
                Console.WriteLine($"\n{deserializedMessage.Error.Message}");
            }
        }

        private void ProcessCommand(dynamic deserializedMessage)
        {
            string command = deserializedMessage.Command;

            switch (command)
            {
                case CommandConstants.RegisterNewVehicle:
                    ProcessRegisterNewVehicle(deserializedMessage);
                    break;
                case CommandConstants.LoadVehicleData:
                    ProcessLoadVehicleData(deserializedMessage);
                    break;
            }
        }

        private void ProcessRegisterNewVehicle(dynamic deserializedMessage)
        {
            string registrationNumber = deserializedMessage.Data?.RegistrationNumber;
            if (!string.IsNullOrEmpty(registrationNumber))
            {
                Console.WriteLine($"\nSikeres jármű regisztráció!");
                Console.WriteLine($"A jármű rendszáma {registrationNumber}\n");
            }
        }

        private void ProcessLoadVehicleData(dynamic deserializedMessage)
        {
            JObject data = deserializedMessage.Data;
            if (data != null)
            {
                Console.WriteLine("\nJármű adatok");
                Console.WriteLine("--------------");

                PrintData("Rendszám", data["RegistrationNumber"].ToString());
                PrintData("Kategória", data["VehicleType"].ToString());
                PrintData("Gyártmány", data["Make"].ToString());
                PrintData("Típus", data["Model"].ToString());
                PrintData("Motorszám", data["EngineNumber"].ToString());
                PrintData("Környezetvédelmi osztályba sorolás", data["MotorEmissionType"].ToString());
                PrintData("Ülések száma", data["NumberOfSeats"].ToString());
                PrintData("Szín", data["Color"].ToString());
                PrintData("Saját tömeg", data["MassInService"].ToString());
                PrintData("Össztömeg", data["MaxMass"].ToString());
                PrintData("Fékezett vontatmány", data["BrakedTrailer"].ToString());
                PrintData("Fékezetlen vontatmány", data["UnbrakedTrailer"].ToString());

                Console.WriteLine();
            }
        }

        private void PrintData(string prompt, string data)
        {
            Console.WriteLine($"{prompt}: {data}");
        }

        public static class CommandConstants
        {
            public const string RegisterNewVehicle = "RegisterNewVehicle";
            public const string LoadVehicleData = "load_vehicle_data";
        }
    }
}
