using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IVehicleManagerInBoundary
    { 
        void ProcessTrafficMessage(string message);

        void LoadVehicleData(string registrationNumberRequest);
    }
}
