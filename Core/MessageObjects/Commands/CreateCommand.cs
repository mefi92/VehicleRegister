using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessageObjects.Commands
{
    internal class CreateCommand
    {
        public GenericCommandMessage<RegisterNewVehicleCommand> CreateRegisterVehicleCommand(string registrationNumber = "", int error = 0)
        {
            var registerVehicleMessage = new GenericCommandMessage<RegisterNewVehicleCommand>
            {
                Command = "RegisterNewVehicle"
            };

            if (error == 0)
            {
                registerVehicleMessage.Data = new RegisterNewVehicleCommand
                {
                    RegistrationNumber = registrationNumber
                };
            }
            else if (error == 100)
            {
                registerVehicleMessage.Error = new ErrorData
                {
                    Message = "A megadott motorszámmal már regisztráltak járművet!",
                    ErrorCode = error
                };
            }

            return registerVehicleMessage;
        }

        public GenericCommandMessage<LoadVehicleDataCommand> CreateLoadVehicleCommand(string vehicleType = "", string registrationNumber = "", string engineNumber = "", int error = 0)
        {
            var loadVehicleDataMessage = new GenericCommandMessage<LoadVehicleDataCommand>
            {
                Command = "load_vehicle_data"
            };

            if (error == 0)
            {
                loadVehicleDataMessage.Data = new LoadVehicleDataCommand
                {
                    VehicleType = vehicleType,
                    RegistrationNumber = registrationNumber,    
                    EngineNumber = engineNumber,
                };
            }
            else if (error == 101)
            {
                loadVehicleDataMessage.Error = new ErrorData
                {
                    Message = "A megadott rendszámmal már regisztráltak járművet!",
                    ErrorCode = error
                };
            }
            else if (error == 102)
            {
                loadVehicleDataMessage.Error = new ErrorData
                {
                    Message = "A megadott rendszám nem létezik!\nPróbálja újra.",
                    ErrorCode = error
                };
            }

            return loadVehicleDataMessage;
        }
    }
}
