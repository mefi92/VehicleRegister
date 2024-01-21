using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface VehicleManagerInBoundary
    {
        void registerNewVehicle(string vehicleType, string engineNumber);
    }
}
