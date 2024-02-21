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

        public void SeparatePersonalAndVehicelData(VehicleRegistrationInfo validatedUserData)
        {
            RegisterNewVehicleRequest registerNewVehicleRequest = new RegisterNewVehicleRequest();

            if (persistentVehicleGateway.IsItemInUse(validatedUserData.EngineNumber))
            { 
                registerNewVehicleRequest.Error.Message = "A megadott motorszámmal már regisztráltak járművet!";
                registerNewVehicleRequest.Error.ErrorCode = 100;
            }
            else
            {
                string previousRegistrationNumber = persistentVehicleGateway.GetLatestRegNumber();
                string newRegistrationNumber = new RegistrationNumberGenerator().GetNextRegistrationNumber(previousRegistrationNumber);

                Person person = RegisterPerson(validatedUserData);
                registerNewVehicleRequest = RegisterVehice(validatedUserData, person, newRegistrationNumber);
            }

            //presenterManager.displayMessage(JsonHandler.Serialize(registerNewVehicleRequest));
            presenterManager.DisplayRegistrationResult(JsonHandler.Serialize(registerNewVehicleRequest));
        }

        private Person RegisterPerson(VehicleRegistrationInfo validatedUserData)
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

        private RegisterNewVehicleRequest RegisterVehice(VehicleRegistrationInfo validatedUserData, Person person, string registrationNumber)
        {
            Vehicle vehicle = new Vehicle(registrationNumber, validatedUserData.VehicleType, validatedUserData.Make, validatedUserData.Model,
                                            validatedUserData.EngineNumber, validatedUserData.MotorEmissionType, validatedUserData.FirstRegistrationDate,
                                            validatedUserData.NumberOfSeats, validatedUserData.Color, validatedUserData.MassInService, validatedUserData.MaxMass,
                                            validatedUserData.BrakedTrailer, validatedUserData.UnbrakedTrailer, person.Hash);

            persistentVehicleGateway.SaveVehicle(vehicle);
           
            RegisterNewVehicleRequest vehicleReg = new RegisterNewVehicleRequest();
            vehicleReg.RegistrationNumber = registrationNumber;
            return vehicleReg;

            /*
            vehicleReg.FirstName = person.FirstName;
            vehicleReg.LastName = person.LastName;
            vehicleReg.AdPostalCode = person.AdPostalCode;
            vehicleReg.AdCity = person.AdCity;
            vehicleReg.AdStreet = person.AdStreet;
            vehicleReg.AdStreetNumber = person.AdStreetNumber;
            vehicleReg.VehicleType = vehicle.VehicleType;
            vehicleReg.RegistrationNumber = registrationNumber;
            vehicleReg.EngineNumber = vehicle.EngineNumber;
            vehicleReg.FirstRegistrationDate = vehicle.FirstRegistrationDate;
            vehicleReg.Make = vehicle.Make;
            vehicleReg.Model = vehicle.Model;
            vehicleReg.NumberOfSeats = vehicle.NumberOfSeats;
            vehicleReg.Color = vehicle.Color;
            vehicleReg.MassInService = vehicle.MassInService;
            vehicleReg.MaxMass = vehicle.MaxMass;
            vehicleReg.BrakedTrailer = vehicle.BrakedTrailer;
            vehicleReg.UnbrakedTrailer = vehicle.UnbrakedTrailer;
            vehicleReg.MotorEmissionType = vehicle.MotorEmissionType;
            */
        }
    }
}
