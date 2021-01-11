using Calculator;
using Calculator.Exceptios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalculatorConsoleApplication
{
    public class InputOutputHandler
    {

        ArithmeticParser parser = new ArithmeticParser();
        IOutput output;
        Dictionary<Type, string> exceptionOutputs = new Dictionary<Type, string>()
        {
            { typeof( IncorrectArithmeticNotation), Properties.Localization.IncorrectNotation},
            { typeof( UnexpectedCharacterException), Properties.Localization.UnexpectedCharacter},
            { typeof( UnexpectedEndingException), Properties.Localization.UnexpectedEnding},
            { typeof( IncorrectBacketsException), Properties.Localization.IncorrectBackets},
            { typeof( ArgumentNullException), Properties.Localization.EmptyString},
            { typeof( DivideByZeroException), Properties.Localization.DivideByZero}
        };

        public InputOutputHandler(IOutput output)
        {
            this.output = output;
        }

        public void HandleInput(string input)
        {
            double result;
            try
            {
                output.Write($"{input} = ");
                result = parser.Parse(input);

                output.WriteLine(result.ToString());
            }
            catch (Exception exception)
            {
                output.WriteLine(exceptionOutputs[exception.GetType()]);
            }
        }

        public void HandleFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string expression in lines)
            {
                HandleInput(expression);
            }
        }
    }
}
