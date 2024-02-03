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
        Vehicle loadVehicle(string carRegistrationNumber);
        void saveVehicle(Vehicle vehicle);
        string? GetLatestRegNumber();

        bool IsExistsEngineNumber(string engineNumber);
    }
}
