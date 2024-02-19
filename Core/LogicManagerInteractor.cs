using BoundaryHelper;
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
            GenericCommandMessage<Vehicle> outputMessage;

            if (vehicle == null)
            {
                outputMessage = createCommand.CreateLoadVehicleDataCommand(error: 102); 
            }
            else
            {
                Person person = persistentVehicleGateway.LoadPerson(vehicle.OwnerHash);
                vehicle.OwnerHash = $"{person.LastName} {person.FirstName}";
                outputMessage = createCommand.CreateLoadVehicleDataCommand(vehicle);
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
                case CommandConstants.LoadVehicleData:
                    ProcessLoadVehicleData(deserializedMessage);
                    break;
                case CommandConstants.RegisterNewVehicle:
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

        private void ProcessLoadVehicleData(dynamic deserializedMessage)
        {
            string registrationNumber = deserializedMessage.Data.RegistrationNumber;
            LoadVehicleManager(registrationNumber);
        }

        public void LoadVehicleData(string request)
        {
            LoadVehicleDataRequest registrationNumberRequest = JsonHandler.Deserialize<LoadVehicleDataRequest>(request);
            LoadVehicleManager(registrationNumberRequest.RegistrationNumber);
        }

        public void RegisterNewVehicle(string request)
        {
            RegisterNewVehicleRequest registerNewVehicleRequest = JsonHandler.Deserialize<RegisterNewVehicleRequest>(request);

            VehicleRegistrationInfo userDataForRegistration = new VehicleRegistrationInfo(registerNewVehicleRequest);
            List<int> validationOutcome = userDataForRegistration.ValidateVehiceDataFormat();

            // TODO: a validáció simán mehetne a reg classba és akkor már a gui -n lehetne javítani, ha valami hiba van

            if (validationOutcome.Count != 0)
            {
                ErrorMessageHandler(validationOutcome);                
            }            

            ProcessUserDataForRegistration(userDataForRegistration);

        }

        public static class CommandConstants
        {
            public const string RegisterNewVehicle = "register_new_vehicle";
            public const string LoadVehicleData = "load_vehicle_data";
        }
    }
}