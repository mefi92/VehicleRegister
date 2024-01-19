using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
 

namespace Core
{
    internal interface PersistentVehicleGateway
    {
        Vehicle loadVehicle(string carRegistrationNumber);
        void saveVehicle(Vehicle vehicle);
         
    }
}
