using Core.MessageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class InputVerificationHandler
    {
        private string inputData;

        public InputVerificationHandler(string input)
        {
            inputData = input;
        }

        public ErrorData ValidateDataFormat()
        {
            string ez = inputData;
            return new ErrorData();
        }

        public bool ValidateRegistrationNumber(string registrationNumber)
        {
            return true;
        }

        public bool ValidateVehicleType(string vehicleType) 
        {
            return true;
        }

        public bool ValidateEningineNumber(string engineNumber)
        {
            return true;
        }
    }

}
