using EquationTransformationLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransformationLibrary
{
    public static class Helper
    {
        internal static string InvertSign(char sign)
        {
            return sign == '+' ? '-'.ToString() : '+'.ToString();
        }


        internal static void RemoveFreeCoefficientsFromOtherMembers(List<KeyValuePair<string, EquationMember>> freeCoefs, Dictionary<string, EquationMember> equationMembers)
        {
            foreach (var kv in freeCoefs)
            {
                equationMembers.Remove(kv.Key);
            }
        }


        internal static string AppendFreeCoefficient(string resultString, float freeCoefficient)
        {
            if (freeCoefficient == 0)
            {
                return resultString;
            }
            var signToAdd = float.Sign(freeCoefficient) > 0 ? "+" : "";
            var valueToAdd = signToAdd + freeCoefficient.ToString();
            return resultString + valueToAdd;
        }

        internal static string FormatResultEquation(string unformattedEquation, float freeCoefficient)
        {
            var formattedEquation = (Helper.AppendFreeCoefficient(unformattedEquation.ToString(), freeCoefficient) + " " + "= " + "0").Replace("^1", "");
            if (formattedEquation.StartsWith("+"))
                formattedEquation = formattedEquation.Remove(0, 1);

            return formattedEquation;
        }
    }
}
