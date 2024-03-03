using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication.Verification
{
    
    public class InputVerification
    {
        public static bool IsValidRegistrationNumber(string registrationNumber)
        {
            string pattern = @"^[A-Z]{4}\d{3}$";
            if (!PatternChecker(pattern, registrationNumber))
            {
                return false;                
            }
            return true;
        }

        private static bool PatternChecker(string pattern, string data)
        {
            return !string.IsNullOrEmpty(data) && Regex.IsMatch(data, pattern);
        }
    }
}
