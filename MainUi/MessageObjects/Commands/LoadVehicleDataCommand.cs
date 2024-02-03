using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUi.MessageObjects.Commands
{
    internal class LoadVehicleDataCommand
    {
        public string VehicleType { get; set; }
        
        public string EngineNumber { get; set; }

        public string RegistrationNumber { get; set; }

    }
}
