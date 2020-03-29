using System;
using System.Threading.Tasks;

using CliFx;
using CliFx.Attributes;

using ShiftCo.ifmo_ca_lab_3.Chief.IOUtils;

namespace ShiftCo.ifmo_ca_lab_3.Chief.Commands
{
    [Command("syntax")]
    public class InputSyntaxCommand : ICommand
    {
        private readonly Tuple<string, string>[] _examples =
            {
                new Tuple<string, string>("Func(x)",
                    "Expression with head \"Func\" and one symbolic argument."),
                new Tuple<string, string>("Func(Eee(0,0), x)",
                    "Example of nested expressions."),
                new Tuple<string, string>("Func",
                    "Considered a plain symbol."),
                new Tuple<string, string>("100",
                    "A number is a number."),
            };

        public ValueTask ExecuteAsync(IConsole console)
        {
            IOBeautifier.PrintHeader(console, "Input examples");
            foreach (var (input, description) in _examples)
            {
                IOBeautifier.PrintPair(console, input, description);
            }

            return default;
        }
    }
}
