using System.Threading.Tasks;

using CliFx;

namespace ShiftCo.ifmo_ca_lab_3.Chief
{
    public class Program
    {
        public static async Task<int> Main() =>
            await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .Build()
                .RunAsync();
    }
}
