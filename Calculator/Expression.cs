using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Expression : IExpression
    {
        const string validCharacters = @"[^\d\+\-\*\/\(\)]";
        const string dublicaserOrExcess = @"[\+\-\*\/]{2,}|[\+\-\*\/]$";
        const string splitCharacters = @"([\+\-\*\/\(\)])";
        const string bracketExpression = @"\(([\d\+\-\*\/]*)\)";
        const string splitCharactersReplacement = @" $1 ";
        /// <summary>
        /// Піймать точки, що стоять не між цифрами
        /// </summary>
        //const string validDotSeparator = @"[\D]\.[\D]|[\d]\.[\D]|[\D]\.[\d]";
        //const string

        /// <summary>
        /// Унарний мінус
        /// </summary>
        //const string unaryMinus = @"([^\d])(-[\d|\(])";
        /// <summary>
        /// Заміна унарного мінуса
        /// </summary>
        //const string unaryMinusReplacement = @"$1\ $2";
        //const string neglectedMultiPreBacket = @"([\d]|\))(\()";
        //const string neglectedMultiAfterBacket = @"([\)])([\d])";
        //const string neglectedMultiReplacement = @"$1\*$2";

        IExpression root;

        Dictionary<char, IExpression> operations = new Dictionary<char, IExpression>()
        {
            {'+', new Addition() },
            {'-', new Subtraction() },
            {'*', new Multiplication() },
            {'/', new Division() }
        };

        Dictionary<char, byte> operationPriority = new Dictionary<char, byte>()
        {
            {'*',1 },
            {'/',1 },
            {'+',2 },
            {'-',2 }
        };

        static char[] splitLexemes = new char[]
        {
            '(', ')','*','/', '+', '-'
        };

        public Expression(string input)
        {
            // прибрати лишні пробіли, коми замінить на точки
            input = input.Replace(" ", "").Replace(",", ".");
            Validate(input);
            Parse(input);
            //input = Prepare(input);
            //string[] lexems = SplitToLexemes(input);
        }

        public int Calculate()
        {
            throw new NotImplementedException();
        }

        string[] SplitToLexemes(string input)
        {
            Regex regex = new Regex(splitCharacters);
            input = regex.Replace(input, splitCharactersReplacement);
            return input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        void BuildTree(string input)
        {

            /*
            for (int index = 0; index < lexemes.Length; index++)
            {
                int constant = 0;
                if (int.TryParse(lexemes[index], out constant))
                {

                }
            }
            */
        }

        void Parse(string input)
        {
            Regex regex = new Regex(bracketExpression);
            var bracket = regex.Match(input);
            if (bracket.Groups.Count > 1)
            {

            }
        }

        /*
            Regex regex = new Regex(unaryMinus);
            input = regex.Replace(input, unaryMinusReplacement);

            regex = new Regex(neglectedMultiPreBacket);
            input = regex.Replace(input, neglectedMultiReplacement);

            regex = new Regex(neglectedMultiAfterBacket);
            input = regex.Replace(input, neglectedMultiReplacement);
            */

        IExpression ParseBackets(string backets)
        {
            return null;
        }

        void Validate(string input)
        {
            Regex regex = new Regex(validCharacters);
            if (regex.IsMatch(input))
                throw new Exception("There is invalid character in expression.");


            regex = new Regex(dublicaserOrExcess);
            if (regex.IsMatch(input))
                throw new Exception("There is trouble with arithmetic operations.");
        }

        int FindEndingOfBracket(string[] lexemses, int start)
        {
            int numOfBrackets = 1;
            int index = ++start;
            try
            {
                for (; numOfBrackets > 0; index++)
                {
                    if (lexemses[index] == "(")
                    {
                        numOfBrackets++;
                    }
                    else if (lexemses[index] == ")")
                    {
                        numOfBrackets--;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Problem with brackets!");
            }
            return index;
        }
    }
}
