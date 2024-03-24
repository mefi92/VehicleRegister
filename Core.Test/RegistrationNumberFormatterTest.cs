
namespace Core.Test
{
    [TestClass]
    public class RegistrationNumberFormatterTest
    {
        [TestMethod]
        public void FormatRegistrationNumber_WithValidFormat_ShouldReturnFormattedNumber()
        {
            string input = "aaaa001";
            string expected = "aa:aa-001";

            string result = RegistrationNumberFormatter.FormatRegistrationNumber(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FormatRegistrationNumber_WithEmptyStringInput_ShouldReturnArgumentOutOfRangeException()
        {
            string input = "";
            RegistrationNumberFormatter.FormatRegistrationNumber(input);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FormatRegistrationNumber_WithNullInput_ShouldReturnNullReferenceException()
        {
            string input = null;
            RegistrationNumberFormatter.FormatRegistrationNumber(input);
        }

        [TestMethod]
        public void CleanRegistrationNumber_WithAlphanumericString_ReturnsCleanedString()
        {           
            string input = "ABC123XYZ456";
            string expected = "ABC123XYZ456";

            string result = RegistrationNumberFormatter.CleanRegistrationNumber(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CleanRegistrationNumber_WithSpecialCharacters_ReturnsOnlyAlphanumericCharacters()
        {
            string input = "A!@B#C$1%2^3&X*()YZ456";
            string expected = "ABC123XYZ456";

            string result = RegistrationNumberFormatter.CleanRegistrationNumber(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]        
        public void CleanRegistrationNumber_WithEmptyStringInput_ShouldReturnEmptyString()
        {
            string input = "";
            string expected = "";

            string result = RegistrationNumberFormatter.CleanRegistrationNumber(input);

            Assert.AreEqual(expected, input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CleanRegistrationNumber_WithNullInput_ShouldReturnArgumentNullException()
        {
            string input = null;

            RegistrationNumberFormatter.CleanRegistrationNumber(input);
        }        
    }
}