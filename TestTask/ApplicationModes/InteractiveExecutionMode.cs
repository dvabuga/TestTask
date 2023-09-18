using EquationTransformationLibrary;

namespace TestTaskConsole.ApplicationModes
{
    internal class InteractiveExecutionMode : IExecutionMode
    {
        public string ModeName => "Interactive";

        public void Execute()
        {
            Console.WriteLine(ConsoleApplicationMessages.AskToProvideInputForInteractiveModeMessage);

            var inputEquation = Console.ReadLine();

            if (string.IsNullOrEmpty(inputEquation))
                Console.WriteLine(ConsoleApplicationMessages.InputStringIsEmpty);
            else
            {
                var equationTransformationResult = TransformEquation.ToCanonicalForm(inputEquation);
                Console.WriteLine(equationTransformationResult);
            }
        }
    }
}
