namespace BoundaryHelper
{
    public class RegistrationNumberRequest
    {
        public string RegistrationNumber { get; set; }


        //TODO kiszervezni ezt a két metódust, általánostani
        public static string GetRegistraionNumberRequestInJson(RegistrationNumberRequest request)
        {
            //Json-ben konvertálás: Newtonsoft json
            throw new NotImplementedException();
        }

        public static RegistrationNumberRequest GetRegistrationNumberRequestInObject(string registrationNumberRequest)
        {
            throw new NotImplementedException();
        }
    }
}
