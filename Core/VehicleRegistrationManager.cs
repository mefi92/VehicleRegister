using BoundaryHelper;
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

        public void SeparatePersonalAndVehicelData(RegisterNewVehicleRequest validatedUserData)
        { 
            if (persistentVehicleGateway.IsItemInUse(validatedUserData.EngineNumber))
            { 
                //registerNewVehicleRequest.Error.Message = "A megadott motorszámmal már regisztráltak járművet!";
                //registerNewVehicleRequest.Error.ErrorCode = 100;

                // TODO: ide validátorba valami funkció ami ezt az error t visszadobja
            }
            else
            {
                string previousRegistrationNumber = persistentVehicleGateway.GetLatestRegNumber();
                string newRegistrationNumber = new RegistrationNumberGenerator().GetNextRegistrationNumber(previousRegistrationNumber);

                Person person = RegisterPerson(validatedUserData);
                RegisterNewVehicleResponse registerNewVehicleResponse = RegisterVehice(validatedUserData, person, newRegistrationNumber);
                presenterManager.DisplayRegistrationResult(JsonHandler.Serialize(registerNewVehicleResponse));
            }            
        }

        private Person RegisterPerson(RegisterNewVehicleRequest validatedUserData)
        {            
            Person person = new Person(validatedUserData.FirstName, validatedUserData.LastName, validatedUserData.AdPostalCode,
                                        validatedUserData.AdCity, validatedUserData.AdStreet, validatedUserData.AdStreetNumber);
            
            string personHash = person.Hash;

            
            if (!persistentVehicleGateway.IsItemInUse(personHash))
            {
                persistentVehicleGateway.SavePerson(person);
            }
            
            return person;
        }

        private RegisterNewVehicleResponse RegisterVehice(RegisterNewVehicleRequest validatedUserData, Person person, string registrationNumber)
        {
            Vehicle vehicle = new Vehicle(registrationNumber, validatedUserData.VehicleType, validatedUserData.Make, validatedUserData.Model,
                                            validatedUserData.EngineNumber, validatedUserData.MotorEmissionType, validatedUserData.FirstRegistrationDate,
                                            validatedUserData.NumberOfSeats, validatedUserData.Color, validatedUserData.MassInService, validatedUserData.MaxMass,
                                            validatedUserData.BrakedTrailer, validatedUserData.UnbrakedTrailer, person.Hash);

            persistentVehicleGateway.SaveVehicle(vehicle);
           
            RegisterNewVehicleResponse registerNewVehicleResponse = new RegisterNewVehicleResponse();
            registerNewVehicleResponse.RegistrationNumber = registrationNumber;
            return registerNewVehicleResponse;

        }
    }
}
