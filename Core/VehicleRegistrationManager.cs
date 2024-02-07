using Core.MessageObjects;
using Core.MessageObjects.Commands;
using Entity;

namespace Core
{
    internal class VehicleRegistrationManager
    {
        private IPersistentVehicleGateway persistentVehicleGateway;
        private IVehicleManagerPresenterOutBoundary presenterManager;        

        public VehicleRegistrationManager(IPersistentVehicleGateway persistentVehicleGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;
            this.presenterManager = presenterManager;
        }

        public void RegisterNewVehicle(string vehicleType, string engineNumber)
        {
            var createCommand = new CreateCommand();
            GenericCommandMessage<RegisterNewVehicleCommand> outputMessage;            

            if (persistentVehicleGateway.IsEngineNumberInUse(engineNumber))
            {
                outputMessage = createCommand.CreateRegisterVehicleCommand(error: 100);
            }
            else
            {
                string latestRegistrationNumber = persistentVehicleGateway.GetLatestRegNumber();
                Vehicle newVehicle = new RegisterNewVehicle().addNewVehicle(vehicleType, engineNumber, latestRegistrationNumber);
                persistentVehicleGateway.SaveVehicle(newVehicle);

                outputMessage = createCommand.CreateRegisterVehicleCommand(newVehicle.registrationNumber);
            }

            presenterManager.displayMessage(outputMessage.Serialize());
        } 
    }
}
