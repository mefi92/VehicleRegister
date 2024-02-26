using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IVehicleManagerPresenterOutBoundary
    {  
        void DisplayVehicleData(string vehicleDataResponse);

        void DisplayRegistrationResult(string registrationResultResponse);
    }
}
