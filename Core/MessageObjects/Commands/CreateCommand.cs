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
            else if (error == 201)
            {
                registerVehicleMessage.Error = new ErrorData
                {
                    Message = "Nem létező jármű kategória!\nVálssz az albábiak közül:\n\t M1, N1, N2, N3, O1, O2, O3, L3E\n",
                    ErrorCode = error
                };
            }
            else if (error == 202)
            {
                registerVehicleMessage.Error = new ErrorData
                {
                    Message = "Hibás motorszám formátum!\n\t Példa: IK260220055445\n",
                    ErrorCode = error
                };
            }

            return registerVehicleMessage;
        }

        public GenericCommandMessage<LoadVehicleDataCommand> CreateLoadVehicleDataCommand(string vehicleType = "", string registrationNumber = "", string engineNumber = "", int error = 0)
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
