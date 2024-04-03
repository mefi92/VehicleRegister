using Core.Mappers;
using Entity;
using BoundaryHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test
{
    [TestClass]
    public class PersonMapperTest
    {
        [TestMethod]
        public void MapPersonToResponse_ValidPersonInput_ReturnsNoError()
        {
            // Arrange
            var person = new Person
            {
                FirstName = "Tibi",
                LastName = "Template",
                AdPostalCode = "5000",
                AdCity = "Budapest",
                AdStreet = "Kossuth utca",
                AdStreetNumber = "20",
            };
            
            var response = new LoadVehicleDataResponse();

            // Act
            PersonMapper.MapPersonToResponse(person, response);

            // Assert
            Assert.AreEqual(person.FirstName, response.FirstName);
            Assert.AreEqual(person.LastName, response.LastName);
            Assert.AreEqual(person.AdPostalCode, response.AdPostalCode);
            Assert.AreEqual(person.AdCity, response.AdCity);
            Assert.AreEqual(person.AdStreet, response.AdStreet);
            Assert.AreEqual(person.AdStreetNumber, response.AdStreetNumber);
        }        

        [TestMethod]
        public void MapResponseToPerson_ValidResponseInput_ReturnsNoError()
        {
            // Arrange
            var response = new LoadVehicleDataResponse
            {
                FirstName = "Tibi",
                LastName = "Template",
                AdPostalCode = "5000",
                AdCity = "Budapest",
                AdStreet = "Kossuth utca",
                AdStreetNumber = "20",
            };

            var person = new Person();

            // Act
            PersonMapper.MapResponseToPerson(response, person);

            // Assert
            Assert.AreEqual(person.FirstName, response.FirstName);
            Assert.AreEqual(person.LastName, response.LastName);
            Assert.AreEqual(person.AdPostalCode, response.AdPostalCode);
            Assert.AreEqual(person.AdCity, response.AdCity);
            Assert.AreEqual(person.AdStreet, response.AdStreet);
            Assert.AreEqual(person.AdStreetNumber, response.AdStreetNumber);
        }
    }
}
