using BoundaryHelper;
using Core.Resources;
using System.Runtime.CompilerServices;

using System.Text.RegularExpressions;
[assembly: InternalsVisibleTo("Core.Test")]

namespace Core.VerificationObjects
{
    internal static class LoadVehicleDataRequestValidator
    {
        public static ValidatorResult Validate(LoadVehicleDataRequest request)
        {
            ValidatorResult result = new ValidatorResult();

            request.RegistrationNumber = TryConvertStringToUppercase(request.RegistrationNumber);

            ValidateRegistrationNumber(request.RegistrationNumber, result);
            return result;
        }

        private static void ValidateRegistrationNumber(string registrationNumber, ValidatorResult result)
        {
            //ABCD012
            string pattern = @"^[A-Z]{4}\d{3}$";
            if (!PatternChecker(pattern, registrationNumber))
            {
                result.Errors.Add(ErrorCollection.InvalidRegistrationNumber);
            }
        }

        private static bool PatternChecker(string pattern, string data)
        {
            return !string.IsNullOrEmpty(data) && Regex.IsMatch(data, pattern);
        }

        private static string TryConvertStringToUppercase(string input)
        {
            try { return input.ToUpper(); } catch { return input; }
        }
    }
}
