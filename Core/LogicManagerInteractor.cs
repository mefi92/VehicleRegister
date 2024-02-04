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

            GenericCommandMessage<LoadVehicleDataCommand> outputMessage;
            CreateCommand createCommand = new CreateCommand();

            if (vehicle == null)
            {
                outputMessage = createCommand.CreateLoadVehicleCommand(error: 102); 
            }
            else
            {
                outputMessage = createCommand.CreateLoadVehicleCommand(vehicle.type, vehicle.registrationNumber, vehicle.engineNumber);
            }

            presenterManager.displayMessage(outputMessage.Serialize());

        }
                 
        public void ProcessTrafficMessage(string message)
        {
            if (message != null)
            {
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

                }

            }
                     
            

            

        }

        
    }
}
