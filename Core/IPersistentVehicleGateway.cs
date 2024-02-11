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

        Person LoadPerson(string hashNumber);

        void SavePerson(Person person);

        string GetLatestRegNumber();

        bool IsItemInUse(string engineNumber);
    }
}
