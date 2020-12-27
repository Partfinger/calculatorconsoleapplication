using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Operations
{
    public abstract class ArithmeticOperation : IExpression
    {
        protected IExpression leftOperand, rightOperand;
        public abstract int Calculate();
    }
}
