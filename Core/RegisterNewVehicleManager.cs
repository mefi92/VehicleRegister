using Core.MessageObjects;
using Core.MessageObjects.Commands;
using Entity;

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
            GenericCommandMessage<RegisterNewVehicleCommand> outputMessage;

            if (persistentVehicleGateway.IsExistsEngineNumber(engineNumber))
            {
                outputMessage = CreateRegisterVehicleCommand("100", false);
            }
            else
            {
                string? latestRegNumber = persistentVehicleGateway.GetLatestRegNumber();
                Vehicle newVehicle = new RegisterNewVehicle().addNewVehicle(vehicleType, engineNumber, latestRegNumber);
                persistentVehicleGateway.saveVehicle(newVehicle);

                outputMessage = CreateRegisterVehicleCommand(latestRegNumber, true);
            }

            presenterManager.displayMessage(outputMessage.Serialize());
        }


        public GenericCommandMessage<RegisterNewVehicleCommand> CreateRegisterVehicleCommand(string message, bool success)
        {
            var registerVehicleMessage = new GenericCommandMessage<RegisterNewVehicleCommand>
            {
                Command = "RegisterNewVehicle"
            };

            if (success)
            {
                registerVehicleMessage.Data = new RegisterNewVehicleCommand
                {
                    RegistrationNumber = message
                };
            }
            else if (message == "100")
            {
                registerVehicleMessage.Error = new ErrorData
                {
                    Message = "A megadott motorszámmal már regisztráltak járművet!",
                    ErrorCode = 100
                };
            }

            return registerVehicleMessage;
        }

    }
}
