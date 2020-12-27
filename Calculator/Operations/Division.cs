using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Operations
{
    public class Division : ArithmeticOperation
    {
        public override int Calculate()
        {
            return leftOperand.Calculate() / rightOperand.Calculate();
        }
    }
}
