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

        public InputOutputHandler()
        {
        }

        public void HandleUserInput()
        {
            Console.WriteLine(Properties.Localization.Welcome);
            Console.WriteLine(Properties.Localization.InputYoutExpression);
            while (true)
            {
                string input = Console.ReadLine();
                int output;
                try
                {
                    if (input.Length > 0)
                        output = parser.Parse(input);
                    else
                        throw new ArgumentNullException();

                    Console.WriteLine($" = {output};");
                }
                catch (IncorrectArithmeticNotation)
                {
                    Console.WriteLine(Properties.Localization.IncorrectNotation);
                }
                catch (UnexpectedCharacterException)
                {
                    Console.WriteLine(Properties.Localization.UnexpectedCharacter);
                }
                catch (UnexpectedEndingException)
                {
                    Console.WriteLine(Properties.Localization.UnexpectedEnding);
                }
                catch (IncorrectBacketsException)
                {
                    Console.WriteLine(Properties.Localization.IncorrectBackets);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine(Properties.Localization.EmptyString);
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine(Properties.Localization.DivideByZero); 
                }
            }
        }

        public void HandleFile(string path)
        {

        }
    }
}
