using System;
using System.Linq;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;
using ShiftCo.ifmo_ca_lab_3.Plot;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;
using System.Threading;

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
            if (element is Expression && (element.GetHead().Equals(new Symbol(nameof(set))) || 
                element.GetHead().Equals(new Symbol(nameof(delayed)))))
            {
                return LoopedEvaluate(element);
            }

            var expr = new Expression("Evaluate", element);
            var evaluated = LoopedEvaluate(expr);

            ShowPlot(evaluated);
            
            return evaluated;
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
            if (element is Expression expr)
            {
                expr.Head = LoopedEvaluate(expr.Head);
                expr = AddAttributes((Expression)element);
                
                // add a rule in the context if expr is delayed 
                if (expr.Head.Equals(new Symbol(nameof(delayed))) || 
                    expr.Head.Equals(new Symbol(nameof(set))))
                {
                    var e = Evaluate(element);
                    Context.AddRule(((Expression)e).Operands[0], ((Expression)e).Operands[1]);
                    return ((Expression)e).Operands[1];
                }
            }

            var evaluated = Evaluate(element);

            if (evaluated is Expression && ((Expression)evaluated).Operands.Count == 1)
            {
                evaluated = ((Expression)evaluated).Operands.First();
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
                case Number i:
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
                        if (((Expression)element).Attributes.Contains(new HoldAttribute("HoldFirst")))
                        {
                            from = Math.Min(1, e.Operands.Count);
                        }
                        if (((Expression)element).Attributes.Contains(new HoldAttribute("HoldRest")))
                        {
                            to = Math.Min(1, e.Operands.Count);
                        }

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

        private static void ShowPlot(IElement evaluated)
        {
            if (Thread.CurrentThread.GetApartmentState() is ApartmentState.STA)
            {
                if (evaluated.GetHead().Equals(new Symbol("Points")))
                {
                    var list = new List<(decimal, decimal)>();
                    foreach (var point in ((Expression)evaluated).Operands)
                    {
                        decimal x = ((Number)((Expression)point).Operands[0]).Value;
                        decimal y = ((Number)((Expression)point).Operands[1]).Value;
                        list.Add((x, y));
                    }
                    new MainWindow(list).ShowDialog();
                }
                if (evaluated.GetHead().Equals(new Symbol("Point")))
                {
                    decimal x = ((Number)((Expression)evaluated).Operands[0]).Value;
                    decimal y = ((Number)((Expression)evaluated).Operands[1]).Value;
                    var list = new List<(decimal, decimal)>() { (x, y) };
                    new MainWindow(list).ShowDialog();
                }
            }

        }

        private static Expression AddAttributes(Expression expr)
        {

            if (expr.Head.Equals(new Symbol("set")))
            {
                expr.Attributes.Add(new HoldAttribute("HoldFirst"));
            }

            if (expr.Head.Equals(new Symbol("delayed")))
            {
                expr.Attributes.Add(new HoldAttribute("HoldAll"));
            }

            if (expr.Head.Equals(new Symbol("if")))
            {
                expr.Attributes.Add(new HoldAttribute("HoldRest"));
            }
            return expr;
        }

        private static Expression ApplyAttributes(Expression expr)
        {
            if (expr.Head.Equals(new Symbol("set")) || expr.Head.Equals(new Symbol("delayed")) || expr.Head.Equals(new Symbol("if")) ||
                expr.Head.Equals(new Symbol("equals")) || expr.Head.Equals(new Symbol("nequals")) || expr.Head.Equals(new Symbol("greater")) ||
                expr.Head.Equals(new Symbol("greatere")) || expr.Head.Equals(new Symbol("less")) || expr.Head.Equals(new Symbol("lesse")) ||
                expr.Head.Equals(new Symbol("and")) || expr.Head.Equals(new Symbol("or")) || expr.Head.Equals(new Symbol("not")) ||
                expr.Head.Equals(new Symbol("Point")) || expr.Head.Equals(new Symbol("plot")) || expr.Head.Equals(new Symbol("Points")) ||
                expr.Head.Equals(new Symbol("div")) || expr.Head.Equals(new Symbol("taylorsin")) || expr.Head.Equals(new Symbol("term")) || 
                expr.Head.Equals(new Symbol("taylorcos")))
            {
                return expr;
            }

            var tmp = expr;
            foreach (var attribute in expr.Attributes)
            {
                if (tmp.Head.Equals(new Symbol("Points")))
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
