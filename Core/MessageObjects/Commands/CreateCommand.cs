using Entity;

namespace Core.MessageObjects.Commands
{
    internal class CreateCommand
    {
        public GenericCommandMessage<RegisterNewVehicleCommand> CreateRegisterVehicleCommand(Vehicle vehicle = null, int error = 0)
        {
            var registerVehicleMessage = new GenericCommandMessage<RegisterNewVehicleCommand>
            {
                Command = "register_new_vehicle"
            };

            if (error == 0 && vehicle != null)
            {
                registerVehicleMessage.Data = new RegisterNewVehicleCommand
                {
                    RegistrationNumber = vehicle.RegistrationNumber,
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

        public GenericCommandMessage<Vehicle> CreateLoadVehicleDataCommand(Vehicle vehicle = null, int error = 0)
        {
            var loadVehicleDataMessage = new GenericCommandMessage<Vehicle>
            {
                Command = "load_vehicle_data"
            };

            if (error == 0 && vehicle != null)
            {
                loadVehicleDataMessage.Data = vehicle;
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
