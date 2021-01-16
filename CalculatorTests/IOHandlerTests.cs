using CalculatorConsoleApplication;
using CalculatorTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTests
{
    [TestClass]
    public class IOHandlerTests
    {
        [TestMethod]
        public void TestWritingInFile()
        {
            string path = "Files\\expressions.txt";
            string expected = $"1+2*(3+2) = 11\n1+2+4 = 7\n2+15/3+4*2 = 15\n";
            OutputStub output = new OutputStub();
            InputOutputHandler handler = new InputOutputHandler(output);

            handler.HandleFile(path);

            Assert.AreEqual(expected, output.output);
        }
    }
}
