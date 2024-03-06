
namespace BoundaryHelper
{
    public interface IVehicleData
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string AdPostalCode { get; set; }
        string AdCity { get; set; }
        string AdStreet { get; set; }
        string AdStreetNumber { get; set; }
        string VehicleType { get; set; }
        string EngineNumber { get; set; }
        string FirstRegistrationDate { get; set; }
        string Make { get; set; }
        string Model { get; set; }
        int NumberOfSeats { get; set; }
        string Color { get; set; }
        int MassInService { get; set; }
        int MaxMass { get; set; }
        int BrakedTrailer { get; set; }
        int UnbrakedTrailer { get; set; }
        string MotorEmissionType { get; set; }
    }
}
