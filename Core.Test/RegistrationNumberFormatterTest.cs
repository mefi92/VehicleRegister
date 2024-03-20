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
        public void FormatRegistrationNumber_WithShortInput_ShouldReturnUnformattedNumber()
        {
            string input = "aaa001";
            string expected = "aa:a0-01";

            string result = RegistrationNumberFormatter.FormatRegistrationNumber(input);
            Assert.AreEqual(expected, result);
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
        public void CleanRegistrationNumber_WithEmptyString_ReturnsEmptyString()
        {
            string input = "";
            string expected = "";

            string result = RegistrationNumberFormatter.CleanRegistrationNumber(input);
            Assert.AreEqual(expected, result);
        }

        
    }
}