using System;
using System.Threading.Tasks;
using ShiftCo.ifmo_ca_lab_3.Plot;

using CliFx;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Core;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace ShiftCo.ifmo_ca_lab_3.Chief
{
    public class Program
    {
        public static async Task<int> Main()
        {
            Thread plot = new Thread(PlotTesting);
            plot.SetApartmentState(ApartmentState.STA);
            plot.Start();

            return await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .Build()
                .RunAsync();
        }

        private static void PlotTesting()
        {
            var spiral = new Expression("spiral",
                new Number(2),
                new Number(0),
                new Number(6),
                new Number(0.1)
            );
            var evaluated = Evaluator.Run(spiral);
            /*var setF = new Expression(nameof(delayed),
                new Expression("f",
                    new NumberPattern("x")
                ),
                new Expression("sin",
                    new NumberPattern("x")
                )
            );
            var setResult = Evaluator.Run(setF);
            var expr = new Expression(nameof(plot),
                new Symbol("f"),
                new Number(0),
                new Number(3.25),
                new Number(0.25)
            );
            var evaluated = Evaluator.Run(expr);*/
        }
    }
}
