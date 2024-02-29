using BoundaryHelper;
using System.Text.RegularExpressions;

namespace Core.VerificationObjects
{
    //statikus?
    internal class RegisterNewVehicleRequestValidator
    {
        //ezt nem biztos, hogy ide égetném be
        private const int MaxFirstNameLength = 30;
        private const int MaxLastNameLength = 30;

        private const int ErrorInvalidFirstNameCode = 201;
        private const int ErrorInvalidLastNameCode = 202;
                
        private const int ErrorInvalidPostalCodeCode = 301;

        private const int ErrorVehicleTypeCode = 401;
        private const int ErrorInvalidEngineNumberCode = 402;        

        public ValidatorResult Validate(RegisterNewVehicleRequest request)
        {
            ValidatorResult result = new ValidatorResult();

            ValidateFirstName(request.FirstName, result);
            ValidateLastName(request.LastName, result);
            ValidateEngineNumber(request.EngineNumber, result);
            ValidateVehicleType(request.VehicleType, result);

            return result;
        }

        // TODO: TBC

        private void ValidateFirstName(string firstName, ValidatorResult result)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                result.Errors.Add(CreateError("\nA keresztnév nem lehet null vagy üres!", ErrorInvalidFirstNameCode));
            }

            if (firstName.Length > MaxFirstNameLength)
            {
                result.Errors.Add(CreateError($"\nA keresztnév nem lehet hosszabb {MaxFirstNameLength} karakternél!", ErrorInvalidFirstNameCode));
            }

            if (!IsAllLetters(firstName))
            {
                result.Errors.Add(CreateError("\nA keresztvén csak betűkből állhat.", ErrorInvalidFirstNameCode));
            }
        }

        private void ValidateLastName(string lastName, ValidatorResult result)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                result.Errors.Add(CreateError("\nA vezetéknév nem lehet null vagy üres!", ErrorInvalidLastNameCode));
            }

            if (lastName.Length > MaxLastNameLength)
            {
                result.Errors.Add(CreateError($"\nA vezetéknév nem lehet hosszabb {MaxLastNameLength} karakternél!", ErrorInvalidLastNameCode));
            }

            if (!IsAllLetters(lastName))
            {
                result.Errors.Add(CreateError("\nA vezetéknév csak betűkből állhat.", ErrorInvalidLastNameCode));
            }
        }

        private void ValidatePostalCode(string postalCode, ValidatorResult result)
        {
            string pattern = @"^\d{4}$";
            if (!PatternChecker(pattern, postalCode))
            {
                result.Errors.Add(CreateError("\nHibás irányítószám formátum.", ErrorInvalidPostalCodeCode));
            }
        }

        private void ValidateVehicleType(string vehicleType, ValidatorResult result)
        {
            string[] validInputs = { "M1", "N1", "N2", "N3", "O1", "O2", "O3", "L3E" };
            if (!Array.Exists(validInputs, validInput => string.Equals(validInput, vehicleType, StringComparison.OrdinalIgnoreCase)))
            {
                result.Errors.Add(CreateError("\nNem létező jármű kategória!\nVálassz az alábbiak közül: M1, N1, N2, N3, O1, O2, O3, L3E", ErrorVehicleTypeCode));
            }
        }

        private void ValidateEngineNumber(string engineNumber, ValidatorResult result)
        {
            string pattern = @"^[A-Z]{2}\d{12}$";
            if (!PatternChecker(pattern, engineNumber))
            {
                result.Errors.Add(CreateError("\nHibás motorszám formátum! Két betű, majd tizenkét szám. Pl: AB123456789000", ErrorInvalidEngineNumberCode));
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

        private static bool IsAllLetters(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
