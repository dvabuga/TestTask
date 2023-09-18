using EquationTransformationLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskConsole.ApplicationModes
{
    internal class FileExecutionMode : IExecutionMode
    {
        private readonly string _pathToFile;
        public FileExecutionMode(string pathToFile)
        {
            _pathToFile = pathToFile;
        }
        public string ModeName => "File";

        public void Execute()
        {
            Console.WriteLine(ConsoleApplicationMessages.FileProcessingHasStartedMessage);
            string docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Equations.out");
            Console.WriteLine($"Result file will be located in {docPath}");

            TransformEquation.ToCanonicalFormFromFile(_pathToFile, docPath);

            Console.WriteLine(ConsoleApplicationMessages.FileProcessingHasFinishedMessage);
        }
    }
}
