using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessageObjects.Commands
{
    internal class CreateCommand
    {
        public GenericCommandMessage<RegisterNewVehicleCommand> CreateRegisterVehicleCommand(string message, bool success)
        {
            var registerVehicleMessage = new GenericCommandMessage<RegisterNewVehicleCommand>
            {
                Command = "RegisterNewVehicle"
            };

            if (success)
            {
                registerVehicleMessage.Data = new RegisterNewVehicleCommand
                {
                    RegistrationNumber = message
                };
            }
            else if (message == "100")
            {
                registerVehicleMessage.Error = new ErrorData
                {
                    Message = "A megadott motorszámmal már regisztráltak járművet!",
                    ErrorCode = 100
                };
            }

            return registerVehicleMessage;
        }
    }
}
