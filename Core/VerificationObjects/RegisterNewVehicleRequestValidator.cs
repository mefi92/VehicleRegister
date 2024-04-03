using BoundaryHelper;
using Core.Resources;
using System.Runtime.CompilerServices;

using System.Text.RegularExpressions;
[assembly: InternalsVisibleTo("Core.Test")]

namespace Core.VerificationObjects
{

    internal static class RegisterNewVehicleRequestValidator
    {
        public static ValidatorResult Validate(RegisterNewVehicleRequest request)
        {
            ValidatorResult result = new ValidatorResult();

            request.FirstName = TryConvertStringToUppercase(request.FirstName);
            request.LastName = TryConvertStringToUppercase(request.LastName);
            request.EngineNumber = TryConvertStringToUppercase(request.EngineNumber);
            request.VehicleType = TryConvertStringToUppercase(request.VehicleType);

            ValidateFirstName(request.FirstName, result);
            ValidateLastName(request.LastName, result);
            ValidatePostalCode(request.AdPostalCode, result);
            ValidateEngineNumber(request.EngineNumber, result);
            ValidateVehicleType(request.VehicleType, result);            

            return result;
        }

        // TODO: TBC

        private static void ValidateFirstName(string firstName, ValidatorResult result)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                result.Errors.Add(ErrorCollection.InvalidFirstNameNull);
            }

            if (firstName.Length > ConstantCollection.MaxFirstNameLength)
            {
                result.Errors.Add(ErrorCollection.InvalidFirstNameLength);
            }

            if (!IsAllLetters(firstName))
            {
                result.Errors.Add(ErrorCollection.InvalidFirstNameChar);
            }
        }

        private static void ValidateLastName(string lastName, ValidatorResult result)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                result.Errors.Add(ErrorCollection.InvalidLastNameNull);
            }

            if (lastName.Length > ConstantCollection.MaxLastNameLength)
            {
                result.Errors.Add(ErrorCollection.InvalidLastNameLength);
            }

            if (!IsAllLetters(lastName))
            {
                result.Errors.Add(ErrorCollection.InvalidLastNameChar);
            }
        }

        private static void ValidatePostalCode(string postalCode, ValidatorResult result)
        {
            string pattern = @"^[1-9]\d{3}$";
            if (!PatternChecker(pattern, postalCode))
            {
                result.Errors.Add(ErrorCollection.InvalidPostalCode);
            }
        }

        private static void ValidateVehicleType(string vehicleType, ValidatorResult result)
        {
            string[] validInputs = ["M1", "N1", "N2", "N3", "O1", "O2", "O3", "L3E"];
            if (!Array.Exists(validInputs, validInput => string.Equals(validInput, vehicleType, StringComparison.OrdinalIgnoreCase)))
            {
                result.Errors.Add(ErrorCollection.InvalidVehicleType);
            }
        }

        private static void ValidateEngineNumber(string engineNumber, ValidatorResult result)
        {
            string pattern = @"^[A-Z]{2}\d{12}$";
            if (!PatternChecker(pattern, engineNumber))
            {
                result.Errors.Add(ErrorCollection.InvalidEngineNumber);
            }
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

        private static string TryConvertStringToUppercase(string input)
        {
            try { return input.ToUpper(); } catch { return input; }
        }
    }
}
