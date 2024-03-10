using BoundaryHelper;
using ConsoleApplication.Verification;
using System.Text.RegularExpressions;
using Core.Interfaces;

namespace ConsoleApplication
{
    public class Presenter : IVehicleManagerPresenterOutBoundary
    {
        private ConsoleView view;

        public Presenter(ConsoleView view)
        {
            this.view = view;
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
        
    }
}
