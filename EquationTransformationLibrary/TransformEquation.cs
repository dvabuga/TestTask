using EquationTransformationLibrary.Core;
using EquationTransformationLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransformationLibrary
{
    public static class TransformEquation
    {

        //TODO: probably this variable should be configurable using some config file
        const int batchSize = 50;
        public static string ToCanonicalForm(string inputEquation)
        {
            return Core.Core.ConvertEquation(inputEquation);
        }

        public static void ToCanonicalFormFromFile(string inputFilePath, string outputFilePath)
        {
            foreach (var batchOfLines in File.ReadLines(inputFilePath).ToTasksBatch(batchSize))
            {
                Task.WaitAll(batchOfLines.ToArray());

                foreach (var line in batchOfLines)
                {
                    //TODO: This could be done in async style
                    using (StreamWriter outputFile = new StreamWriter(outputFilePath, true))
                    {
                        outputFile.WriteLine(line.Result);
                    }
                }
            }
        }
    }
}
