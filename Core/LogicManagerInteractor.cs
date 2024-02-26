using BoundaryHelper;
using Core.MessageObjects;
using Core.MessageObjects.Commands;
using Core.VerificationObjects;
using Entity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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

        public void ProcessUserDataForRegistration(RegisterNewVehicleRequest validatedUserData)
        {
            VehicleRegistrationManager registerManager = new VehicleRegistrationManager(persistentVehicleGateway, presenterManager);
            registerManager.SeparatePersonalAndVehicelData(validatedUserData);
        }

        public void LoadVehicleManager(string registrationNumber)
        {
            Vehicle vehicle = persistentVehicleGateway.LoadVehicle(registrationNumber);
            LoadVehicleDataResponse loadVehicleDataResponse = new LoadVehicleDataResponse();

            if (vehicle == null)
            {
                HandleError(loadVehicleDataResponse);
            }
            else
            {
                LoadVehicleDetails(vehicle, loadVehicleDataResponse);
            }

            presenterManager.DisplayVehicleData(JsonHandler.Serialize(loadVehicleDataResponse));
        }

        private void HandleError(LoadVehicleDataResponse loadVehicleDataResponse)
        {
            loadVehicleDataResponse.Error = new BoundaryHelper.ErrorData
            {
                Message = "A megadott rendszám nem létezik!\nPróbálja újra.",
                ErrorCode = 102
            };
        }

        private void LoadVehicleDetails(Vehicle vehicle, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            Person person = persistentVehicleGateway.LoadPerson(vehicle.OwnerHash);

            LoadPersonDetails(person, loadVehicleDataResponse);

            LoadVehicleProperties(vehicle, loadVehicleDataResponse);
        }

        private void LoadPersonDetails(Person person, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            loadVehicleDataResponse.FirstName = person.FirstName;
            loadVehicleDataResponse.LastName = person.LastName;
            loadVehicleDataResponse.AdPostalCode = person.AdPostalCode;
            loadVehicleDataResponse.AdCity = person.AdCity;
            loadVehicleDataResponse.AdStreet = person.AdStreet;
            loadVehicleDataResponse.AdStreetNumber = person.AdStreetNumber;
        }

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

        private void ErrorMessageHandler(List<int> errorCodes)
        {
            var createCommand = new CreateCommand();
            GenericCommandMessage<RegisterNewVehicleCommand> outputMessage;

            foreach (int code in errorCodes)
            {
                outputMessage = createCommand.CreateRegisterVehicleCommand(error: code);
                presenterManager.displayMessage(outputMessage.Serialize());
            }
        }
                 
        /*public void ProcessTrafficMessage(string message)
        {
            if (message == null) return;            

            var deserializedMessage = JsonConvert.DeserializeObject<dynamic>(message);            
            string command = deserializedMessage.Command;

            if (deserializedMessage.Error != null)
            {
                // Később, ha a UI felől hibát küldenénk itt le lehet kezelni.                    
            }

            switch (command)
            {
                case CommandConstants.LoadVehicleData:
                    ProcessLoadVehicleData(deserializedMessage);
                    break;
                case CommandConstants.RegisterNewVehicle:
                    VehicleRegistrationInfo userDataForRegistration = new VehicleRegistrationInfo(deserializedMessage.Data);
                    List<int> validationOutcome = userDataForRegistration.ValidateVehiceDataFormat();

                    if(validationOutcome.Count != 0)
                    {
                        ErrorMessageHandler(validationOutcome);
                        break;
                    }
                    ProcessUserDataForRegistration(userDataForRegistration);
                    break;
                default:
                    break;
            }
        }*/

        private void ProcessLoadVehicleData(dynamic deserializedMessage)
        {
            string registrationNumber = deserializedMessage.Data.RegistrationNumber;
            LoadVehicleManager(registrationNumber);
        }

        public void LoadVehicleData(string request)
        {
            LoadVehicleDataRequest registrationNumberRequest = JsonHandler.Deserialize<LoadVehicleDataRequest>(request);
            LoadVehicleManager(registrationNumberRequest.RegistrationNumber);
        }

       /* public void RegisterNewVehicle(string request)
        {
            RegisterNewVehicleRequest registerNewVehicleRequest = JsonHandler.Deserialize<RegisterNewVehicleRequest>(request);

            VehicleRegistrationInfo userDataForRegistration = new VehicleRegistrationInfo(registerNewVehicleRequest);
            List<int> validationOutcome = userDataForRegistration.ValidateVehiceDataFormat();

            // TODO: a validáció simán mehetne a reg classba és akkor már a gui -n lehetne javítani, ha valami hiba van

            if (validationOutcome.Count != 0)
            {
                ErrorMessageHandler(validationOutcome);                
            }            

            ProcessUserDataForRegistration(userDataForRegistration);

        }*/

        public static class CommandConstants
        {
            public const string RegisterNewVehicle = "register_new_vehicle";
            public const string LoadVehicleData = "load_vehicle_data";
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
                foreach (BoundaryHelper.ErrorData error in validatorResult.Errors)
                {
                    // response to ui
                }
            }

        }
    }
}