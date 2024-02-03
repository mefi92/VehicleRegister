using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IVehicleManagerInBoundary
    {
        void registerNewVehicle(string vehicleType, string engineNumber);

        void LoadVehicle(string registrationNumber);

        void ProcessTrafficMessage(string message);
    }
}
