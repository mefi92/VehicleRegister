using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RegistrationNumberFormatter
    {
        public string FormatRegistrationNumber(string registrationNumber)
        {
            if (registrationNumber.Length >= 7)
            {                
                string transformedString = registrationNumber.Insert(2, ":").Insert(5, "-");
                return transformedString;
            }
            return registrationNumber;
        }

        public string CleanRegistrationNumber(string registrationNumber)
        {
            string cleanedRegistrationNumber = new string(registrationNumber.Where(c => Char.IsLetterOrDigit(c)).ToArray());
            return cleanedRegistrationNumber;
        }
    }
}
