using Calculator;
using Calculator.Exceptios;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorConsoleApplication
{
    public class InputOutputHandler
    {

        ArithmeticParser parser = new ArithmeticParser();
        IOutput output;

        public InputOutputHandler(IOutput output)
        {
            this.output = output;
        }

        public void HandleInput(string input)
        {
            int result;
            try
            {
                output.Write($"{input} = ");
                if (input.Length > 0)
                    result = parser.Parse(input);
                else
                    throw new ArgumentNullException();

                output.WriteLine(result.ToString());
            }
            catch (IncorrectArithmeticNotation)
            {
                output.WriteLine(Properties.Localization.IncorrectNotation);
            }
            catch (UnexpectedCharacterException)
            {
                output.WriteLine(Properties.Localization.UnexpectedCharacter);
            }
            catch (UnexpectedEndingException)
            {
                output.WriteLine(Properties.Localization.UnexpectedEnding);
            }
            catch (IncorrectBacketsException)
            {
                output.WriteLine(Properties.Localization.IncorrectBackets);
            }
            catch (ArgumentNullException)
            {
                output.WriteLine(Properties.Localization.EmptyString);
            }
            catch (DivideByZeroException)
            {
                output.WriteLine(Properties.Localization.DivideByZero);
            }
        }
    }
}
