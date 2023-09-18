using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskConsole
{
    internal static class ConsoleApplicationMessages
    {
        public const string IncorrectFilePathMessage = "You have executed the application in file mode. But file path that you provided does not exist or file in other format than .txt. Please ensure that path and file format are correct and try again.";
        public const string AskToProvideInputForInteractiveModeMessage = "Please write input equation that needs to transform to canonical form";
        public const string FileProcessingHasStartedMessage = "File processing has started...";
        public const string FileProcessingHasFinishedMessage = "File processing has finished.";
        public const string InputStringIsEmpty = "Input string is empty";
    }
}
