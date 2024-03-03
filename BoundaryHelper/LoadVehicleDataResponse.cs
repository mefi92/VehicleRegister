
namespace BoundaryHelper
{
    public class LoadVehicleDataResponse : Response
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AdPostalCode { get; set; }
        public string AdCity { get; set; }
        public string AdStreet { get; set; }
        public string AdStreetNumber { get; set; }
        public string VehicleType { get; set; }
        public string RegistrationNumber { get; set; }
        public string EngineNumber { get; set; }
        public string FirstRegistrationDate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int NumberOfSeats { get; set; }
        public string Color { get; set; }
        public int MassInService { get; set; }
        public int MaxMass { get; set; }
        public int BrakedTrailer { get; set; }
        public int UnbrakedTrailer { get; set; }
        public string MotorEmissionType { get; set; }
    }
}
