using EquationTransformationLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransformationLibrary.Core
{
    internal static class Processor
    {
        /// <summary>
        /// Finds similar values on left and right sides of equation. Then substracts them or adds them.
        /// </summary>
        /// <param name="leftSideEquationMembersDictionary"></param>
        /// <param name="rightSideEquationMembersDictionary"></param>
        /// <returns></returns>
        static internal Dictionary<Tuple<string,string>, int> ProcessEquationMembersFromRightAndLeft(Dictionary<string, EquationMember> leftSideEquationMembersDictionary, Dictionary<string, EquationMember> rightSideEquationMembersDictionary)
        {
            var resultDictionary = new Dictionary<Tuple<string, string>, int>();

            foreach (var member in leftSideEquationMembersDictionary)
            {
                var maxExpanent = member.Value.variables.Max(v => v.Expanent);

                if (rightSideEquationMembersDictionary.ContainsKey(member.Key))
                {
                    var valueFromRightDictionary = rightSideEquationMembersDictionary[member.Key];
                    var valueFromLeftSide = float.Parse(member.Value.sign.ToString() + member.Value.coefficient.ToString());
                    var valueFromRightSide = float.Parse(Helper.InvertSign(valueFromRightDictionary.sign) + valueFromRightDictionary.coefficient.ToString());
                    var calculatedValue = valueFromLeftSide + valueFromRightSide;
                    var sign = calculatedValue > 0 ? "+" : "";
                    var valueWithSign = sign + calculatedValue.ToString();
                    resultDictionary.Add(new Tuple<string, string>(valueWithSign, member.Key), maxExpanent);
                }
                else
                {
                    resultDictionary.Add(new Tuple<string, string>(member.Value.sign.ToString() + member.Value.coefficient, member.Key), maxExpanent);
                }
            }


            foreach (var member in rightSideEquationMembersDictionary)
            {
                if (!leftSideEquationMembersDictionary.ContainsKey(member.Key))
                {
                    var maxExpanent = member.Value.variables.Max(v => v.Expanent);
                    var calculatedValue = float.Parse(Helper.InvertSign(member.Value.sign) + member.Value.coefficient);
                    resultDictionary.Add(new Tuple<string, string>(calculatedValue.ToString(), member.Key), maxExpanent);
                }
            }

            resultDictionary = resultDictionary.OrderByDescending(c => c.Value).ToDictionary(x => x.Key, x => x.Value);
            return resultDictionary;
        }



        static internal float ProcessFreeCoefficients(List<KeyValuePair<string, EquationMember>> left, List<KeyValuePair<string, EquationMember>> right)
        {
            float sum = 0;

            foreach (var pair in left)
            {
                var sign = pair.Value.sign;
                var coefficient = pair.Value.coefficient;
                sum += float.Parse(sign.ToString() + coefficient);
            }

            foreach (var pair in right)
            {
                var sign = Helper.InvertSign(pair.Value.sign);
                var coefficient = pair.Value.coefficient;
                sum += float.Parse(sign + coefficient);
            }

            return sum;
        }
    }
}
