using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test
{
    [TestClass]
    public class RegistrationNumberGeneratorTest
    {
        [TestMethod]
        public void GetNextRegistrationNumber_FirstRegNumberUnformatted_NextRegistrationNumber()
        {
            // arrange
            string latestRegistrationNumber = "AAAA001";
            string expected = "AA:AA-002";

            // act
            string result = RegistrationNumberGenerator.GetNextRegistrationNumber(latestRegistrationNumber);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetNextRegistrationNumber_AAAALast_AAABFirstRegistrationNumber()
        {
            string latestRegistrationNumber = "AAAA999";
            string expected = "AA:AB-001";
            
            string result = RegistrationNumberGenerator.GetNextRegistrationNumber(latestRegistrationNumber);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetNextRegistrationNumber_AAAZLast_AABAFirstRegistrationNumber()
        {
            string latestRegistrationNumber = "AAAZ999";
            string expected = "AA:BA-001";

            string result = RegistrationNumberGenerator.GetNextRegistrationNumber(latestRegistrationNumber);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetNextRegistrationNumber_AZZZLast_BAAAFirstRegistrationNumber()
        {
            string latestRegistrationNumber = "AZZZ999";
            string expected = "BA:AA-001";

            string result = RegistrationNumberGenerator.GetNextRegistrationNumber(latestRegistrationNumber);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfRegistrationNumberException))]
        public void GetNextRegistrationNumber_ZZZZLast_BAAAFirstRegistrationNumber()
        {
            string lastRegistrationNumber = "ZZZZ999";

            RegistrationNumberGenerator.GetNextRegistrationNumber(lastRegistrationNumber);
        }



        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetNextRegistrationNumber_FormattedRegNumber_FormatException()
        {
            // arrange
            string formattedRegistrationNumber = "AA:AA-001";

            // act
            RegistrationNumberGenerator.GetNextRegistrationNumber(formattedRegistrationNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetNextRegistrationNumber_EmptyStringInput_ArgumentOutOfRangeException()
        {
            string formattedRegistrationNumber = "";

            RegistrationNumberGenerator.GetNextRegistrationNumber(formattedRegistrationNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNextRegistrationNumber_NullInput_NullReferenceException()
        {
            string formattedRegistrationNumber = null;

            RegistrationNumberGenerator.GetNextRegistrationNumber(formattedRegistrationNumber);
        }
    }
}
