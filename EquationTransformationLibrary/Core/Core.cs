using EquationTransformationLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransformationLibrary.Core
{
    //TODO: Encapsulation should be done properly
    internal static class Core
    {
        static internal string ConvertEquation(string inputEquation)
        {
            inputEquation = inputEquation.Trim();
            var equation = new Equation(inputEquation);
            

            var leftSideEquationMembersDictionary = ParseEquation(equation.LeftSide.Trim()).ToDictionary(x => x.baseMent, v => v);
            var rightSideEquationMembersDictionary = ParseEquation(equation.RightSide.Trim()).ToDictionary(x => x.baseMent, v => v);


            var freeCoefficientsFromLeftSideDict = leftSideEquationMembersDictionary.Where(c => c.Value.freeCoefficient).ToList();
            Helper.RemoveFreeCoefficientsFromOtherMembers(freeCoefficientsFromLeftSideDict, leftSideEquationMembersDictionary);


            var freeCoefficientsFromRightSideDict = rightSideEquationMembersDictionary.Where(c => c.Value.freeCoefficient).ToList();
            Helper.RemoveFreeCoefficientsFromOtherMembers(freeCoefficientsFromRightSideDict, rightSideEquationMembersDictionary);

            var processingResultDict = Processor.ProcessEquationMembersFromRightAndLeft(leftSideEquationMembersDictionary, rightSideEquationMembersDictionary);
            var freeCoefficient = Processor.ProcessFreeCoefficients(freeCoefficientsFromLeftSideDict, freeCoefficientsFromRightSideDict);

            var canonicalFormOnEquation = GetPreparedConvertationResult(processingResultDict, freeCoefficient);
            equation.CanonicalForm = canonicalFormOnEquation;

            return equation.CanonicalForm;
        }


        static private string GetPreparedConvertationResult(Dictionary<Tuple<string, string>, int> processingResultDict, float freeCoefficient)
        {
            var resultString = new StringBuilder();

            foreach (var item in processingResultDict)
            {
                if (float.Parse(item.Key.Item1) == 0)
                {
                    continue;
                }

                if (float.Abs(float.Parse(item.Key.Item1)) == 1)
                {
                    resultString.Append(float.Sign(float.Parse(item.Key.Item1)) > 0 ? "+" : "-");
                    resultString.Append(item.Key.Item2);
                    continue;
                }

                resultString.Append(item.Key.Item1 + item.Key.Item2);
            }

            return Helper.FormatResultEquation(resultString.ToString(), freeCoefficient);
        }


        static private List<EquationMember> ParseEquation(string input)
        {
            int index = 0;
            char minus = '-';
            char plus = '+';
            bool memberStarted = false;
            char sign = ' ';
            var member = new StringBuilder();
            var members = new List<EquationMember>();

            while (index < input.Length)
            {
                if (index == 0 && (input[index] == minus || input[index] == plus))
                {
                    memberStarted = true;
                    sign = input[index];
                    member.Append(sign);
                    index++;
                }

                // skip whitespace
                while (index < input.Length && input[index] == ' ')
                {
                    index++;
                }


                if (memberStarted && index < input.Length && (input[index] == minus || input[index] == plus))
                {
                    memberStarted = false;

                    members.Add(new EquationMember(member.ToString()));

                    member = new StringBuilder();

                    memberStarted = true;
                    sign = input[index];
                    member.Append(sign);
                    index++;
                    continue;
                }


                if (char.IsNumber(input[index]) ||
                    char.IsLetter(input[index]) ||
                    input[index] == plus ||
                    input[index] == minus ||
                    input[index] == '.' ||
                    input[index] == ',' ||
                    input[index] == '^')
                {
                    memberStarted = true;
                    member.Append(input[index]);
                    index++;
                    continue;
                }
            }

            members.Add(new EquationMember(member.ToString()));
            return members;
        }
    }
}
