using Core.MessageObjects;
using Core.MessageObjects.Commands;
using Entity;

namespace Core
{
    internal class RegisterNewVehicleManager
    {
        private IPersistentVehicleGateway persistentVehicleGateway;
        private IVehicleManagerPresenterOutBoundary presenterManager;        

        public RegisterNewVehicleManager(IPersistentVehicleGateway persistentVehicleGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;
            this.presenterManager = presenterManager;
        }

        public void RegisterNewVehicle(string vehicleType, string engineNumber)
        {
            GenericCommandMessage<RegisterNewVehicleCommand> outputMessage;
            CreateCommand createCommand = new CreateCommand();

            if (persistentVehicleGateway.IsExistsEngineNumber(engineNumber))
            {
                outputMessage = createCommand.CreateRegisterVehicleCommand(error: 100);
            }
            else
            {
                string? latestRegNumber = persistentVehicleGateway.GetLatestRegNumber();
                Vehicle newVehicle = new RegisterNewVehicle().addNewVehicle(vehicleType, engineNumber, latestRegNumber);
                persistentVehicleGateway.saveVehicle(newVehicle);

                outputMessage = createCommand.CreateRegisterVehicleCommand(newVehicle.registrationNumber);
            }

            presenterManager.displayMessage(outputMessage.Serialize());
        }


        

    }
}
