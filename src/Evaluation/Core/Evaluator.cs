using System;
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
        private static readonly int s_maxIterationsAmount = 100000;
        private static int s_iterations = 0;
        private static readonly ElementComparer s_comparer = new ElementComparer();

        public static IElement Run(IElement element)
        {
            s_iterations = 0;
            if (element is Expression && (element.Head == nameof(set) || element.Head == nameof(delayed)))
            {
                return LoopedEvaluate(element);
            }
            var expr = new Expression("Evaluate", element);
            return LoopedEvaluate(expr);
        }

        private static void IncreaseIterations()
        {
            s_iterations++;
            if (s_iterations == s_maxIterationsAmount)
            {
                throw new TooManyIterationsException();
            }
        }

        private static IElement LoopedEvaluate(IElement element)
        {
            // evaluate head and apply attributes
            if (element is Expression)
            {
                element.Head = EvaluateHead(element);
                element = AddAttributes((Expression)element);
            }

            if (element.Head == nameof(plot) && ((Expression)element).Operands[0] is Symbol symbol)
            {
                Context.AddRule(symbol, new Symbol("func"));
            }

            // add a rule in the context if expr is delayed 
            if (element.Head == nameof(delayed) || element.Head == nameof(set))
            {
                var e = Evaluate(element);
                Context.AddRule(((Expression)e).Operands[0], ((Expression)e).Operands[1]);
                return ((Expression)e).Operands[1];
            }

            var evaluated = Evaluate(element);
            if (evaluated is Expression expr && expr.Operands.Count == 1)
            {
                evaluated = expr.Operands.First();
            }
            if (s_comparer.Compare(evaluated, element) == 0)
            {
                return evaluated;
            }
            else
            {
                return LoopedEvaluate(evaluated);
            }
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

                    // apply attributes
                    e = ApplyAttributes(e);

                    if (!e.Attributes.Contains(new HoldAttribute("HoldAll")))
                    {
                        // evaluate each child
                        int from = 0, to = e.Operands.Count;
                        if (((Expression)element).Attributes.Contains(new HoldAttribute("HoldFirst"))) from = Math.Min(1, e.Operands.Count);
                        if (((Expression)element).Attributes.Contains(new HoldAttribute("HoldRest"))) to = Math.Min(1, e.Operands.Count);
                        for (var i = from; i < to; i++)
                        {
                            e.Operands[i] = LoopedEvaluate(e.Operands[i]);
                        }
                    }

                    // apply rules
                    return Context.GetElement(e);

                default:
                    return element;
            }
        }

        private static Expression AddAttributes(Expression expr)
        {

            switch (expr.Head)
            {
                case nameof(set):
                    expr.Attributes.Add(new HoldAttribute("HoldFirst"));
                    break;
                case nameof(delayed):
                    expr.Attributes.Add(new HoldAttribute("HoldAll"));
                    break;
                case ("if"):
                    expr.Attributes.Add(new HoldAttribute("HoldRest"));
                    break;
                default:
                    break;
            }
            return expr;
        }

        private static string EvaluateHead(IElement expr)
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
            if (expr.Head == nameof(set) || expr.Head == nameof(delayed) || expr.Head == "if" ||
                expr.Head == "equals" || expr.Head == "nequals" || expr.Head == "greater" ||
                expr.Head == "greatere" || expr.Head == "less" || expr.Head == "lesse" ||
                expr.Head == "and" || expr.Head == "or" || expr.Head == "not" ||
                expr.Head == "Point" || expr.Head == nameof(plot))
            {
                return expr;
            }

            var tmp = expr;
            foreach (var attribute in expr.Attributes)
            {
                if (tmp.Head == "Points")
                {
                    if (attribute is FlatAttribute)
                    {
                        tmp = attribute.Apply(tmp);
                    }
                }
                else
                {
                    tmp = attribute.Apply(tmp);
                }
            }
            expr = tmp;

            return expr;
        }
    }
}
