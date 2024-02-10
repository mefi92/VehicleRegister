﻿

using MainUi.MessageObjects.Commands;
using System.Runtime.InteropServices;

namespace MainUi
{
    public class OperationHandler
    {
        private UiController uiController;

        public OperationHandler(UiController uiController)
        {
            this.uiController = uiController;
        }

        public void OperationSelection()
        {
            bool isSelected = false;
            while (!isSelected)
            {
                Console.WriteLine("Válassz tevékenységet!\r\nÚj jármű regisztrálása (r), adatlekérdezés rendszám alapján (l),  kilépés (q):");
                string input = Console.ReadLine();
                isSelected = SelectAction(input);
            }
        }

        private bool SelectAction(string input)
        {
            bool isSelected = false;
            switch (input.ToLower())
            {
                case "r":
                    RegisterNewVehicleInput();
                    break;
                case "l":
                    LoadVehicleDataInput();
                    break;
                case "q":
                    isSelected = QuitProgram();
                    break;
                default:
                    break;
            }

            return isSelected;
        }

        public void RegisterNewVehicleInput()
        {
            RegisterNewVehicleCommand vehicleParameters = new RegisterNewVehicleCommand();

            Console.WriteLine("Adja meg következő adatok");

            GatherPersonalDetails(vehicleParameters);

            GatherAddressDetails(vehicleParameters);

            GatherVehicleDetails(vehicleParameters);

            uiController.RegisterVehicle(vehicleParameters);
        }
        
        public void LoadVehicleDataInput()
        {
            Console.WriteLine("Írja be a rendszámot a következő formátumba: AAAA123");
            string plateNumber = Console.ReadLine().ToUpper();
            uiController.LoadVehicle(plateNumber);
        }

        private static bool QuitProgram()
        {
            Console.WriteLine("A program leáll.");
            return true;
        }

        private void GatherPersonalDetails(RegisterNewVehicleCommand vehicleParameters)
        {
            Console.WriteLine("\nSzemélyes adatok");            
            vehicleParameters.LastName = GetInput("Vezetéknév");
            vehicleParameters.FirstName = GetInput("Keresztnév");
        }

        private void GatherAddressDetails(RegisterNewVehicleCommand vehicleParameters)
        {
            Console.WriteLine("\nLakcím adatok");
            vehicleParameters.AdPostalCode = GetInput("Irányítószám");
            vehicleParameters.AdCity = GetInput("Város");
            vehicleParameters.AdStreet = GetInput("Utca");
            vehicleParameters.AdStreetNumber = GetInput("Házszám");
        }
        private void GatherVehicleDetails(RegisterNewVehicleCommand vehicleParameters)
        {
            Console.WriteLine("\nJármű adatok");
            vehicleParameters.VehicleType = GetInput("Kategória");
            vehicleParameters.Make = GetInput("Gyártmány");
            vehicleParameters.Model = GetInput("Típus");
            vehicleParameters.EngineNumber = GetInput("Motorszám");
            vehicleParameters.MotorEmissionType = GetInput("Környezetvédelmi osztályba sorolás");
            vehicleParameters.FirstRegistrationDate = GetInput("Első nyilvántartásba vétel időpontja");
            vehicleParameters.NumberOfSeats = GetIntegerInput("Ülések száma");
            vehicleParameters.Color = GetInput("Szín");
            vehicleParameters.MassInService = GetIntegerInput("Saját tömeg");
            vehicleParameters.MaxMass = GetIntegerInput("Össztömeg");
            vehicleParameters.BrakedTrailer = GetIntegerInput("Fékezett vontatmány");
            vehicleParameters.UnbrakedTrailer = GetIntegerInput("Fékezetlen vontatmány");
        }

        private string GetInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine().ToUpper();
        }

        private int GetIntegerInput(string prompt)
        {
            int result;
            string input;
            bool isValid;

            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();
                isValid = int.TryParse(input, out result);

                if (!isValid)
                {
                    Console.WriteLine("Hibás adat.");
                }

            } while (!isValid);

            return result;
        }
    }
}
