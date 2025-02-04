﻿using BoundaryHelper;
using Core.Interfaces;
using Core.Mappers;
using Core.Resources;
using Core.VerificationObjects;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class LoadVehicleProcess
    {
        private readonly IPersistentVehicleGateway persistentVehicleGateway;
        private readonly IPersistentPersonGateway persistentPersonGateway;
        private readonly IVehicleManagerPresenterOutBoundary presenterManager;

        public LoadVehicleProcess(IPersistentVehicleGateway persistentVehicleGateway, IPersistentPersonGateway persistentPersonGateway, IVehicleManagerPresenterOutBoundary presenterManager)
        {
            this.persistentVehicleGateway = persistentVehicleGateway;
            this.persistentPersonGateway = persistentPersonGateway;
            this.presenterManager = presenterManager;
        }

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
                loadVehicleDataResponse.Error = ErrorCollection.RegistrationNumberDoesNotExist;
            }
            presenterManager.DisplayVehicleData(JsonHandler.Serialize(loadVehicleDataResponse));
        }

        private void LoadVehicleDetails(Vehicle vehicle, LoadVehicleDataResponse loadVehicleDataResponse)
        {

            Person person = persistentPersonGateway.LoadPerson(vehicle.OwnerHash);

            LoadPersonDetails(person, loadVehicleDataResponse);

            LoadVehicleProperties(vehicle, loadVehicleDataResponse);
        }

        private void LoadPersonDetails(Person person, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            PersonMapper.MapPersonToResponse(person, loadVehicleDataResponse);
        }

        private void LoadVehicleProperties(Vehicle vehicle, LoadVehicleDataResponse loadVehicleDataResponse)
        {
            VehicleMapper.MapVehicleToResponse(vehicle, loadVehicleDataResponse);
        }

        public void ProcessLoadVehicleRequest(string request)
        {
            LoadVehicleDataRequest loadVehicleDataRequest = JsonHandler.Deserialize<LoadVehicleDataRequest>(request);

            ValidatorResult validatorResult = LoadVehicleDataRequestValidator.Validate(loadVehicleDataRequest);

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
    }
}
