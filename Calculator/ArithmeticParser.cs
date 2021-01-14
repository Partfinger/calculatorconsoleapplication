using Calculator.Exceptios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Calculator
{
    public class ArithmeticParser
    {
        const string DublicaserOrExcess = @"[\+\*\/]{2,}|\-{3,}|[\+\-\*\/]$|^[\+\*\/]|^\-{2,}";
        const string defaultValidCharacters = @"\d\+\-\*\/\(\)\";

        const string SplitCharacters = @"([\+\-\*\/\(\)])";
        const string SplitCharactersReplacement = @" $1 ";

        const string InvalidDecimalSeparatorPattern = @"[\D]\.[\D]|[\d]\.[\D]|[\S]\.$";
        const string AnyWhiteSpacesPattern = @"\s+";
        const string NeglectedMultiOperationPattern = @"([\d]|\))(\()|([\)])([\d])";
        const string NeglectedMultiReplacement = @"$1$3*$2$4";

        readonly string OnlyValidCharacters;

        string[] lexemes;
        int position;

        public ArithmeticParser()
        {
            OnlyValidCharacters = $"[^{defaultValidCharacters}{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}]";
        }

        public double Parse(string input)
        {
            Regex regex = new Regex(AnyWhiteSpacesPattern);
            input = regex.Replace(input, "");

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
            if (!double.TryParse(next, out result))
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
            Regex regex = new Regex(SplitCharacters);
            input = regex.Replace(input, SplitCharactersReplacement);
            return input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        void Validate(string input)
        {
            Regex regex = new Regex(OnlyValidCharacters);
            if (regex.IsMatch(input))
            {
                throw new UnexpectedCharacterException();
            }    

            regex = new Regex(DublicaserOrExcess);
            if (regex.IsMatch(input))
            {
                throw new IncorrectArithmeticNotation();
            }

            regex = new Regex(InvalidDecimalSeparatorPattern);
            if (regex.IsMatch(input))
            {
                throw new Exception();
            }

            ValidateBackets(input);
        }

        string DecodeNeglectedOperations(string input)
        {
            Regex regex = new Regex(NeglectedMultiOperationPattern);
            input = regex.Replace(input, NeglectedMultiReplacement);

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
