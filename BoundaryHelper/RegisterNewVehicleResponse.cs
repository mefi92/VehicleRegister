
namespace BoundaryHelper
{
    //ebben és a LoadVehicle-ben is van Error, ez ki lehetne emelni egy közös ősbe
    public class RegisterNewVehicleResponse
    {
        public string RegistrationNumber { get; set; }
        public ErrorData Error { get; set; }
    }
}
