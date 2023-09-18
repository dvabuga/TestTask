using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskConsole.ApplicationModes
{
    /// <summary>
    /// Declared interface because it might be useful in case it will be neccessary to create more extended menu in the application
    /// </summary>
    internal interface IExecutionMode
    {
        public string ModeName { get; }
        void Execute();
    }
}
