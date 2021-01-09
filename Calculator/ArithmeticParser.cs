using Calculator.Exceptios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class ArithmeticParser
    {
        /// <summary>
        /// Шукає невалідні символи
        /// </summary>
        const string validCharacters = @"[^\d\+\-\*\/\(\)]";
        /// <summary>
        /// Дублікати, два знаки підряд, знаки в кінці виразу
        /// </summary>
        const string dublicaserOrExcess = @"[\+\*\/]{2,}|\-{3,}|[\+\-\*\/]$|^[\+\*\/]|^\-{2,}";
        //[\+\-\*\/]{2,}|[\+\-\*\/]$|^[\+\-\*\/]
        const string splitCharacters = @"([\+\-\*\/\(\)])";
        const string splitCharactersReplacement = @" $1 ";

        //const string validDotSeparator = @"[\D]\.[\D]|[\d]\.[\D]|[\D]\.[\d]";
        //const string bracketExpression = @"\(([\#\d\+\-\*\/]*)\)";
        const string neglectedMultiPreBacket = @"([\d]|\))(\()";
        const string neglectedMultiAfterBacket = @"([\)])([\d])";
        const string neglectedMultiReplacement = @"$1*$2";

        string[] lexemes;
        int position;

        public ArithmeticParser()
        {
        }

        public int Parse(string input)
        {
            input = input.Replace(" ", "");
            if (input.Length == 0)
            {
                throw new ArgumentNullException();
            }
            Validate(input);

            input = DecodeNeglectedOperations(input);
            lexemes = SplitToLexemes(input);
            ValidateBackets();
            position = 0;
            return Expression();
        }

        /*
        E -> T + E  | T - E | T
        T -> F * T  | F / T | F
        F -> N      | (E)
        */

        int Expression()
        {
            int firstOperand = Term();

            while (position < lexemes.Length)
            {
                string current = lexemes[position];
                if (current != "+" && current != "-")
                {
                    break;
                }
                position++;

                int secondOperand = Term();
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

        int Term()
        {
            int firstOperand = Factor();

            while (position < lexemes.Length)
            {
                string current = lexemes[position];
                if (current != "*" && current != "/")
                {
                    break;
                }
                position++;

                int secondOperand = Factor();
                if (current == "*" )
                {
                    firstOperand *= secondOperand;
                }
                else
                {
                    firstOperand /= secondOperand;
                }
            }
            return firstOperand;
        }

        int Factor()
        {
            string next = lexemes[position];
            int result;
            if (!int.TryParse(next, out result))
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
                    throw new UnexpectedCharacterException();
                }
                else if (next == "-") // унарний мінус
                {
                    position++;
                    return -Factor();
                }
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
        }

        string DecodeNeglectedOperations(string input)
        {
            Regex regex = new Regex(neglectedMultiPreBacket);
            input = regex.Replace(input, neglectedMultiReplacement);

            regex = new Regex(neglectedMultiAfterBacket);
            input = regex.Replace(input, neglectedMultiReplacement);

            return input;
        }

        void ValidateBackets()
        {
            int numberOfBackets = 0;
            foreach (string lexeme in lexemes)
            {
                if (lexeme == "(")
                {
                    numberOfBackets++;
                }
                else if (lexeme == ")")
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
