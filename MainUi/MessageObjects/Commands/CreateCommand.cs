
namespace MainUi.MessageObjects.Commands
{
    internal class CreateCommand
    {
        public GenericCommandMessage<LoadVehicleDataCommand> CreateLoadVehicleDataCommand(string message)
        {
            var loadVehicleMessage = new GenericCommandMessage<LoadVehicleDataCommand>
            {
                Command = "load_vehicle_data",
                Data = new LoadVehicleDataCommand
                {
                    RegistrationNumber = message
                }                
            };            

            return loadVehicleMessage;
        }
    }
}
