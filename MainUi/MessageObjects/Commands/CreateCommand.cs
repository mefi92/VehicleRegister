
namespace MainUi.MessageObjects.Commands
{
    internal class CreateCommand
    {
        public GenericCommandMessage<LoadVehicleDataCommand> CreateLoadVehicleDataCommand(string registrationNumber)
        {
            var loadVehicleMessage = new GenericCommandMessage<LoadVehicleDataCommand>
            {
                Command = "load_vehicle_data",
                Data = new LoadVehicleDataCommand
                {
                    RegistrationNumber = registrationNumber
                }
            };

            return loadVehicleMessage;
        }

        public GenericCommandMessage<RegisterNewVehicleCommand> CreateRegisterVehicleCommand(RegisterNewVehicleCommand vehicleParameters)
        {
            var registerVehicleMessage = new GenericCommandMessage<RegisterNewVehicleCommand>
            {
                Command = "register_new_vehicle",
                Data = vehicleParameters                
            };

            return registerVehicleMessage;
        }
    }
}
