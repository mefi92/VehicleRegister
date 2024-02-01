using Core.MessageObjects;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class RegisterNewVehicleManager
    {
        private PersistentVehicleGateway persistentVehicleGateway;
        private VehicleManagerPresenterOutBoundary presenterManager;

        public RegisterNewVehicleManager(PersistentVehicleGateway persistentVehicleGateway, VehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;
            this.presenterManager = presenterManager;
        }

        public void RegisterNewVehicle(string vehicleType, string engineNumber)
        {
            if (persistentVehicleGateway.IsExistsEngineNumber(engineNumber))
            {
                RegisterNewVehicleOutputMessage outputMessage = GenerateEngineNumberExistsOutput();
                JsonConverter jsonConverter = new JsonConverter();

                presenterManager.displayMessage(jsonConverter.ConvertToStringRegisterNewVehicleOutputMessage(outputMessage));
            }
            string? latestRegNumber = persistentVehicleGateway.GetLatestRegNumber();
            Vehicle newVehicle = new RegisterNewVehicle().addNewVehicle(vehicleType, engineNumber, latestRegNumber);
            persistentVehicleGateway.saveVehicle(newVehicle);
            presenterManager.displayMessage("Sikeres regsztráció!\nA jármű rendszáma:\n" + latestRegNumber);
        }

        public RegisterNewVehicleOutputMessage GenerateEngineNumberExistsOutput()
        {
            RegisterNewVehicleOutputMessage outputMessage = new RegisterNewVehicleOutputMessage();
            outputMessage.Error = new OutputMessageError();
            outputMessage.Error.Message = "Már létezik ilyen motorszámmal jármű!";
            outputMessage.Error.ErrorCode = 1;

            return outputMessage;
        }
    }
}
