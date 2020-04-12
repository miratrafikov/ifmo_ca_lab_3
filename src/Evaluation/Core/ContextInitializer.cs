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

        // Symbols
        private static readonly SymbolPattern s_sym1 = new SymbolPattern("sym1");
        private static readonly SymbolPattern s_sym2 = new SymbolPattern("sym2");
        private static readonly SymbolPattern s_sym3 = new SymbolPattern("sym3");

        // Integers
        private static readonly IPattern s_int1 = new NumberPattern("int1");
        private static readonly IPattern s_int2 = new NumberPattern("int2");
        private static readonly IPattern s_int3 = new NumberPattern("int3");

        // Sequences
        // Named sequences
        private static readonly IPattern s_seqX = new NullableSequencePattern("seqX");
        private static readonly IPattern s_seqY = new NullableSequencePattern("seqY");
        private static readonly IPattern s_seqZ = new NullableSequencePattern("seqZ");

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
            context = context
                .Concat(PowBuiltins())
                .Concat(MulBuiltins())
                .Concat(DivBuiltins())
                .Concat(AddBuiltins())
                .Concat(IfBuiltins())
                .Concat(PlotBuiltins())
                .Concat(TaylorBuiltins())
                .ToList();
            return context;
        }

        // Pow
        private static List<(IElement, IElement)> PowBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // Pow(2,2) -> 4
            var lhs = new Expression(nameof(pow),
                s_seq1,
                s_int1,
                s_seq2,
                s_int2,
                s_seq3
            );
            var rhs = new Expression();
            builtins.Add((lhs, rhs));

            // Pow(x,2) -> Mul(x,x)
            lhs = new Expression(nameof(pow),
                s_seq1,
                s_x,
                s_seq2,
                s_int1,
                s_seq3
            );
            rhs = new Expression();
            builtins.Add((lhs, rhs));

            // pow(x, 0) -> 1
            lhs = new Expression(nameof(pow),
                s_seq1,
                new Number(0)
            );
            rhs = new Expression(nameof(pow),
                new Number(1)
            );
            builtins.Add((lhs, rhs));
            return builtins;
        }

        // Mul
        private static List<(IElement, IElement)> MulBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // mul(1,2) -> 2
            IElement lhs = new Expression(nameof(mul),
                s_seq1,
                s_int1,
                s_seq2,
                s_int2,
                s_seq3
            );
            IElement rhs = new Expression();
            builtins.Add((lhs, rhs));

            // mul(1,x) -> x
            lhs = new Expression(nameof(mul),
                s_seq1,
                new Number(1),
                s_seq2,
                s_seqX,
                s_seq3
            );
            rhs = new Expression(nameof(mul),
                s_seq1,
                s_seq2,
                s_seqX,
                s_seq3
            );
            builtins.Add((lhs, rhs));

            // mul(0,x) -> 0
            lhs = new Expression(nameof(mul),
                s_seq1,
                new Number(0),
                s_seq2
            );
            rhs = new Number(0);
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
                        new Expression(nameof(sum), s_seqZ)
                    ),
                    new Expression(nameof(mul),
                        new Expression(nameof(sum), s_seqY),
                        new Expression(nameof(sum), s_seqZ)
                    )
                ),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            // mul(x_,add(a_,a___))  -> add(mul(x_,a_),mul(x_,a___))
            lhs = new Expression(nameof(mul),
                s_seq1,
                s_x,
                s_seq2,
                new Expression(nameof(sum),
                    s_y,
                    s_seqZ
                ),
                s_seq3
            );
            rhs = new Expression(nameof(mul),
                s_seq1,
                s_seq2,
                new Expression(nameof(sum),
                    new Expression(nameof(mul),
                        s_x,
                        s_y
                    ),
                    new Expression(nameof(mul),
                        s_x,
                        new Expression(nameof(sum), s_seqZ)
                    )
                ),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            return builtins;
        }

        // Div
        private static List<(IElement, IElement)> DivBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            var lhs = new Expression(nameof(div),
                s_seq1,
                s_int1,
                s_seq2,
                s_int2,
                s_seq3
            );
            var rhs = new Expression();
            builtins.Add((lhs, rhs));

            return builtins;
        }

        //Sum
        private static List<(IElement, IElement)> AddBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // add(1,2) -> 3
            var lhs = new Expression(nameof(sum),
                s_seq1,
                s_int1,
                s_seq2,
                s_int2,
                s_seq3
            );
            var rhs = new Expression();
            builtins.Add((lhs, rhs));

            // add(0,x,y) -> add(x,y)
            lhs = new Expression(nameof(sum),
                s_seq1,
                new Number(0),
                s_seq2
            );
            rhs = new Expression(nameof(sum),
                s_seq1,
                s_seq2
            );
            builtins.Add((lhs, rhs));

            // sum(x,x) -> mul(2,x)
            lhs = new Expression(nameof(sum),
                s_seq1,
                s_x,
                s_seq2,
                s_x,
                s_seq3
            );
            rhs = new Expression(nameof(sum),
                s_seq1,
                s_seq2,
                new Expression(nameof(mul),
                    new Number(2),
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
                    s_seqX
                ),
                s_seq2,
                new Expression(nameof(mul),
                    s_int2,
                    s_seqX
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
                    s_seqX
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
                        new Number(1),
                        s_seq6
                    ),
                    s_x
                ),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            // sum(mul(x,y),mul(2,x,y)) -> mul(sum(2,1),x,y)
            lhs = new Expression(nameof(sum),
                s_seq1,
                new Expression(nameof(mul), s_seqX),
                s_seq2,
                new Expression(nameof(mul),
                    s_int1,
                    s_seqX
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
                        new Number(1),
                        s_seq6
                    ),
                    s_seqX
                ),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            return builtins;
        }

        // If
        private static List<(IElement, IElement)> IfBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // Then
            IElement lhs = new Expression("if",
                new Symbol("true"),
                new ElementPattern("then"),
                new ElementPattern("else")
            );
            IElement rhs = new ElementPattern("then");
            builtins.Add((lhs, rhs));

            //Else
            lhs = new Expression("if",
                new Symbol("false"),
                new ElementPattern("then"),
                new ElementPattern("else"));
            rhs = new ElementPattern("else");
            builtins.Add((lhs, rhs));

            // Less
            lhs = new Expression("less",
                s_int1,
                s_int2
            );
            rhs = new Expression();
            builtins.Add((lhs, rhs));

            // LessOrEquals
            lhs = new Expression("lesse",
                s_int1,
                s_int2
            );
            rhs = new Expression();
            builtins.Add((lhs, rhs));

            // Greater
            lhs = new Expression("greater",
                s_int1,
                s_int2
            );
            rhs = new Expression();
            builtins.Add((lhs, rhs));

            // GreaterOrEquals
            lhs = new Expression("greatere",
                s_int1,
                s_int2
            );
            rhs = new Expression();
            builtins.Add((lhs, rhs));

            // Equals
            lhs = new Expression("equals",
                s_int1,
                s_int2
            );
            rhs = new Expression();
            builtins.Add((lhs, rhs));

            // NotEquals
            lhs = new Expression("nequals",
                s_int1,
                s_int2
            );
            rhs = new Expression();
            builtins.Add((lhs, rhs));

            // Not(false) -> true
            lhs = new Expression("not",
                new Symbol("false")
            );
            rhs = new Symbol("true");
            builtins.Add((lhs, rhs));

            // Not(true) -> false
            lhs = new Expression("not",
                new Symbol("true")
            );
            rhs = new Symbol("false");
            builtins.Add((lhs, rhs));

            // And (true, true) -> true
            lhs = new Expression("and",
                s_seq1,
                new Symbol("true"),
                s_seq2,
                new Symbol("true"),
                s_seq3
            );
            rhs = new Expression("and",
                s_seq1,
                s_seq2,
                new Symbol("true"),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            // And (false) -> false
            lhs = new Expression("and",
                s_seq1,
                new Symbol("false"),
                s_seq2
            );
            rhs = new Symbol("false");
            builtins.Add((lhs, rhs));

            // Or (false, false) -> false
            lhs = new Expression("or",
                s_seq1,
                new Symbol("false"),
                s_seq2,
                new Symbol("false"),
                s_seq3
            );
            rhs = new Expression("or",
                s_seq1,
                s_seq2,
                new Symbol("false"),
                s_seq3
            );
            builtins.Add((lhs, rhs));

            // Or (true) -> true
            lhs = new Expression("or",
                s_seq1,
                new Symbol("true"),
                s_seq2
            );
            rhs = new Symbol("true");
            builtins.Add((lhs, rhs));

            return builtins;
        }

        // Plot
        private static List<(IElement, IElement)> PlotBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // plot(f,from,to,step)
            IElement lhs = new Expression(nameof(plot),
                new SymbolPattern("func"),
                new NumberPattern("from"),
                new NumberPattern("to"),
                new NumberPattern("step")
            );
            IElement rhs = new Expression("if",
                new Expression("lesse",
                    new Expression(nameof(sum),
                        new NumberPattern("from"),
                        new NumberPattern("step")
                    ),
                    new NumberPattern("to")
                ),
                new Expression("Points",
                    new Expression("Point",
                        new NumberPattern("from"),
                        new Expression(new SymbolPattern("func"),
                            new NumberPattern("from")
                        )
                    ),
                    new Expression(nameof(plot),
                        new SymbolPattern("func"),
                        new Expression(nameof(sum),
                            new NumberPattern("from"),
                            new NumberPattern("step")
                        ),
                        new NumberPattern("to"),
                        new NumberPattern("step")
                    )
                ),
                new Expression("Point",
                    new NumberPattern("from"),
                    new Expression(new SymbolPattern("func"),
                        new NumberPattern("from")
                    )
                )
            );
            builtins.Add((lhs, rhs));
            return builtins;
        }

        // Taylor
        private static List<(IElement, IElement)> TaylorBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // Factorial(x)
            IElement lhs = new Expression("factorial", new Number(1));
            IElement rhs = new Number(1);
            builtins.Add((lhs, rhs));
            lhs = new Expression("factorial", new NumberPattern("x"));
            rhs = new Expression("mul",
                    new NumberPattern("x"),
                    new Expression("factorial",
                        new Expression("sum",
                            new Number(-1),
                            new NumberPattern("x")
                        )
                    )
            );
            builtins.Add((lhs, rhs));

            // x^(2n+1) / (2n+1)! | aka term
            lhs = new Expression("term", new NumberPattern("x"), new NumberPattern("n"));
            rhs = new Expression(nameof(div), 
                new Expression("pow", 
                    new NumberPattern("x"),
                    new Expression("sum",
                        new Expression("mul",
                            new Number(2),
                            new NumberPattern("n")
                        ),
                        new Number(1)
                    )
                ),
                new Expression("factorial",
                    new Expression("sum", 
                        new Expression("mul", 
                            new Number(2),
                            new NumberPattern("n")
                        ),
                        new Number(1)
                    )
                )
            );
            builtins.Add((lhs, rhs));

            // sin(x) -> taylor_sin(curr, x)
            lhs = new Expression("sin", new NumberPattern("x"));
            rhs = new Expression("taylorsin",
                new Number(0),
                new Number(10),
                new NumberPattern("x")
            );
            builtins.Add((lhs, rhs));

            // taylor_sin -> x - xxx/3! + ...
            lhs = new Expression("taylorsin", new NumberPattern("curr"), new NumberPattern("n"), new NumberPattern("x"));
            rhs = new Expression("if",
                new Expression("lesse",
                    new Expression("sum",
                        new NumberPattern("curr"),
                        new Number(1)
                    ),
                    new NumberPattern("n")
                ),
                new Expression("sum", 
                    new Expression("mul",
                        new Expression("pow",
                            new Number(-1),
                            new NumberPattern("curr")
                        ),
                        new Expression("term", new NumberPattern("x"), new NumberPattern("curr"))
                    ),
                    new Expression("taylorsin", 
                        new Expression("sum",
                            new Number(1),
                            new NumberPattern("curr")
                        ),
                        new NumberPattern("n"),
                        new NumberPattern("x"))
                ),
                new Expression("mul",
                    new Expression("pow",
                        new Number(-1),
                        new NumberPattern("curr")
                    ),
                    new Expression("term", new NumberPattern("x"), new NumberPattern("curr"))
                )
            );
            builtins.Add((lhs, rhs));

            return builtins;
        }
    }
}
