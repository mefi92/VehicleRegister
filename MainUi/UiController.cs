using Core;
using MainUi.MessageObjects.Commands;
using MainUi.MessageObjects;

namespace MainUi
{
    public class UiController
    {
        private IVehicleManagerInBoundary vehicleManagerInBoundary;

        public UiController(IVehicleManagerInBoundary vehicleManagerInBoundary)
        {
            this.vehicleManagerInBoundary = vehicleManagerInBoundary;
        }

        public void addNewVehicle(string vehicleType, string engineNumber)
        {
            vehicleManagerInBoundary.registerNewVehicle(vehicleType, engineNumber);
        }

        public void LoadVehicle(string registrationNumber)
        {
            //vehicleManagerInBoundary.LoadVehicle(registrationNumber);
            

            GenericCommandMessage<LoadVehicleDataCommand> outputMessage;
            CreateCommand createCommand = new CreateCommand();

            outputMessage = createCommand.CreateLoadVehicleDataCommand(registrationNumber);
            vehicleManagerInBoundary.ProcessTrafficMessage(outputMessage.Serialize());
        }

    }
}
