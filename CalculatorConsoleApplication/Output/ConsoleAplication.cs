using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorConsoleApplication.Output
{
    public class ConsoleAplication : IOutput
    {
        public void Write(string output)
        {
            Console.Write(output);
        }

        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
