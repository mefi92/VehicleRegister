using BoundaryHelper;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappers
{
    public static class PersonMapper
    {
        public static void MapPersonToResponse(Person person, IPersonData response)
        {
            response.FirstName = person.FirstName;
            response.LastName = person.LastName;
            response.AdPostalCode = person.AdPostalCode;
            response.AdCity = person.AdCity;
            response.AdStreet = person.AdStreet;
            response.AdStreetNumber = person.AdStreetNumber;
        }

        public static void MapResponseToPerson(IPersonData response, Person person)
        {
            person.FirstName = response.FirstName;
            person.LastName = response.LastName;
            person.AdPostalCode = response.AdPostalCode;
            person.AdCity = response.AdCity;
            person.AdStreet = response.AdStreet;
            person.AdStreetNumber = response.AdStreetNumber;
        }
    }
}
