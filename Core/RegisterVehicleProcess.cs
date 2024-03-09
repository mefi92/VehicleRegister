using BoundaryHelper;
using Core.Interfaces;
using Core.VerificationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private void ProcessUserDataForRegistration(RegisterNewVehicleRequest validatedUserData)
        {
            VehicleRegistrationManager registerManager = new VehicleRegistrationManager(persistentVehicleGateway, persistentPersonGateway, presenterManager);
            registerManager.SeparatePersonalAndVehicelData(validatedUserData);
        }


    }


}
