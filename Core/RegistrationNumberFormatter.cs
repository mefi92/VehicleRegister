
namespace Core
{
    public static class RegistrationNumberFormatter //ebből statikust csinálnék, hogy ne kellejen példányosítani x
    {
        public static string FormatRegistrationNumber(string registrationNumber)
        {
            if (registrationNumber.Length >= 7)
            {                
                string transformedString = registrationNumber.Insert(2, ":").Insert(5, "-");
                return transformedString;
            }
            return registrationNumber;
        }

        public static string CleanRegistrationNumber(string registrationNumber)
        {
            string cleanedRegistrationNumber = new string(registrationNumber.Where(c => Char.IsLetterOrDigit(c)).ToArray());
            return cleanedRegistrationNumber;
        }
    }
}
