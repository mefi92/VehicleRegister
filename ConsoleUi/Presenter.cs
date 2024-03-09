using BoundaryHelper;
using ConsoleApplication.Verification;
using System.Text.RegularExpressions;
using Core.Interfaces;

namespace ConsoleApplication
{
    public class Presenter : IVehicleManagerPresenterOutBoundary
    {
        private ConsoleView view;
        private IVehicleManagerInBoundary vehicleManager;

        public Presenter(ConsoleView view)
        {
            this.view = view;
        }

        public void SetVehicleManager(IVehicleManagerInBoundary vehicleManager)
        {
            this.vehicleManager = vehicleManager;
        }

        public void StartApplication()
        {
            bool isExiting = false;

            while (!isExiting)
            {
                view.DisplayMenu();
                string input = view.GetUserInput();
                isExiting = OperationSelection(input);
            }
        }

        private bool OperationSelection(string input)
        {
            switch (input.ToLower())
            {
                case "r":                    
                    view.DisplayMessage("Új jármű regisztrálása.");
                    RegisterNewVehicleInput();
                    return false;

                case "l":                    
                    view.DisplayMessage("Jármű adatai rendszám alapján.");
                    LoadVehicleDataInput();
                    return false;

                case "q":
                    view.DisplayMessage("Kilépés az applikációból.");
                    return true;

                default:
                    view.DisplayMessage("Helytelen választás, próbáld újra!");
                    return false;
            }
        }

        public void RegisterNewVehicleInput()
        {
            RegisterNewVehicleRequest vehicleParameters = new RegisterNewVehicleRequest();

            view.DisplayMessage("Adja meg következő adatokat");

            GatherPersonalDetails(vehicleParameters);

            GatherAddressDetails(vehicleParameters);

            GatherVehicleDetails(vehicleParameters);

            vehicleManager.RegisterNewVehicle(JsonHandler.Serialize(vehicleParameters));
        }

        public void LoadVehicleDataInput()
        {
            LoadVehicleDataRequest vehicleParameters = new LoadVehicleDataRequest();

            string registrationNumber = GetVerifiedInput("Írja be a rendszámot a következő formátumban [AAAA123]", IsValidRegistrationNumber);            
            vehicleParameters.RegistrationNumber = registrationNumber;
            vehicleManager.LoadVehicleData(JsonHandler.Serialize(vehicleParameters));
        }

        public void DisplayRegistrationResult(string registrationResultResponse)
        {
            RegisterNewVehicleResponse registerNewVehicleResponse = JsonHandler.Deserialize<RegisterNewVehicleResponse>(registrationResultResponse);
            
            if (registerNewVehicleResponse.Error != null)
            {
                view.DisplayMessage(registerNewVehicleResponse.Error.Message);
                return;
            }

            view.DisplayVehicleRegistration(registerNewVehicleResponse.RegistrationNumber);
        }

        private void GatherPersonalDetails(RegisterNewVehicleRequest vehicleParameters)
        {
            view.DisplayMessage("Személyes adatok");
            view.DisplayMessage("-----------------");
            vehicleParameters.LastName = GetVerifiedInput("Vezetéknév", IsValidName);
            vehicleParameters.FirstName = GetVerifiedInput("Keresztnév", IsValidName);
        }

        private void GatherAddressDetails(RegisterNewVehicleRequest vehicleParameters)
        {
            view.DisplayMessage("Lakcím adatok");
            view.DisplayMessage("-------------");
            vehicleParameters.AdPostalCode = GetVerifiedInput("Irányítószám", IsValidPostalCode);
            vehicleParameters.AdCity = GetVerifiedInput("Város", IsValidName);
            vehicleParameters.AdStreet = GetVerifiedInput("Utca", IsValidName);
            vehicleParameters.AdStreetNumber = GetVerifiedInput("Házszám", IsValidStreetNumber);
        }

        private void GatherVehicleDetails(RegisterNewVehicleRequest vehicleParameters)
        {
            view.DisplayMessage("Jármű adatok");
            view.DisplayMessage("-------------");
            vehicleParameters.VehicleType = GetVerifiedInput("Kategória", IsValidCategory);
            vehicleParameters.Make = GetVerifiedInput("Gyártmány", IsValidName);
            vehicleParameters.Model = GetVerifiedInput("Típus", IsValidName);
            vehicleParameters.EngineNumber = GetVerifiedInput("Motorszám", IsValidEngineNumber);
            vehicleParameters.MotorEmissionType = GetVerifiedInput("Környezetvédelmi osztályba sorolás", IsValidEmissionType);
            vehicleParameters.FirstRegistrationDate = GetVerifiedInput("Első nyilvántartásba vétel időpontja", IsValidDate);
            vehicleParameters.NumberOfSeats = GetVerifiedIntegerInput("Ülések száma", IsValidNumberOfSeats);
            vehicleParameters.Color = GetVerifiedInput("Szín", IsValidColor);
            vehicleParameters.MassInService = GetVerifiedIntegerInput("Saját tömeg", IsValidWeight);
            vehicleParameters.MaxMass = GetVerifiedIntegerInput("Össztömeg", IsValidWeight);
            vehicleParameters.BrakedTrailer = GetVerifiedIntegerInput("Fékezett vontatmány", IsValidTrailerWeight);
            vehicleParameters.UnbrakedTrailer = GetVerifiedIntegerInput("Fékezetlen vontatmány", IsValidTrailerWeight);
        }

        public void DisplayVehicleData(string vehicleDataResponse)
        {
            LoadVehicleDataResponse vehicleParameters = JsonHandler.Deserialize<LoadVehicleDataResponse>(vehicleDataResponse);

            if (vehicleParameters.Error == null )
            {
                view.DisplayMessage("Jármű adatok");
                view.DisplayMessage("--------------");
                view.DisplayMessage("Tulajdonos: " + vehicleParameters.LastName + " " + vehicleParameters.FirstName);
                view.DisplayMessage($"Rendszám: {vehicleParameters.RegistrationNumber}");
                view.DisplayMessage("Kategória: " + vehicleParameters.VehicleType);
                view.DisplayMessage("Gyártmány: " + vehicleParameters.Make);
                view.DisplayMessage("Típus: " + vehicleParameters.Model);
                view.DisplayMessage("Motorszám: " + vehicleParameters.EngineNumber);
                view.DisplayMessage("Környezetvédelmi osztályba sorolás: " + vehicleParameters.MotorEmissionType);
                view.DisplayMessage("Ülések száma: " + vehicleParameters.NumberOfSeats);
                view.DisplayMessage("Szín: " + vehicleParameters.Color);
                view.DisplayMessage("Saját tömeg: " + vehicleParameters.MassInService);
                view.DisplayMessage("Össztömeg: " + vehicleParameters.MaxMass);
                view.DisplayMessage("Fékezett vontatmány: " + vehicleParameters.BrakedTrailer);
                view.DisplayMessage("Fékezetlen vontatmány: " + vehicleParameters.UnbrakedTrailer);
            }
            else
            {
                view.DisplayMessage(vehicleParameters.Error.Message);
            }            
        }

        private string GetVerifiedInput(string prompt, Func<string, bool> validationFunction)
        {
            while (true)
            {
                string userInput = view.GetInput(prompt);
                if (!string.IsNullOrWhiteSpace(userInput) && validationFunction(userInput))
                {
                    return userInput.ToUpper();
                }
                else
                {
                    view.DisplayMessage($"Hibás adat! Ellenőrizze, majd próbálja a újra.");
                }
            }
        }

        private int GetVerifiedIntegerInput(string prompt, Func<int, bool> validationFunction)
        {
            while (true)
            {
                string userInput = view.GetInput(prompt);
                if (int.TryParse(userInput, out int intValue) && validationFunction(intValue))
                {
                    return intValue;
                }
                else
                {
                    view.DisplayMessage($"Hibás adat! Ellenőrizze, majd próbálja a újra.");
                }
            }
        }

        private bool IsValidRegistrationNumber(string registrationNumber)
        {
            string pattern = @"^[A-Z]{4}\d{3}$";
            if (!PatternChecker(pattern, registrationNumber))
            {
                return false;
            }
            return true;
        }

        private static bool PatternChecker(string pattern, string data)
        {
            return !string.IsNullOrEmpty(data) && Regex.IsMatch(data, pattern);
        }

        private bool IsValidName(string input)
        {
            return true;
        }

        private bool IsValidPostalCode(string input)
        {
            return true;
        }

        private bool IsValidStreetNumber(string input)
        {
            return true;
        }

        private bool IsValidWeight(int value)
        {
            return true;
        }

        private bool IsValidColor(string arg)
        {
            return true;
        }

        private bool IsValidNumberOfSeats(int arg)
        {
            return true;
        }

        private bool IsValidDate(string arg)
        {
            return true;
        }

        private bool IsValidEmissionType(string arg)
        {
            return true;
        }

        private bool IsValidEngineNumber(string arg)
        {
            return true;
        }

        private bool IsValidCategory(string arg)
        {
            return true;
        }

        private bool IsValidTrailerWeight(int value)
        {
            return true;
        }
    }
}
