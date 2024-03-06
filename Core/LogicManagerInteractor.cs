using BoundaryHelper;
using Core.Mappers;
using Core.VerificationObjects;
using Entity;

namespace Core
{
    public class LogicManagerInteractor : IVehicleManagerInBoundary
    {
        private IPersistentVehicleGateway persistentVehicleGateway;
        private IPersistentPersonGateway persistentPersonGateway;
        private IVehicleManagerPresenterOutBoundary presenterManager;        

        public LogicManagerInteractor(IPersistentVehicleGateway persistentVehicleGateway, IPersistentPersonGateway persistentPersonGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;
            this.persistentPersonGateway = persistentPersonGateway;

            this.presenterManager = presenterManager;
            this.persistentPersonGateway = persistentPersonGateway;
        }

        //TODO sorrendezés: mi, hol van az osztályban

        //ez miért public, amikor csak osztályon belül hívják? x
        private void ProcessUserDataForRegistration(RegisterNewVehicleRequest validatedUserData)
        {
            VehicleRegistrationManager registerManager = new VehicleRegistrationManager(persistentVehicleGateway,persistentPersonGateway, presenterManager);
            registerManager.SeparatePersonalAndVehicelData(validatedUserData);
        }

        //ez miért public, amikor csak osztályon belül hívják? x
        //a nevéből nem igazán értem, hogy mit is csinál ez a metódus, mi az a Manager? x
        private void LoadAndDisplayVehicleDetails(string registrationNumber)
        {
            Vehicle vehicle = persistentVehicleGateway.LoadVehicle(registrationNumber);
            LoadVehicleDataResponse loadVehicleDataResponse = new LoadVehicleDataResponse();

            if (vehicle != null)
            {            
                LoadVehicleDetails(vehicle, loadVehicleDataResponse);                
            }
            else
            {
                CreateRegistrationNumberNotFoundError(loadVehicleDataResponse);

            }
            presenterManager.DisplayVehicleData(JsonHandler.Serialize(loadVehicleDataResponse));
        }

        private static void CreateRegistrationNumberNotFoundError(LoadVehicleDataResponse loadVehicleDataResponse)
        {
            loadVehicleDataResponse.Error = new ErrorData
            {
                Message = "A rendszám még nem létezik.",
                ErrorCode = 1234
            };
        }

        private void LoadVehicleDetails(Vehicle vehicle, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            
            Person person = persistentPersonGateway.LoadPerson(vehicle.OwnerHash);

            LoadPersonDetails(person, loadVehicleDataResponse);

            LoadVehicleProperties(vehicle, loadVehicleDataResponse);
        }

        //ez egy tipikus mapper, lehet így is hívni, és kezelni, de mindenképpen érdemes lenne kiszervezni x
        private void LoadPersonDetails(Person person, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            PersonMapper.MapPersonToResponse(person, loadVehicleDataResponse);
        }

        //ez egy tipikus mapper, lehet így is hívni, és kezelni, de mindenképpen érdemes lenne kiszervezni x
        private void LoadVehicleProperties(Vehicle vehicle, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            VehicleMapper.MapVehicleToResponse(vehicle, loadVehicleDataResponse);           
        }

        public void LoadVehicleData(string request)
        {
            LoadVehicleDataRequest loadVehicleDataRequest = JsonHandler.Deserialize<LoadVehicleDataRequest>(request);
            LoadVehicleDataRequestValidator validator = new LoadVehicleDataRequestValidator();

            ValidatorResult validatorResult = validator.Validate(loadVehicleDataRequest);

            
            if (validatorResult.IsValid)
            {
                LoadAndDisplayVehicleDetails(loadVehicleDataRequest.RegistrationNumber);
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