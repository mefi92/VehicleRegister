using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    internal class Vehicle
    {
        public string type;
        public string registrationNumber; 
        public string engineNumber;

        public Vehicle(string vehicleType, string vehicleRegistrationNumber, string vehicleEngineNumber)
        {
            type = vehicleType;
            registrationNumber = vehicleRegistrationNumber;
            engineNumber = vehicleEngineNumber;
        }

    }
}
