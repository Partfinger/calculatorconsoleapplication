using Calculator;
using Calculator.Exceptios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideByZeroTest()
        {
            string expression = " 4 / (25 - 35 + 10)";
            int expected = 1;
            ArithmeticParser parser = new ArithmeticParser();

            int actual = parser.Parse(expression);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectBacketsException))]
        public void IncorrectBacketTest()
        {
            string expression = " 4 / (25 - 35 + 10";
            int expected = 1;
            ArithmeticParser parser = new ArithmeticParser();

            int actual = parser.Parse(expression);

            Assert.AreEqual(expected, actual);
        }
    }
}
