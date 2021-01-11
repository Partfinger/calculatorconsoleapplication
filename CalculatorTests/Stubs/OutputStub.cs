using CalculatorConsoleApplication;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTests.Stubs
{
    public class OutputStub : IOutput
    {
        public string output = "";
        public void Write(string text)
        {
            output += text;
        }

        public void WriteLine(string text)
        {
            output += $"{text}\n";
        }
    }
}
