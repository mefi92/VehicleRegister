using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Test
{
    [TestClass]
    public class PersonFilePersistenceManagerTest
    {
        [TestMethod]
        public void SavePerson_PersonInput_ReturnsNoError()
        {
            // Arrange
            PersonFilePersistenceManager personFilePersistenceManager = new PersonFilePersistenceManager();

            var person = new Person
            {
                FirstName = "Tibi",
                LastName = "Template",
                AdPostalCode = "5000",
                AdCity = "Budapest",
                AdStreet = "Kossuth utca",
                AdStreetNumber = "20",
            };

            string filePathWithExtension = person.Hash + ".txt";
            string filePathWoExtension = person.Hash;

            try
            {
                // Act
                personFilePersistenceManager.SavePerson(person);
                Person testPerson = personFilePersistenceManager.LoadPerson(filePathWoExtension);


                // Assert
                Assert.AreEqual(person.FirstName, testPerson.FirstName);
                Assert.AreEqual(person.LastName, testPerson.LastName);
                Assert.AreEqual(person.AdPostalCode, testPerson.AdPostalCode);
                Assert.AreEqual(person.AdCity, testPerson.AdCity);
                Assert.AreEqual(person.AdStreet, testPerson.AdStreet);
                Assert.AreEqual(person.AdStreetNumber, testPerson.AdStreetNumber);
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePathWithExtension))
                {
                    File.Delete(filePathWithExtension);
                }
            }


        }
    }
}
