using Calculator;
using Calculator.Exceptios;
using CalculatorConsoleApplication.Output;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
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
            { typeof( DivideByZeroException), Properties.Localization.DivideByZero},
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

        public bool HandleFile(string path)
        {
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
                if (lines.Length == 0)
                {
                    Console.WriteLine(string.Format(Properties.Localization.EmptyFile, path));
                    return false;
                }

                foreach (string expression in lines)
                {
                    HandleInput(expression);
                }
                return true;
            }
            catch (SecurityException)
            {
                Console.WriteLine(string.Format(Properties.Localization.SecurityException, path));
            }
            catch(Exception)
            {
                Console.WriteLine(string.Format(Properties.Localization.FileUnknownException, path));
            }
            return false;
        }
    }
}
