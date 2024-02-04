using Core.MessageObjects;
using Core.MessageObjects.Commands;
using Entity;
using Newtonsoft.Json;

namespace Core
{
    public class LogicManagerInteractor : IVehicleManagerInBoundary
    {
        private IPersistentVehicleGateway persistentVehicleGateway;
        private IVehicleManagerPresenterOutBoundary presenterManager;

        public LogicManagerInteractor(IPersistentVehicleGateway persistentVehicleGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;

            this.presenterManager = presenterManager;
        }

        public void registerNewVehicle(string vehicleType, string engineNumber)
        {
            RegisterNewVehicleManager registerManager = new RegisterNewVehicleManager(persistentVehicleGateway, presenterManager);
            registerManager.RegisterNewVehicle(vehicleType, engineNumber);
        }

        public void LoadVehicle(string registrationNumber)
        { 
            Vehicle vehicle = persistentVehicleGateway.loadVehicle(registrationNumber);

            var createCommand = new CreateCommand();
            GenericCommandMessage<LoadVehicleDataCommand> outputMessage;

            if (vehicle == null)
            {
                outputMessage = createCommand.CreateLoadVehicleDataCommand(error: 102); 
            }
            else
            {
                outputMessage = createCommand.CreateLoadVehicleDataCommand(vehicle.type, vehicle.registrationNumber, vehicle.engineNumber);
            }

            presenterManager.displayMessage(outputMessage.Serialize());

        }
                 
        public void ProcessTrafficMessage(string message)
        {
            if (message == null) return;

            var deserializedMessage = JsonConvert.DeserializeObject<dynamic>(message);
            string command = deserializedMessage.Command;

            if (deserializedMessage.Error != null)
            {
                // todo: itt valami választ lehetne a későbbiekben dobni a ui felé, ha hiba lenne bármiért is.                    
            }

            switch (command)
            {
                case "load_vehicle_data":
                    string registrationNumber = deserializedMessage.Data.RegistrationNumber;
                    LoadVehicle(registrationNumber);
                    break;
                case "register_new_vehicle":
                    string vehicleType = deserializedMessage.Data.VehicleType;
                    string engineNumber = deserializedMessage.Data.EngineNumber;
                    registerNewVehicle(vehicleType, engineNumber);
                    break;
                default:
                    break;
            }
        }
    }
}