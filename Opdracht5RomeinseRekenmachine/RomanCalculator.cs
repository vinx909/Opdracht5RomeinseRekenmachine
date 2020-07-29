using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht5RomeinseRekenmachine
{
    partial class RomanCalculator
    {
        private string number1;
        private string number2;
        private Opperator opperator;

        internal RomanCalculator()
        {
            number1 = "";
            number2 = "";
            opperator = Opperator.Unset;
        }

        internal void RunLambdaTroughOpperators(Action<Object> lambda)
        {
            foreach(Opperator opperator in Enum.GetValues(typeof(Opperator)))
            {
                if (opperator != Opperator.Unset)
                {
                    lambda(opperator);
                }
            }
        }
        internal void RunLambdaTroughRomanNumerals(Action<Object> Lambda)
        {
            char[] numerals = RomanNumberConverter.GetNumericals();
            foreach(char numeral in numerals)
            {
                Lambda(numeral);
            }
        }

        internal void AddNumeral(char numeral)
        {
            if (opperator == Opperator.Unset)
            {
                number1 += numeral;
            }
            else
            {
                number2 += numeral;
            }
        }
        internal void AddOpperator(Object opperator)
        {
            if (typeof(Opperator).IsInstanceOfType(opperator))
            {
                this.opperator = (Opperator)opperator;
            }
        }

        internal string GetSumText()
        {
            if (opperator == Opperator.Unset)
            {
                return number1;
            }
            else
            {
                return number1 + " " + opperator + " " + number2;
            }
        }
        internal string GetResult()
        {
            int int1 = RomanNumberConverter.ConvertToNumber(number1);
            int int2 = RomanNumberConverter.ConvertToNumber(number2);
            int result = calculate(int1, int2, opperator);
            string toReturn = RomanNumberConverter.ConvertToRoman(result);
            return toReturn;
        }

        private int calculate(int number1, int number2, Opperator opperator)
        {
            switch (opperator)
            {
                case Opperator.Plus:
                    return number1 + number2;
                case Opperator.Minus:
                    return number1 - number2;
                case Opperator.Multiply:
                    return number1 * number2;
                case Opperator.Devide:
                    return number1 / number2;
                case Opperator.Unset:
                    return number1;
            }
            throw new NotImplementedException();
        }

        private enum Opperator
        {
            Plus,
            Minus,
            Multiply,
            Devide,
            Unset
        }
    }
}
