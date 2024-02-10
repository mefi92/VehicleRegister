using Core;
using MainUi.MessageObjects.Commands;
using MainUi.MessageObjects;

namespace MainUi
{
    public class UiController
    {
        private IVehicleManagerInBoundary vehicleManagerInBoundary;
        
        private CreateCommand createCommand = new CreateCommand();

        public UiController(IVehicleManagerInBoundary vehicleManagerInBoundary)
        {
            this.vehicleManagerInBoundary = vehicleManagerInBoundary;
        }

        public void RegisterVehicle(RegisterNewVehicleCommand vehicleParameters)
        {            
            GenericCommandMessage<RegisterNewVehicleCommand> outputMessage;
            outputMessage = createCommand.CreateRegisterVehicleCommand(vehicleParameters);
            vehicleManagerInBoundary.ProcessTrafficMessage(outputMessage.Serialize());
        }

        public void LoadVehicle(string registrationNumber)
        { 
            GenericCommandMessage<LoadVehicleDataCommand> outputMessage;
            outputMessage = createCommand.CreateLoadVehicleDataCommand(registrationNumber);
            vehicleManagerInBoundary.ProcessTrafficMessage(outputMessage.Serialize());
        }


    }
}
