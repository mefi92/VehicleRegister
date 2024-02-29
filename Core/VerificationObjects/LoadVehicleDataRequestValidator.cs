using BoundaryHelper;
using System.Text.RegularExpressions;

namespace Core.VerificationObjects
{
    //statikus?
    internal class LoadVehicleDataRequestValidator
    {
        //ezt nem biztos, hogy ide égetném be
        private const int ErrorInvalidRegistrationNumber = 501;

        public ValidatorResult Validate(LoadVehicleDataRequest request)
        {
            ValidatorResult result = new ValidatorResult();

            ValidateRegistrationNumber(request.RegistrationNumber, result);
            return result;
        }

        private void ValidateRegistrationNumber(string registrationNumber, ValidatorResult result)
        {
            //ABCD012
            string pattern = @"^[A-Z]{4}\d{3}$";
            if (!PatternChecker(pattern, registrationNumber))
            {
                result.Errors.Add(CreateError("\nHibás rendszám formátum. Példa: AABB001", ErrorInvalidRegistrationNumber));
            }
        }

        private ErrorData CreateError(string message, int errorCode)
        {
            return new ErrorData
            {
                Message = message,
                ErrorCode = errorCode
            };
        }

        private static bool PatternChecker(string pattern, string data)
        {
            return !string.IsNullOrEmpty(data) && Regex.IsMatch(data, pattern);
        }
    }
}
