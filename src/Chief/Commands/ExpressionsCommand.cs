using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using ShiftCo.ifmo_ca_lab_3.Chief.IOUtils;

namespace ShiftCo.ifmo_ca_lab_3.Chief.Commands
{
    [Command("exprs")]
    public class ExpressionsCommand : ICommand
    {
        private readonly Tuple<string, string>[] expressions =
        {
            new Tuple<string, string>("Sum", "(element)+"),
            new Tuple<string, string>("Mul", "(element)+"), 
            new Tuple<string, string>("Pow", "(element)(number)+"), 
            new Tuple<string, string>("Set", "(symbol|expression)(element)"), 
            new Tuple<string, string>("Head", "(element)")
        };

        public ValueTask ExecuteAsync(IConsole console)
        {
            IOBeautifier.PrintHeader(console, "Built-in expressions and their operands");
            foreach (var (expression, operands) in expressions)
            {
                IOBeautifier.PrintPair(console, expression, operands);
            }
            return default;
        }
    }
}
