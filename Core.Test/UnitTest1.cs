namespace Core.Test
{
    [TestClass]
    public class RegistrationNumberFormatterTest
    {
        [TestMethod]
        public void FormatRegistrationNumber1()
        {
            string result = RegistrationNumberFormatter.FormatRegistrationNumber("aaaa001");
            Assert.AreEqual("aa:aa-001", result);
        }

        [TestMethod]
        public void FormatRegistrationNumber2()
        {
            string result = RegistrationNumberFormatter.FormatRegistrationNumber("aaa001");
            Assert.AreEqual("aaa001", result);
        }
    }
}