using System;
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
            // evaluate head
            if (element is Expression)
            {
                element.Head = EvaluateHead(element);
            }

            // add hold if needed
            if (element.Head == nameof(set))
            {
                ((Expression)element).Attributes.Add(new HoldAttribute("HoldFirst"));
            }
            if (element.Head == nameof(delayed))
            {
                ((Expression)element).Attributes.Add(new HoldAttribute("HoldAll"));
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

                    if (e.Attributes.Contains(new HoldAttribute("HoldAll")))
                    {
                        return e;
                    }

                    // apply attributes
                    e = ApplyAttributes(e);

                    // evaluate each child
                    var ini = 0;
                    if (e.Attributes.Contains(new HoldAttribute("HoldFirst"))) ini = Math.Min(1, e.Operands.Count);
                    for (var i = ini; i < e.Operands.Count; i++)
                    {
                        e.Operands[i] = LoopedEvaluate(e.Operands[i]);
                    }

                    // add a rule in the context if expr is set
                    if (e.Head == nameof(set))
                    {
                        Context.AddRule(e.Operands[0], e.Operands[1]);
                        // no need in applying rules
                        return e.Operands[1];
                    }

                    // apply rules
                    return Context.GetElement(e);

                default:
                    return element;
            }
        }

        private static Expression AddHoldAttribute(Expression expr, string attr)
        {
            switch (attr)
            {
                case "HoldAll":
                    expr.Attributes.Add(new HoldAttribute(attr));
                    for (var i = 0; i < expr.Operands.Count; i++)
                    {
                        if (expr.Operands[i] is Expression)
                        {
                            ((Expression)expr.Operands[i]).Attributes.Add(new HoldAttribute(attr));
                        }
                    }
                    break;
                case "HoldFirst":
                    expr.Attributes.Add(new HoldAttribute(attr));
                    if (expr.Operands.First() is Expression)
                    {
                        ((Expression)expr.Operands[0]).Attributes.Add(new HoldAttribute(attr));
                    }
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
