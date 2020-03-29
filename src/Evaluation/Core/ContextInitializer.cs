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
        private static readonly IPattern s_x = new ElementPattern("x");
        private static readonly IPattern s_y = new ElementPattern("y");
        private static readonly IPattern s_z = new ElementPattern("z");

        // Integers
        private static readonly IPattern s_int1 = new IntegerPattern("int1");
        private static readonly IPattern s_int2 = new IntegerPattern("int2");
        private static readonly IPattern s_int3 = new IntegerPattern("int3");

        // Sequences
        // Named sequences
        private static readonly IPattern s_seqX = new NullableSequencePattern("s_x");
        private static readonly IPattern s_seqY = new NullableSequencePattern("s_y");
        private static readonly IPattern s_seqZ = new NullableSequencePattern("s_z");

        // _ seqs
        private static readonly IPattern s_seq1 = new NullableSequencePattern("seq1");
        private static readonly IPattern s_seq2 = new NullableSequencePattern("seq2");
        private static readonly IPattern s_seq3 = new NullableSequencePattern("seq3");
        private static readonly IPattern s_seq4 = new NullableSequencePattern("seq4");
        private static readonly IPattern s_seq5 = new NullableSequencePattern("seq5");
        private static readonly IPattern s_seq6 = new NullableSequencePattern("seq6");

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
                s_seq1,
                s_x,
                s_seq2,
                s_y,
                s_seq3
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
                s_seq1,
                s_x,
                s_seq2,
                new Expression(nameof(sum),
                    s_y,
                    s_seqZ
                ),
                s_seq3
            );
            var rhs = new Expression(nameof(mul),
                s_seq1,
                s_seq2,
                new Expression(nameof(sum),
                    new Expression(nameof(mul),
                        s_x,
                        s_y
                    ),
                    new Expression(nameof(mul),
                        s_x,
                        s_seqZ
                    )
                ),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            // mul(add(x_, y___), add(z___)) -> add(mul(x_, z___), mul(add(y___), add(z___)))
            lhs = new Expression(nameof(mul),
                s_seq1,
                new Expression(nameof(sum),
                    s_x,
                    s_seqY
                ),
                s_seq2,
                new Expression(nameof(sum), s_seqZ),
                s_seq3
            );
            rhs = new Expression(nameof(mul),
                s_seq1,
                s_seq2,
                new Expression(nameof(sum),
                    new Expression(nameof(mul),
                        s_x,
                        s_seqZ
                    ),
                    new Expression(nameof(mul),
                        s_seqY,
                        s_seqZ
                    )
                ),
                s_seq3
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
                s_seq1,
                s_x,
                s_seq2,
                s_x,
                s_seq3
            );
            var rhs = new Expression(nameof(sum),
                s_seq1,
                s_seq2,
                new Expression(nameof(mul),
                    new Integer(2),
                    s_x
                ),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            // sum(mul(2,x),mul(2,x)) -> mul(sum(2,2),x)
            lhs = new Expression(nameof(sum),
                s_seq1,
                new Expression(nameof(mul),
                    s_int1,
                    s_x
                ),
                s_seq2,
                new Expression(nameof(mul),
                    s_int2,
                    s_x
                ),
                s_seq3
            );
            rhs = new Expression(nameof(sum),
                s_seq1,
                s_seq2,
                new Expression(nameof(mul),
                    new Expression(nameof(sum),
                        s_seq4,
                        s_int1,
                        s_seq5,
                        s_int2,
                        s_seq6
                    ),
                    s_x
                ),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            // sum(x,mul(2,x)) -> mul(sum(2,1),x)
            lhs = new Expression(nameof(sum),
                s_seq1,
                s_x,
                s_seq2,
                new Expression(nameof(mul),
                    s_int1,
                    s_x
                ),
                s_seq3
            );
            rhs = new Expression(nameof(sum),
                s_seq1,
                s_seq2,

                new Expression(nameof(mul),
                    new Expression(nameof(sum),
                        s_seq4,
                        s_int1,
                        s_seq5,
                        new Integer(1),
                        s_seq6
                    ),
                    s_x
                ),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            return builtins;
        }
    }
}
