using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Vehicle
    {
        public string type;
        public int registrationNumber; 
        public string engineNumber;

        public Vehicle(string vehicleType, int vehicleRegistrationNumber, string vehicleEngineNumber)
        {
            type = vehicleType;
            registrationNumber = vehicleRegistrationNumber;
            engineNumber = vehicleEngineNumber;
        }

    }
}
