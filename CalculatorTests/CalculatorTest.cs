using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void OrderOfOperationTest()
        {
            string expression = "2 + 2 * 2";
            int expected = 6;
            ArithmeticParser parser = new ArithmeticParser();

            int actual = parser.Parse(expression);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BacketsTest()
        {
            string expression = "714 + 22 - 14((24 - 1)5 + 22 * 16(3(12(13 - 17)(5 + 12))))12";
            int expected = 144746344;
            ArithmeticParser parser = new ArithmeticParser();

            int actual = parser.Parse(expression);

            Assert.AreEqual(expected, actual);
        }
    }
}
