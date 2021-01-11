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
            double expected = 6;
            ArithmeticParser parser = new ArithmeticParser();

            double actual = parser.Parse(expression);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BacketsTest()
        {
            string expression = "(2 + 2) * 2";
            double expected = 8;
            ArithmeticParser parser = new ArithmeticParser();

            double actual = parser.Parse(expression);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideByZeroTest()
        {
            string expression = " 4 / (25 - 35 + 10)";
            ArithmeticParser parser = new ArithmeticParser();

            double actual = parser.Parse(expression);
        }

        [TestMethod]
        public void DecimalDigitsTest()
        {
            string expression = " 4 / (.03 + .07)";
            double expected = 40;
            ArithmeticParser parser = new ArithmeticParser();

            double actual = parser.Parse(expression);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectBacketsException))]
        public void IncorrectBacketTest()
        {
            string expression = " 4 / (25 - 35 + 10";
            ArithmeticParser parser = new ArithmeticParser();

            double actual = parser.Parse(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedCharacterException))]
        public void UnexpectedCharacterTest()
        {
            string expression = " 4 * x + 2";
            ArithmeticParser parser = new ArithmeticParser();

            double actual = parser.Parse(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyStringTest()
        {
            string expression = "           ";
            ArithmeticParser parser = new ArithmeticParser();

            double actual = parser.Parse(expression);
        }
    }
}
