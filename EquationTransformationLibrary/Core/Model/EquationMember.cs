using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EquationTransformationLibrary.Core.Core;

namespace EquationTransformationLibrary.Core.Model
{
    internal class EquationMember
    {
        internal EquationMember(string input)
        {
            value = input;
            Parse(input);
            CreateBaseMent();
        }

        public float coefficient = 1;
        string value;
        public char sign = ' ';
        public string baseMent = string.Empty;
        public List<EquationVariable> variables = new List<EquationVariable>();
        public bool freeCoefficient = false;

        void CreateBaseMent()
        {
            baseMent = string.Join("", variables.OrderBy(v => v.Symbol).SelectMany(c => string.Join("", c.Symbol.ToString() + "^" + c.Expanent)));
        }
        void Parse(string input)
        {
            var index = 0;

            if (InputIsFloatNumberThenParseAndReturn(input))
            {
                freeCoefficient = true;
                return;
            }

            input = DetermiceTheCoefficient(input);

            var variable = new EquationVariable();
            while (index < input.Length)
            {
                if (input[index] == '^')
                {
                    index++;
                    var exp = new StringBuilder();
                    while (index < input.Length && char.IsNumber(input[index]))
                    {
                        exp.Append(input[index]);
                        index++;
                    }
                    variable.Expanent = int.Parse(exp.ToString());
                    variables.Add(variable);
                }
                else if (variable.Symbol != null)
                {
                    variables.Add(variable);
                }

                if (index < input.Length && char.IsLetter(input[index]))
                {
                    variable = new EquationVariable();
                    variable.Symbol = input[index];
                    index++;
                    if (index >= input.Length)
                        variables.Add(variable);

                    continue;
                }
            }
        }

        bool InputIsFloatNumberThenParseAndReturn(string input)
        {
            float number = 0f;
            var res = float.TryParse(input, out number);
            if (res)
            {
                sign = float.Sign(number) > 0 ? '+' : '-';
                coefficient = float.Abs(number);
                var variable = new EquationVariable()
                {
                    Symbol = number,
                    Value = number,
                    FreeCoefficient = true
                };
                variables.Add(variable);
            }
            return res;
        }

        string DetermiceTheCoefficient(string input)
        {
            var index = 0;
            bool coefficientWasCalculated = false;
            while (!coefficientWasCalculated)
            {
                if (sign == ' ')
                {
                    if (input[index] == '-' || input[index] == '+')
                    {
                        sign = input[index];
                        input = input.Remove(0, 1);
                    }
                    else
                        sign = '+';

                }
                float number = 0f;

                if (char.IsLetter(input[index]))
                {

                    var result = float.TryParse(input.Substring(0, index), out number);
                    if (result)
                    {
                        coefficient = number;
                        coefficientWasCalculated = true;
                        return input.Substring(index, input.Length - index);
                    }

                    break;
                }
                index++;
            }
            return input;
        }
    }
}
