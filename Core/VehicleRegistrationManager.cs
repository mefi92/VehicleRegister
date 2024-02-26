using BoundaryHelper;
using Entity;

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
            RegisterNewVehicleResponse response = new RegisterNewVehicleResponse();

            if (persistentVehicleGateway.IsItemInUse(validatedUserData.EngineNumber))
            { 
                ErrorData error = new ErrorData
                {
                    Message = "\nA megadott motorszámmal már regisztráltak járművet!",
                    ErrorCode = 100
                };
                response.Error = error;
            }
            else
            {
                string previousRegistrationNumber = persistentVehicleGateway.GetLatestRegNumber();
                string newRegistrationNumber = new RegistrationNumberGenerator().GetNextRegistrationNumber(previousRegistrationNumber);

                Person person = RegisterPerson(validatedUserData);
                response = RegisterVehice(validatedUserData, person, newRegistrationNumber);                
            }
            presenterManager.DisplayRegistrationResult(JsonHandler.Serialize(response));
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
           
            return new RegisterNewVehicleResponse { RegistrationNumber = registrationNumber };

        }
    }
}
