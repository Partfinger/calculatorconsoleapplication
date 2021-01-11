using Calculator.Exceptios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class ArithmeticParser
    {
        /// <summary>
        /// Шукає невалідні символи
        /// </summary>
        const string validCharacters = @"[^\d\+\-\*\/\(\)\.,]";
        /// <summary>
        /// Дублікати, два знаки підряд, знаки в кінці виразу
        /// </summary>
        const string dublicaserOrExcess = @"[\+\*\/]{2,}|\-{3,}|[\+\-\*\/]$|^[\+\*\/]|^\-{2,}";
        //[\+\-\*\/]{2,}|[\+\-\*\/]$|^[\+\-\*\/]
        const string splitCharacters = @"([\+\-\*\/\(\)])";
        const string splitCharactersReplacement = @" $1 ";

        //const string bracketExpression = @"\(([\#\d\+\-\*\/]*)\)";
        const string invalidDotSeparatorPattern = @"[\D]\.[\D]|[\d]\.[\D]|[\S]\.$";
        const string neglectedMultiOperationPattern = @"([\d]|\))(\()|([\)])([\d])";
        const string neglectedZeroInDecimalPattern = @"([\D])(\.[\d])";
        const string anyWhiteSpacesPattern = @"\s+";
        const string neglectedMultiReplacement = @"$1$3*$2$4";
        const string neglectedZeroInDecimalReplacement = @"$1 0$2";

        string[] lexemes;
        int position;

        public ArithmeticParser()
        {
        }

        public double Parse(string input)
        {
            Regex regex = new Regex(anyWhiteSpacesPattern);
            input = regex.Replace(input, "").Replace(',', '.');

            if (input.Length == 0)
            {
                throw new ArgumentNullException();
            }
            Validate(input);

            input = DecodeNeglectedOperations(input);
            lexemes = SplitToLexemes(input);
            position = 0;
            return Expression();
        }

        /*
        E -> T + E  | T - E | T
        T -> F * T  | F / T | F
        F -> N      | (E)   | -F
        */

        double Expression()
        {
            double firstOperand = Term();

            while (position < lexemes.Length)
            {
                string current = lexemes[position];
                if (current != "+" && current != "-")
                {
                    break;
                }
                position++;

                double secondOperand = Term();
                if (current == "+")
                {
                    firstOperand += secondOperand;
                }
                else
                {
                    firstOperand -= secondOperand;
                }
            }
            return firstOperand;
        }

        double Term()
        {
            double firstOperand = Factor();

            while (position < lexemes.Length)
            {
                string current = lexemes[position];
                if (current != "*" && current != "/")
                {
                    break;
                }
                position++;

                double secondOperand = Factor();
                if (current == "*" )
                {
                    firstOperand *= secondOperand;
                }
                else
                {
                    if (secondOperand == 0)
                        throw new DivideByZeroException();
                    firstOperand /= secondOperand;
                }
            }
            return firstOperand;
        }

        double Factor()
        {
            string next = lexemes[position];
            double result;
            if (!double.TryParse(next, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                if (next == "(")
                {
                    position++;
                    result = Expression();

                    if (position >= lexemes.Length)
                    {
                        throw new UnexpectedEndingException();
                    }

                    string closingBracket = lexemes[position];

                    if (closingBracket == ")")
                    {
                        position++;
                        return result;
                    }
                }
                else if (next == "-") // унарний мінус
                {
                    position++;
                    return -Factor();
                }
                throw new UnexpectedCharacterException();
            }
            position++;

            return result;
        }

        string[] SplitToLexemes(string input)
        {
            Regex regex = new Regex(splitCharacters);
            input = regex.Replace(input, splitCharactersReplacement);
            return input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        void Validate(string input)
        {
            Regex regex = new Regex(validCharacters);
            if (regex.IsMatch(input))
            {
                throw new UnexpectedCharacterException();
            }    

            regex = new Regex(dublicaserOrExcess);
            if (regex.IsMatch(input))
            {
                throw new IncorrectArithmeticNotation();
            }

            regex = new Regex(invalidDotSeparatorPattern);
            if (regex.IsMatch(input))
            {
                throw new Exception();
            }

            ValidateBackets(input);
        }

        string DecodeNeglectedOperations(string input)
        {
            Regex regex = new Regex(neglectedMultiOperationPattern);
            input = regex.Replace(input, neglectedMultiReplacement);

            regex = new Regex(neglectedZeroInDecimalPattern);
            input = regex.Replace(input, neglectedZeroInDecimalReplacement);

            return input;
        }

        void ValidateBackets(string input)
        {
            int numberOfBackets = 0;
            foreach (char lexeme in input)
            {
                if (lexeme == '(')
                {
                    numberOfBackets++;
                }
                else if (lexeme == ')')
                {
                    numberOfBackets--;
                    if (numberOfBackets < 0)
                        throw new IncorrectBacketsException();
                }
            }
            if (numberOfBackets != 0)
                throw new IncorrectBacketsException();
        }
    }
}
