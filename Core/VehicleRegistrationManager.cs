using BoundaryHelper;
using Entity;

namespace Core
{
    internal class VehicleRegistrationManager
    {
        private IPersistentVehicleGateway persistentVehicleGateway;
        IPersistentPersonGateway persistentPersonGateway;
        private IVehicleManagerPresenterOutBoundary presenterManager;

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
                ErrorData error = new ErrorData
                {
                    //értem, hogy majd a megjelenítéshez kell a soremelés az elején, de ennek nem itt van a felelőssége
                    //itt csak tiszta szöveget kellene berakni, hogy formázva hogy fog kikerülni a felületre az más tészta
                    //másik: nem kellene beégetni a kódba ezeket, majd beszéljünk róla!
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
            //ez is itt egyfajta map, nem biztos, hogy itt szerencsés
            Person person = new Person(validatedUserData.FirstName, validatedUserData.LastName, validatedUserData.AdPostalCode,
                                        validatedUserData.AdCity, validatedUserData.AdStreet, validatedUserData.AdStreetNumber);
            
            string personHash = person.Hash;

            
            if (!persistentVehicleGateway.IsItemInUse(personHash))
            {
                persistentPersonGateway.SavePerson(person);
            }
            
            return person;
        }

        private RegisterNewVehicleResponse RegisterVehice(RegisterNewVehicleRequest validatedUserData, Person person, string registrationNumber)
        {
            //ez is itt egyfajta map, nem biztos, hogy itt szerencsés
            Vehicle vehicle = new Vehicle(registrationNumber, validatedUserData.VehicleType, validatedUserData.Make, validatedUserData.Model,
                                            validatedUserData.EngineNumber, validatedUserData.MotorEmissionType, validatedUserData.FirstRegistrationDate,
                                            validatedUserData.NumberOfSeats, validatedUserData.Color, validatedUserData.MassInService, validatedUserData.MaxMass,
                                            validatedUserData.BrakedTrailer, validatedUserData.UnbrakedTrailer, person.Hash);

            persistentVehicleGateway.SaveVehicle(vehicle);
           
            return new RegisterNewVehicleResponse { RegistrationNumber = registrationNumber };

        }
    }
}
