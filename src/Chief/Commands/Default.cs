using System.Threading.Tasks;

using CliFx;
using CliFx.Attributes;

namespace ShiftCo.ifmo_ca_lab_3.Chief.Commands
{
    [Command]
    public class Default : ICommand
    {
        [CommandParameter(0, Description = "Statement to evaluate.")]
        public string InputString { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            Main.InputString = InputString;
            Main.AcquireTokens();
            Main.ParseTokens();

            return default;
        }
    }
}
