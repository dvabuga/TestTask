using EquationTransformationLibrary;
using TestTaskConsole;
using TestTaskConsole.ApplicationModes;

namespace TestTask
{
    internal class Program
    {
      
        static void Main(string[] args)
        {
            ExecuteApplicationInFileOrInterractiveMode(args);
        }


        static void ExecuteApplicationInFileOrInterractiveMode(string[] args)
        {
            IExecutionMode executionMode;
            if (ApplicationExecutedInFileMode(args))
            {
                if (FileExistsAndItIsTxtFile(args[0]))
                {
                    executionMode = new FileExecutionMode(args[0]);
                    executionMode.Execute();
                }
                else
                {
                    Console.WriteLine(ConsoleApplicationMessages.IncorrectFilePathMessage);
                }
            }
            else
            {
                while(true)
                {
                    executionMode = new InteractiveExecutionMode();
                    executionMode.Execute();
                }
                
            }
        }

        static bool ApplicationExecutedInFileMode(string[] args) => args.Length == 1;

        static bool FileExistsAndItIsTxtFile(string pathToFile)
        { 
          var bo = File.Exists(pathToFile);
          var ext = Path.GetExtension(pathToFile) == ".txt";
          return true;
        }
    }
}