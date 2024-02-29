using BoundaryHelper;
using Core.VerificationObjects;
using Entity;

namespace Core
{
    public class LogicManagerInteractor : IVehicleManagerInBoundary
    {
        private IPersistentVehicleGateway persistentVehicleGateway;
        private IVehicleManagerPresenterOutBoundary presenterManager;

        public LogicManagerInteractor(IPersistentVehicleGateway persistentVehicleGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;

            this.presenterManager = presenterManager;
        }

        //TODO sorrendezés: mi, hol van az osztályban

        //ez miért public, amikor csak osztályon belül hívják?
        public void ProcessUserDataForRegistration(RegisterNewVehicleRequest validatedUserData)
        {
            VehicleRegistrationManager registerManager = new VehicleRegistrationManager(persistentVehicleGateway, presenterManager);
            registerManager.SeparatePersonalAndVehicelData(validatedUserData);
        }

        //ez miért public, amikor csak osztályon belül hívják?
        //a nevéből nem igazán értem, hogy mit is csinál ez a metódus, mi az a Manager?
        public void LoadVehicleManager(string registrationNumber)
        {
            Vehicle vehicle = persistentVehicleGateway.LoadVehicle(registrationNumber);
            LoadVehicleDataResponse loadVehicleDataResponse = new LoadVehicleDataResponse();  
            
            LoadVehicleDetails(vehicle, loadVehicleDataResponse);
            presenterManager.DisplayVehicleData(JsonHandler.Serialize(loadVehicleDataResponse));
        }


        private void LoadVehicleDetails(Vehicle vehicle, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            Person person = persistentVehicleGateway.LoadPerson(vehicle.OwnerHash);

            LoadPersonDetails(person, loadVehicleDataResponse);

            LoadVehicleProperties(vehicle, loadVehicleDataResponse);
        }

        //ez egy tipikus mapper, lehet így is hívni, és kezelni, de mindenképpen érdemes lenne kiszervezni
        private void LoadPersonDetails(Person person, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            loadVehicleDataResponse.FirstName = person.FirstName;
            loadVehicleDataResponse.LastName = person.LastName;
            loadVehicleDataResponse.AdPostalCode = person.AdPostalCode;
            loadVehicleDataResponse.AdCity = person.AdCity;
            loadVehicleDataResponse.AdStreet = person.AdStreet;
            loadVehicleDataResponse.AdStreetNumber = person.AdStreetNumber;
        }

        //ez egy tipikus mapper, lehet így is hívni, és kezelni, de mindenképpen érdemes lenne kiszervezni
        private void LoadVehicleProperties(Vehicle vehicle, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            loadVehicleDataResponse.VehicleType = vehicle.VehicleType;
            loadVehicleDataResponse.RegistrationNumber = vehicle.RegistrationNumber;
            loadVehicleDataResponse.EngineNumber = vehicle.EngineNumber;
            loadVehicleDataResponse.FirstRegistrationDate = vehicle.FirstRegistrationDate;
            loadVehicleDataResponse.Make = vehicle.Make;
            loadVehicleDataResponse.Model = vehicle.Model;
            loadVehicleDataResponse.NumberOfSeats = vehicle.NumberOfSeats;
            loadVehicleDataResponse.Color = vehicle.Color;
            loadVehicleDataResponse.MassInService = vehicle.MassInService;
            loadVehicleDataResponse.MaxMass = vehicle.MaxMass;
            loadVehicleDataResponse.BrakedTrailer = vehicle.BrakedTrailer;
            loadVehicleDataResponse.UnbrakedTrailer = vehicle.UnbrakedTrailer;
            loadVehicleDataResponse.MotorEmissionType = vehicle.MotorEmissionType;
        }

        public void LoadVehicleData(string request)
        {
            LoadVehicleDataRequest loadVehicleDataRequest = JsonHandler.Deserialize<LoadVehicleDataRequest>(request);
            LoadVehicleDataRequestValidator validator = new LoadVehicleDataRequestValidator();

            ValidatorResult validatorResult = validator.Validate(loadVehicleDataRequest);

            
            if (validatorResult.IsValid)
            {
                LoadVehicleManager(loadVehicleDataRequest.RegistrationNumber);
            }
            else
            {
                LoadVehicleDataResponse vehicleResponse = new LoadVehicleDataResponse();

                foreach (ErrorData error in validatorResult.Errors)
                {
                    vehicleResponse.Error = error;

                    presenterManager.DisplayRegistrationResult(JsonHandler.Serialize(vehicleResponse));
                }
            }
        }

        public void RegisterNewVehicle(string request)
        {
            RegisterNewVehicleRequest registerNewVehicleRequest = JsonHandler.Deserialize<RegisterNewVehicleRequest>(request);
            RegisterNewVehicleRequestValidator validator = new RegisterNewVehicleRequestValidator();

            ValidatorResult validatorResult = validator.Validate(registerNewVehicleRequest);

            if (validatorResult.IsValid)
            {
                ProcessUserDataForRegistration(registerNewVehicleRequest);
            }
            else
            {
                RegisterNewVehicleResponse vehicleResponse = new RegisterNewVehicleResponse();

                foreach (ErrorData error in validatorResult.Errors)
                {
                    vehicleResponse.Error = error;

                    presenterManager.DisplayRegistrationResult(JsonHandler.Serialize(vehicleResponse));
                }
            }

        }
    }
}