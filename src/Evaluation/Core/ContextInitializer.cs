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
        public static List<(IElement, IElement)> GetInitialContext()
        {
            var context = new List<(IElement, IElement)>();
            context = context.Concat(PowBuiltins())
                .Concat(AddBuiltins())
                .Concat(MulBuiltins())
                .ToList();
            return context;
        }

        private static List<(IElement, IElement)> PowBuiltins()
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

        private static List<(IElement, IElement)> MulBuiltins()
        {
            var builtins = new List<(IElement, IElement)>();

            // mul(x_,add(a_,a___))  -> add(mul(x_,a_),mul(x_,a___))
            var lhs = new Expression(nameof(mul), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("x"),
                new NullableSequencePattern("b"),
                new Expression(nameof(sum), new List<IElement>()
                {
                    new ElementPattern("y"),
                    new NullableSequencePattern("z")
                }),
                new NullableSequencePattern("c")
            });
            var rhs = new Expression(nameof(mul), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new NullableSequencePattern("b"),
                new Expression(nameof(sum), new List<IElement>()
                {
                    new Expression(nameof(mul), new List<IElement>()
                    {
                        new ElementPattern("x"),
                        new ElementPattern("y")
                    }),
                    new Expression(nameof(mul), new List<IElement>()
                    {
                        new ElementPattern("x"),
                        new NullableSequencePattern("z")
                    })
                }),
                new NullableSequencePattern("c"),
            });
            builtins.Add((lhs, rhs));

            // mul(add(x_, y___), add(z___)) -> add(mul(x_, z___), mul(add(y___), add(z___)))
            lhs = new Expression(nameof(mul), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new Expression(nameof(sum), new List<IElement>()
                {
                    new ElementPattern("x"),
                    new NullableSequencePattern("y")
                }),
                new NullableSequencePattern("b"),
                new Expression(nameof(sum), new List<IElement>()
                {
                    new NullableSequencePattern("z")
                }),
                new NullableSequencePattern("c")
            });
            rhs = new Expression(nameof(mul), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new NullableSequencePattern("b"),
                new Expression(nameof(sum), new List<IElement>()
                {
                    new Expression(nameof(mul), new List<IElement>()
                    {
                        new ElementPattern("x"),
                        new NullableSequencePattern("z")
                    }),
                    new Expression(nameof(mul), new List<IElement>()
                    {
                        new NullableSequencePattern("y"),
                        new NullableSequencePattern("z")
                    })
                }),
                new NullableSequencePattern("c"),
            });
            builtins.Add((lhs, rhs));

            return builtins;
        }

        private static List<(IElement, IElement)> AddBuiltins()
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

            // sum(mul(2,x),mul(2,x)) -> mul(sum(2,2),x)
            lhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new Expression(nameof(mul), new List<IElement>() 
                { 
                    new IntegerPattern("x1"),
                    new ElementPattern("y"),
                }),
                new NullableSequencePattern("b"),
                new Expression(nameof(mul), new List<IElement>()
                {
                    new IntegerPattern("x2"),
                    new ElementPattern("y"),
                }),
                new NullableSequencePattern("c")
            });
            rhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new NullableSequencePattern("b"),
                new Expression(nameof(mul), new List<IElement>()
                {
                    new Expression(nameof(sum), new List<IElement>() 
                    { 
                        new IntegerPattern("x1"),
                        new IntegerPattern("x2")
                    }),
                    new ElementPattern("y")
                }),
                new NullableSequencePattern("c"),
            });
            builtins.Add((lhs, rhs));

            // sum(x,mul(2,x)) -> mul(sum(2,1),x)
            lhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("y"),
                new NullableSequencePattern("b"),
                new Expression(nameof(mul), new List<IElement>()
                {
                    new IntegerPattern("x"),
                    new ElementPattern("y"),
                }),
                new NullableSequencePattern("c")
            });
            rhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new NullableSequencePattern("b"),
                new Expression(nameof(mul), new List<IElement>()
                {
                    new Expression(nameof(sum), new List<IElement>()
                    {
                        new IntegerPattern("x"),
                        new Integer(1)
                    }),
                    new ElementPattern("y")
                }),
                new NullableSequencePattern("c"),
            });
            builtins.Add((lhs, rhs));

            return builtins;
        }
    }
}
