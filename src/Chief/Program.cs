using System.Collections.Generic;
using System.Threading.Tasks;

using CliFx;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.AInterfaces;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

namespace ShiftCo.ifmo_ca_lab_3.Chief
{
    class Program
    {
        public static async Task<int> Main() =>
            await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .Build()
                .RunAsync();
    }
}
