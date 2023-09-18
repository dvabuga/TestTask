using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransformationLibrary.Core.Model
{
    internal class EquationVariable
    {
        internal EquationVariable()
        {
        }
        public object Symbol { get; set; } 
        public int Expanent { get; set; } = 1; 
        public float Value { get; set; } = 0;
        public bool FreeCoefficient { get; set; } = false;
    }
}
