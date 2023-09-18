using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransformationLibrary.Core.Model
{
    internal class Equation
    {
        public Equation(string equation)
        {
            //TODO: implement business rules validation
            LeftSide = equation.Split('=')[0].Trim();
            RightSide = equation.Split('=')[1].Trim();
            Value = equation;
        }
        public string Value { get; set; }
        public string LeftSide { get; set; }
        public string RightSide { get; set; }
        public string CanonicalForm { get; set; }
    }
}
