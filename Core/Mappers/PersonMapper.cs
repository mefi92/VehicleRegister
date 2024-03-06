using BoundaryHelper;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappers
{
    public class PersonMapper
    {
        public static void MapPersonToResponse(Person person, LoadVehicleDataResponse response)
        {
            response.FirstName = person.FirstName;
            response.LastName = person.LastName;
            response.AdPostalCode = person.AdPostalCode;
            response.AdCity = person.AdCity;
            response.AdStreet = person.AdStreet;
            response.AdStreetNumber = person.AdStreetNumber;
        }
    }
}
