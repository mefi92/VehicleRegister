using Core;
using BoundaryHelper;


namespace ConsoleUi
{
    public class Presenter : IVehicleManagerPresenterOutBoundary
    {
        private Model _model;
        private ConsoleView _view;
        private IVehicleManagerInBoundary vehicleManager;

        public Presenter(Model model, ConsoleView view)
        {
            this._model = model;
            this._view = view;
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
                _view.DisplayMenu();
                string input = _view.GetUserInput();
                isExiting = OperationSelection(input);
            }
        }

        private bool OperationSelection(string input)
        {
            switch (input.ToLower())
            {
                case "r":                    
                    _view.DisplayMessage("Új jármű regisztrálása.");
                    RegisterNewVehicleInput();
                    return false;

                case "l":                    
                    _view.DisplayMessage("Jármű adatai rendszám alapján.");
                    LoadVehicleDataInput();
                    return false;

                case "q":
                    _view.DisplayMessage("Kilépés az applikációból.");
                    return true;

                default:
                    _view.DisplayMessage("Helytelen választás, próbáld újra!");
                    return false;
            }
        }

        public void RegisterNewVehicleInput()
        {
            RegisterNewVehicleRequest vehicleParameters = new RegisterNewVehicleRequest();

            _view.DisplayMessage("\nAdja meg következő adatokat");

            GatherPersonalDetails(vehicleParameters);

            GatherAddressDetails(vehicleParameters);

            GatherVehicleDetails(vehicleParameters);

            vehicleManager.RegisterNewVehicle(JsonHandler.Serialize(vehicleParameters));
        }

        public void LoadVehicleDataInput()
        {
            LoadVehicleDataRequest vehicleParameters = new LoadVehicleDataRequest();

            _view.DisplayMessage("Írja be a rendszámot a következő formátumba: AAAA123");
            vehicleParameters.RegistrationNumber = _view.GetUserInput().ToUpper();            
            vehicleManager.LoadVehicleData(JsonHandler.Serialize(vehicleParameters));
        }

        public void DisplayRegistrationResult(string registrationResultResponse)
        {
            _model.RegistrationNumber = JsonHandler.Deserialize<RegisterNewVehicleResponse>(registrationResultResponse).RegistrationNumber;
            _view.DisplayVehicleRegistration(_model.RegistrationNumber);
        }

        private void GatherPersonalDetails(RegisterNewVehicleRequest vehicleParameters)
        {
            _view.DisplayMessage("\nSzemélyes adatok");
            _view.DisplayMessage("-----------------");
            vehicleParameters.LastName = _view.GetInput("Vezetéknév").ToUpper();
            vehicleParameters.FirstName = _view.GetInput("Keresztnév").ToUpper();
        }

        private void GatherAddressDetails(RegisterNewVehicleRequest vehicleParameters)
        {
            _view.DisplayMessage("\nLakcím adatok");
            _view.DisplayMessage("-------------");
            vehicleParameters.AdPostalCode = _view.GetInput("Irányítószám").ToUpper();
            vehicleParameters.AdCity = _view.GetInput("Város").ToUpper();
            vehicleParameters.AdStreet = _view.GetInput("Utca").ToUpper();
            vehicleParameters.AdStreetNumber = _view.GetInput("Házszám").ToUpper();
        }

        private void GatherVehicleDetails(RegisterNewVehicleRequest vehicleParameters)
        {
            _view.DisplayMessage("\nJármű adatok");
            _view.DisplayMessage("-------------");
            vehicleParameters.VehicleType = _view.GetInput("Kategória").ToUpper();
            vehicleParameters.Make = _view.GetInput("Gyártmány").ToUpper();
            vehicleParameters.Model = _view.GetInput("Típus").ToUpper();
            vehicleParameters.EngineNumber = _view.GetInput("Motorszám").ToUpper();
            vehicleParameters.MotorEmissionType = _view.GetInput("Környezetvédelmi osztályba sorolás").ToUpper();
            vehicleParameters.FirstRegistrationDate = _view.GetInput("Első nyilvántartásba vétel időpontja").ToUpper();
            vehicleParameters.NumberOfSeats = _view.GetIntegerInput("Ülések száma");
            vehicleParameters.Color = _view.GetInput("Szín").ToUpper();
            vehicleParameters.MassInService = _view.GetIntegerInput("Saját tömeg");
            vehicleParameters.MaxMass = _view.GetIntegerInput("Össztömeg");
            vehicleParameters.BrakedTrailer = _view.GetIntegerInput("Fékezett vontatmány");
            vehicleParameters.UnbrakedTrailer = _view.GetIntegerInput("Fékezetlen vontatmány");
        }

        public void DisplayVehicleData(string vehicleDataResponse)
        {
            LoadVehicleDataResponse vehicleParameters = JsonHandler.Deserialize<LoadVehicleDataResponse>(vehicleDataResponse);
            
            _view.DisplayMessage("\nJármű adatok");
            _view.DisplayMessage("--------------");
            _view.DisplayMessage("Tulajdonos: " + vehicleParameters.LastName + " " + vehicleParameters.FirstName);
            _view.DisplayMessage($"Rendszám: {vehicleParameters.RegistrationNumber}");
            _view.DisplayMessage("Kategória: " + vehicleParameters.VehicleType);
            _view.DisplayMessage("Gyártmány: " + vehicleParameters.Make);
            _view.DisplayMessage("Típus: " + vehicleParameters.Model);
            _view.DisplayMessage("Motorszám: " + vehicleParameters.EngineNumber);
            _view.DisplayMessage("Környezetvédelmi osztályba sorolás: " + vehicleParameters.MotorEmissionType);
            _view.DisplayMessage("Ülések száma: " + vehicleParameters.NumberOfSeats);
            _view.DisplayMessage("Szín: " + vehicleParameters.Color);
            _view.DisplayMessage("Saját tömeg: " + vehicleParameters.MassInService);
            _view.DisplayMessage("Össztömeg: " + vehicleParameters.MaxMass);
            _view.DisplayMessage("Fékezett vontatmány: " + vehicleParameters.BrakedTrailer);
            _view.DisplayMessage("Fékezetlen vontatmány: " + vehicleParameters.UnbrakedTrailer);
        } 
    }
}
