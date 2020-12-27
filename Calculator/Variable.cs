using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Variable : IExpression
    {
        int value;

        public Variable(int value = 0)
        {
            this.value = value;
        }

        public int Calculate()
        {
            return value;
        }
    }
}
