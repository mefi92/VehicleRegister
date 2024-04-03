using BoundaryHelper;
using Core.Exceptions;
using Core.Interfaces;
using Core.Resources;
using Core.VerificationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core
{
    public class RegisterVehicleProcess
    {
        private readonly IPersistentVehicleGateway persistentVehicleGateway;
        private readonly IPersistentPersonGateway persistentPersonGateway;
        private readonly IVehicleManagerPresenterOutBoundary presenterManager;

        public RegisterVehicleProcess(IPersistentVehicleGateway persistentVehicleGateway, IPersistentPersonGateway persistentPersonGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;
            this.persistentPersonGateway = persistentPersonGateway;
            this.presenterManager = presenterManager;
        }

        public void ProcessVehicleRegistrationRequest(string request)
        {
            RegisterNewVehicleRequest registerNewVehicleRequest = JsonHandler.Deserialize<RegisterNewVehicleRequest>(request);

            ValidatorResult validatorResult = RegisterNewVehicleRequestValidator.Validate(registerNewVehicleRequest);

            RegisterNewVehicleResponse vehicleResponse = new RegisterNewVehicleResponse();

            if (validatorResult.IsValid)
            {
                try
                {
                    ProcessUserDataForRegistration(registerNewVehicleRequest);
                }
                catch (OutOfRegistrationNumberException)
                {
                    vehicleResponse.Error = ErrorCollection.OutOfRegistrationNumber;
                    presenterManager.DisplayRegistrationResult(JsonHandler.Serialize(vehicleResponse));
                }
                
            }
            else
            {
                foreach (ErrorData error in validatorResult.Errors)
                {
                    vehicleResponse.Error = error;

                    presenterManager.DisplayRegistrationResult(JsonHandler.Serialize(vehicleResponse));
                }
            }
        }
        private void ProcessUserDataForRegistration(RegisterNewVehicleRequest validatedUserData)
        {
            VehicleRegistrationManager registerManager = new VehicleRegistrationManager(persistentVehicleGateway, persistentPersonGateway, presenterManager);
            registerManager.SeparatePersonalAndVehicelData(validatedUserData);
        }
    }
}
