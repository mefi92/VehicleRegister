using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;


namespace Core
{
    public interface IPersistentVehicleGateway
    {
        Vehicle LoadVehicle(string carRegistrationNumber);
        void SaveVehicle(Vehicle vehicle);
        string GetLatestRegNumber();

        bool IsEngineNumberInUse(string engineNumber);
    }
}
