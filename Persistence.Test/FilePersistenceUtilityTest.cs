using Entity;
using Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Test
{
    [TestClass]
    public class FilePersistenceUtilityTest
    {
        [TestMethod]
        public void SaveObjectToTextFile_PersonInput_ReturnsNoError()
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

            string filePath = "unit_test.txt";

            try
            {
                // Act
                FilePersistenceUtility.SaveObjectToTextFile(filePath, person);

                // Assert
                Assert.IsTrue(File.Exists(filePath), "File was not created.");
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }            
        }

        [TestMethod]
        public void SaveObjectToTextFile_NullInput_ReturnsNoError()
        {
            // Arrange
            string? person = null;
            string filePath = "unit_test_null.txt";

            try
            {
                // Act
                FilePersistenceUtility.SaveObjectToTextFile(filePath, person);

                // Assert
                Assert.IsTrue(File.Exists(filePath), "File was not created.");
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FilePersistenceException))]
        public void SaveObjectToTextFile_NonAlphabeticPath_ReturnsFilePersistenceException()
        {
            // Arrange
            string input = "";
            string filePath = " '+!%/=().txt";

            // Act
            FilePersistenceUtility.SaveObjectToTextFile(filePath, input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SaveObjectToTextFile_EmptyStringPath_ReturnsArgumentException()
        {
            // Arrange
            string input = "";
            string filePath = "";

            // Act
            FilePersistenceUtility.SaveObjectToTextFile(filePath, input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SaveObjectToTextFile_NullPath_ReturnsArgumentException()
        {
            // Arrange
            string input = null;
            string filePath = "";

            // Act
            FilePersistenceUtility.SaveObjectToTextFile(filePath, input);
        }

        [TestMethod]
        public void LoadJsonDataFromFile_FromPrepairedFileContainsString_ReturnsNoError()
        {
            // Arrange            
            string filePath = "unit_test_load.txt";
            string textToSave = "is it valid?";

            try
            {
                // Act
                FilePersistenceUtility.SaveObjectToTextFile(filePath, textToSave);
                string loadedText = FilePersistenceUtility.LoadJsonDataFromFile<string>(filePath);

                // Assert
                Assert.AreEqual(textToSave, loadedText);
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadJsonDataFromFile_EmptyStringPath_ReturnsArgumentException()
        {
            // Arrange            
            string filePath = "";            

            // Act            
            FilePersistenceUtility.LoadJsonDataFromFile<string>(filePath);
        }
    }
}
