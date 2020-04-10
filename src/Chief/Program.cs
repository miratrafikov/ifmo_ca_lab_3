using System;
using System.Threading.Tasks;
using ShiftCo.ifmo_ca_lab_3.Plot;

using CliFx;

namespace ShiftCo.ifmo_ca_lab_3.Chief
{
    public class Program
    {
        [STAThread]
        public static async Task<int> Main()
        {
            var plot = new MainWindow();

            return await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .Build()
                .RunAsync();
        }
            
    }
}
