using Core;
using MainUi.MessageObjects.Commands;
using MainUi.MessageObjects;
using BoundaryHelper;

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

        public void RegisterVehicle(RegisterNewVehicleRequest vehicleParameters)
        {
            string jsonRequest = JsonHandler.Serialize(vehicleParameters);
            vehicleManagerInBoundary.RegisterNewVehicle(jsonRequest);


            //GenericCommandMessage<RegisterNewVehicleCommand> outputMessage;
            //outputMessage = createCommand.CreateRegisterVehicleCommand(vehicleParameters);
            //vehicleManagerInBoundary.ProcessTrafficMessage(outputMessage.Serialize());
        }

        public void LoadVehicle(string registrationNumber)
        {            
            LoadVehicleDataRequest registrationNumberRequest = new LoadVehicleDataRequest { RegistrationNumber = registrationNumber };
            string jsonRequest = JsonHandler.Serialize(registrationNumberRequest);
            vehicleManagerInBoundary.LoadVehicleData(jsonRequest);
        }


    }
}
