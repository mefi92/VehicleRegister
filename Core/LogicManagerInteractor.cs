using Core.MessageObjects;
using Core.MessageObjects.Commands;
using Core.VerificationObjects;
using Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public void ProcessUserDataForRegistration(VehicleRegistrationInfo validatedUserData)
        {
            VehicleRegistrationManager registerManager = new VehicleRegistrationManager(persistentVehicleGateway, presenterManager);
            registerManager.SeparatePersonalAndVehicelData(validatedUserData);
        }

        public void LoadVehicleManager(string registrationNumber)
        {                        
            Vehicle vehicle = persistentVehicleGateway.LoadVehicle(registrationNumber);

            var createCommand = new CreateCommand();
            GenericCommandMessage<LoadVehicleDataCommand> outputMessage;

            if (vehicle == null)
            {
                outputMessage = createCommand.CreateLoadVehicleDataCommand(error: 102); 
            }
            else
            {
                outputMessage = createCommand.CreateLoadVehicleDataCommand(vehicle.VehicleType, vehicle.RegistrationNumber, vehicle.EngineNumber);
            }

            presenterManager.displayMessage(outputMessage.Serialize());

        }

        private void ErrorMessageHandler(List<int> errorCodes)
        {
            var createCommand = new CreateCommand();
            GenericCommandMessage<RegisterNewVehicleCommand> outputMessage;

            foreach (int code in errorCodes)
            {
                outputMessage = createCommand.CreateRegisterVehicleCommand(error: code);
                presenterManager.displayMessage(outputMessage.Serialize());
            }
        }
                 
        public void ProcessTrafficMessage(string message)
        {
            if (message == null) return;            

            var deserializedMessage = JsonConvert.DeserializeObject<dynamic>(message);            
            string command = deserializedMessage.Command;

            if (deserializedMessage.Error != null)
            {
                // Később, ha a UI felől hibát küldenénk itt le lehet kezelni.                    
            }

            switch (command)
            {
                case "load_vehicle_data":
                    string registrationNumber = deserializedMessage.Data.RegistrationNumber;
                    LoadVehicleManager(registrationNumber);
                    break;
                case "register_new_vehicle":
                    VehicleRegistrationInfo userDataForRegistration = new VehicleRegistrationInfo(deserializedMessage.Data);
                    List<int> validationOutcome = userDataForRegistration.ValidateVehiceDataFormat();

                    if(validationOutcome.Count != 0)
                    {
                        ErrorMessageHandler(validationOutcome);
                        break;
                    }
                    ProcessUserDataForRegistration(userDataForRegistration);
                    break;
                default:
                    break;
            }
        }
    }
}