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

        public void registerNewVehicle(string vehicleType, string engineNumber)
        {
            VehicleRegistrationManager registerManager = new VehicleRegistrationManager(persistentVehicleGateway, presenterManager);
            registerManager.RegisterNewVehicle(vehicleType, engineNumber);
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
                outputMessage = createCommand.CreateLoadVehicleDataCommand(vehicle.type, vehicle.registrationNumber, vehicle.engineNumber);
            }

            presenterManager.displayMessage(outputMessage.Serialize());

        }

        private void ErrorMessageHandler(List<int> errorCodes)
        {
            foreach (int code in errorCodes)
            {
                int a = code;
            }
            /*
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

            presenterManager.displayMessage(outputMessage.Serialize()); ;
            */


            // IMPORTANT: a hiba validator egy hiba listát adjon vissza és az errorba legyen lekezelve! ez most egész jó ötletnek tűnik

        }
                 
        public void ProcessTrafficMessage(string message)
        {
            if (message == null) return;            

            var deserializedMessage = JsonConvert.DeserializeObject<dynamic>(message);            
            string command = deserializedMessage.Command;

            // todo: reg number verification -> format before loading!!
            // all fomrat check? vagy ide vagy a reg load-ba

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
                    UnvalidatedVehicle unvalidatedVehicle = new UnvalidatedVehicle(deserializedMessage.Data);
                    List<int> validationOutcome = unvalidatedVehicle.ValidateVehiceDataFormat();

                    if(validationOutcome.Count != 0)
                    {
                        ErrorMessageHandler(validationOutcome);
                        break;
                    }
                    registerNewVehicle(unvalidatedVehicle.VehicleType, unvalidatedVehicle.EngineNumber);
                    break;
                default:
                    break;
            }
        }
    }
}