using Core.MessageObjects;
using Core.MessageObjects.Commands;
using Core.VerificationObjects;
using Entity;
using System.Security.Cryptography;
using System.Text;

namespace Core
{
    internal class VehicleRegistrationManager
    {
        private IPersistentVehicleGateway persistentVehicleGateway;
        private IVehicleManagerPresenterOutBoundary presenterManager;

        public VehicleRegistrationManager(IPersistentVehicleGateway persistentVehicleGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;
            this.presenterManager = presenterManager;
        }

        public void SeparatePersonalAndVehicelData(VehicleRegistrationInfo validatedUserData)
        {
            var createCommand = new CreateCommand();
            GenericCommandMessage<RegisterNewVehicleCommand> outputMessage;

            if (persistentVehicleGateway.IsEngineNumberInUse(validatedUserData.EngineNumber))
            {
                outputMessage = createCommand.CreateRegisterVehicleCommand(error: 100);
            }
            else
            {
                string previousRegistrationNumber = persistentVehicleGateway.GetLatestRegNumber();
                string newRegistrationNumber = new RegistrationNumberGenerator().GetNextRegistrationNumber(previousRegistrationNumber);

                string peronHash = RegisterPerson(validatedUserData);
                Vehicle newVehicle = RegisterVehice(validatedUserData, newRegistrationNumber, peronHash);                

                outputMessage = createCommand.CreateRegisterVehicleCommand(newVehicle);
            }

            presenterManager.displayMessage(outputMessage.Serialize());
        }

        private string RegisterPerson(VehicleRegistrationInfo validatedUserData)
        {
            //todo: check if person already exists
            Person person = new Person(validatedUserData.FirstName, validatedUserData.LastName, validatedUserData.AdPostalCode,
                                        validatedUserData.AdCity, validatedUserData.AdStreet, validatedUserData.AdStreetNumber);

            //todo: save file
            return person.GenerateHash();
        }

        private Vehicle RegisterVehice(VehicleRegistrationInfo validatedUserData, string registrationNumber, string personHash)
        {
            Vehicle vehicle = new Vehicle(registrationNumber, validatedUserData.VehicleType, validatedUserData.Make, validatedUserData.Model,
                                            validatedUserData.EngineNumber, validatedUserData.MotorEmissionType, validatedUserData.FirstRegistrationDate,
                                            validatedUserData.NumberOfSeats, validatedUserData.Color, validatedUserData.MassInService, validatedUserData.MaxMass,
                                            validatedUserData.BrakedTrailer, validatedUserData.UnbrakedTrailer, personHash);

            persistentVehicleGateway.SaveVehicle(vehicle);

            return vehicle;
        }
    }
}
