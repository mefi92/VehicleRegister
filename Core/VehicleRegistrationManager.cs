using BoundaryHelper;
using Core.Mappers;
using Core.Resources;
using Entity;

namespace Core
{
    internal class VehicleRegistrationManager
    {
        private readonly IPersistentVehicleGateway persistentVehicleGateway;
        private readonly IPersistentPersonGateway persistentPersonGateway;
        private readonly IVehicleManagerPresenterOutBoundary presenterManager;

        public VehicleRegistrationManager(IPersistentVehicleGateway persistentVehicleGateway, IPersistentPersonGateway persistentPersonGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;
            this.presenterManager = presenterManager;
            this.persistentPersonGateway = persistentPersonGateway;
        }

        public void SeparatePersonalAndVehicelData(RegisterNewVehicleRequest validatedUserData)
        {
            RegisterNewVehicleResponse response = new RegisterNewVehicleResponse();

            if (persistentVehicleGateway.IsItemInUse(validatedUserData.EngineNumber))
            {
                response.Error = ErrorCollection.EngineNumberDoesNotExist;
            }
            else
            {
                string previousRegistrationNumber = persistentVehicleGateway.GetLatestRegNumber();
                string newRegistrationNumber = new RegistrationNumberGenerator().GetNextRegistrationNumber(previousRegistrationNumber);

                Person person = RegisterPerson(validatedUserData);
                response = RegisterVehice(validatedUserData, newRegistrationNumber, person.Hash);                
            }
            presenterManager.DisplayRegistrationResult(JsonHandler.Serialize(response));
        }

        private Person RegisterPerson(RegisterNewVehicleRequest validatedUserData)
        {
            Person person = new Person();
            PersonMapper.MapResponseToPerson(validatedUserData, person);
            person.Hash = person.GenerateHash();

            string personHash = person.Hash;

            
            if (!persistentVehicleGateway.IsItemInUse(personHash))
            {
                persistentPersonGateway.SavePerson(person);
            }
            
            return person;
        }

        private RegisterNewVehicleResponse RegisterVehice(RegisterNewVehicleRequest validatedUserData, string registrationNumber, string ownerHash)
        {
            Vehicle vehicle = new Vehicle();
            VehicleMapper.MapResponseToVehicle(validatedUserData, vehicle, registrationNumber, ownerHash);
            persistentVehicleGateway.SaveVehicle(vehicle);
           
            return new RegisterNewVehicleResponse { RegistrationNumber = registrationNumber };

        }
    }
}
