using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    class ContextInitializer
    {
        public List<(IElement, IElement)> GetInitialContext()
        {
            var context = new List<(IElement, IElement)>();
            context = context.Concat(PowBuiltins())
                .Concat(AddBuiltins())
                .Concat(MulBuiltins())
                .ToList();
            return context;
        }

        private List<(IElement, IElement)> PowBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();
            var lhs = new Expression(nameof(pow), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("x"),
                new NullableSequencePattern("b"),
                new IntegerPattern("y"),
                new NullableSequencePattern("c")
            });
            var rhs = new Expression();
            builtins.Add((lhs, rhs));
            return builtins;
        }

        private List<(IElement, IElement)> MulBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            return builtins;
        }

        private List<(IElement, IElement)> AddBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // sum(x,x) -> mul(2,x)
            var lhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("x"),
                new NullableSequencePattern("b"),
                new ElementPattern("x"),
                new NullableSequencePattern("c")
            });
            var rhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new NullableSequencePattern("b"),
                new Expression(nameof(mul), new List<IElement>()
                {
                    new Integer(2),
                    new ElementPattern("x")
                }),
                new NullableSequencePattern("c"),
            });
            builtins.Add((lhs, rhs));


            lhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),

                new NullableSequencePattern("b")
            });

            rhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new NullableSequencePattern("b"),
                new Expression(nameof(mul), new List<IElement>()
                {
                    new Integer(2),
                    new ElementPattern("x")
                }),
                new NullableSequencePattern("c"),
            });
            builtins.Add((lhs, rhs));

            return builtins;
        }
    }
}
