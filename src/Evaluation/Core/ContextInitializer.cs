using System;
using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public class ContextInitializer
    {

        #region Alphabet

        // Elements
        private static IPattern x = new ElementPattern("x");
        private static IPattern y = new ElementPattern("y");
        private static IPattern z = new ElementPattern("z");

        // Integers
        private static IPattern int1 = new IntegerPattern("int1");
        private static IPattern int2 = new IntegerPattern("int2");
        private static IPattern int3 = new IntegerPattern("int3");

        // Sequences
        // Named sequences
        private static IPattern s_x = new NullableSequencePattern("s_x");
        private static IPattern s_y = new NullableSequencePattern("s_y");
        private static IPattern s_z = new NullableSequencePattern("s_z");

        // _ seqs
        private static IPattern seq1 = new NullableSequencePattern("seq1");
        private static IPattern seq2 = new NullableSequencePattern("seq2");
        private static IPattern seq3 = new NullableSequencePattern("seq3");
        private static IPattern seq4 = new NullableSequencePattern("seq4");
        private static IPattern seq5 = new NullableSequencePattern("seq5");
        private static IPattern seq6 = new NullableSequencePattern("seq6");

        #endregion


        public static List<(IElement, IElement)> GetInitialContext()
        {
            var context = new List<(IElement, IElement)>();
            context = context.Concat(PowBuiltins())
                .Concat(AddBuiltins())
                .Concat(MulBuiltins())
                .ToList();
            return context;
        }

        // Pow
        private static List<(IElement, IElement)> PowBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();
            var lhs = new Expression(nameof(pow),
                seq1,
                x,
                seq2,
                y,
                seq3
            );
            var rhs = new Expression();
            builtins.Add((lhs, rhs));
            return builtins;
        }

        // Mul
        private static List<(IElement, IElement)> MulBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // mul(x_,add(a_,a___))  -> add(mul(x_,a_),mul(x_,a___))
            var lhs = new Expression(nameof(mul),
                seq1,
                x,
                seq2,
                new Expression(nameof(sum),
                    y,
                    s_z
                ),
                seq3
            );
            var rhs = new Expression(nameof(mul),
                seq1,
                seq2,
                new Expression(nameof(sum),
                    new Expression(nameof(mul),
                        x,
                        y
                    ),
                    new Expression(nameof(mul),
                        x,
                        s_z
                    )
                ),
                seq3
            );
            builtins.Add((lhs, rhs));

            // mul(add(x_, y___), add(z___)) -> add(mul(x_, z___), mul(add(y___), add(z___)))
            lhs = new Expression(nameof(mul),
                seq1,
                new Expression(nameof(sum),
                    x,
                    s_y
                ),
                seq2,
                new Expression(nameof(sum), s_z),
                seq3
            );
            rhs = new Expression(nameof(mul),
                seq1,
                seq2,
                new Expression(nameof(sum),
                    new Expression(nameof(mul),
                        x,
                        s_z
                    ),
                    new Expression(nameof(mul), 
                        s_y,
                        s_z
                    )
                ),
                seq3
            );
            builtins.Add((lhs, rhs));

            return builtins;
        }

        //Sum
        private static List<(IElement, IElement)> AddBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // sum(x,x) -> mul(2,x)
            var lhs = new Expression(nameof(sum),
                seq1,
                x,
                seq2,
                x,
                seq3
            );
            var rhs = new Expression(nameof(sum),
                seq1,
                seq2,
                new Expression(nameof(mul),
                    new Integer(2),
                    x
                ),
                seq3
            );
            builtins.Add((lhs, rhs));

            // sum(mul(2,x),mul(2,x)) -> mul(sum(2,2),x)
            lhs = new Expression(nameof(sum),
                seq1,
                new Expression(nameof(mul),
                    int1,
                    x
                ),
                seq2,
                new Expression(nameof(mul),
                    int2,
                    x
                ),
                seq3
            );
            rhs = new Expression(nameof(sum),
                seq1,
                seq2,
                new Expression(nameof(mul),
                    new Expression(nameof(sum),
                        seq4,
                        int1,
                        seq5,
                        int2,
                        seq6
                    ),
                    x
                ),
                seq3
            );
            builtins.Add((lhs, rhs));

            // sum(x,mul(2,x)) -> mul(sum(2,1),x)
            lhs = new Expression(nameof(sum),
                seq1,
                x,
                seq2,
                new Expression(nameof(mul),
                    int1,
                    x
                ),
                seq3
            );
            rhs = new Expression(nameof(sum),
                seq1,
                seq2,
                
                new Expression(nameof(mul),
                    new Expression(nameof(sum),
                        seq4,
                        int1,
                        seq5,
                        new Integer(1),
                        seq6
                    ),
                    x
                ),
                seq3
            );
            builtins.Add((lhs, rhs));

            return builtins;
        }
    }
}
