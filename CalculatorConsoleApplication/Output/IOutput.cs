using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorConsoleApplication.Output
{
    public interface IOutput
    {
        public void Write(string output);
        public void WriteLine(string output);
    }
}
