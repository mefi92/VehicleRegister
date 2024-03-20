
namespace Core
{
    public static class RegistrationNumberFormatter
    {
        public static string FormatRegistrationNumber(string registrationNumber)
        {
            return registrationNumber.Insert(2, ":").Insert(5, "-"); ;
        }

        public static string CleanRegistrationNumber(string registrationNumber)
        {
            string cleanedRegistrationNumber = new string(registrationNumber.Where(c => Char.IsLetterOrDigit(c)).ToArray());
            return cleanedRegistrationNumber;
        }
    }
}
