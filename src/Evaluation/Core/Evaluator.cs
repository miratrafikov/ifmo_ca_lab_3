using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public static class Evaluator
    {
        private static readonly int s_maxIterationsAmount = 10000;
        private static int s_iterations = 0;
        private static readonly ElementComparer s_comparer = new ElementComparer();

        public static IElement Run(IElement element)
        {
            s_iterations = 0;
            var expr = new Expression("Evaluate", element);
            return ((Expression)LoopedEvaluate(expr))._operands.FirstOrDefault();
        }

        private static void IncreaseIterations()
        {
            s_iterations++;
            if (s_iterations == s_maxIterationsAmount)
            {
                throw new TooManyIterationsException();
            }
        }

        private static IElement LoopedEvaluate(IElement expression)
        {
            var post = expression;
            IElement pre = null;
            while (s_comparer.Compare(pre, post) != 0)
            {
                pre = post;
                post = Evaluate(pre);
            }
            return post;
        }

        private static IElement Evaluate(IElement element)
        {
            IncreaseIterations();
            switch (element)
            {
                case Integer i:
                    return i;

                case Symbol s:
                    return Context.GetElement(s);

                case Expression e:
                    if (e.Attributes.Contains(new HoldAttribute()))
                    {
                        return e;
                    }

                    // evaluate head
                    e.Head = EvaluateHead(e);

                    // apply attributes
                    e = ApplyAttributes(e);

                    // evaluate each child
                    // TODO: Hold logic
                    for (var i = 0; i < e._operands.Count; i++)
                    {
                        e._operands[i] = LoopedEvaluate(e._operands[i]);
                    }

                    // add a rule in the context if expr is either set or delayed 
                    if (e.Head == nameof(set) || e.Head == nameof(delayed))
                    {
                        Context.AddRule(e._operands[0], e._operands[1]);
                        // no need in applying rules
                        return e._operands[1];
                    }

                    // apply rules
                    return Context.GetElement(e);

                default:
                    return element;
            }
        }

        private static string EvaluateHead(Expression expr)
        {
            var head = new Symbol(expr.Head);
            var newHead = Context.GetElement(head);
            if (newHead is Symbol nhead)
            {
                expr.Head = nhead.Value;
            }
            return expr.Head;
        }

        private static Expression ApplyAttributes(Expression expr)
        {
            if (expr.Head == nameof(set) || expr.Head == nameof(delayed))
            {
                return expr;
            }

            var tmp = expr;
            foreach (var attribute in expr.Attributes)
            {
                tmp = attribute.Apply(tmp);
            }
            expr = tmp;

            return expr;
        }
    }
}
