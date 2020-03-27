﻿using System;
using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public static class Evaluator
    {
        private static readonly int MaxIterationsAmount = 1000;
        private static readonly ElementComparer Comparer = new ElementComparer();

        private static IElement Evaluate(IElement element)
        {
            if (element is Expression expr && expr.Attributes.Contains(new HoldAttribute()))
            {
                return element;
            }
            switch (element)
            {
                case Integer i:
                    return i;

                case Symbol s:
                    // lookup in context
                    return Context.GetElement(s);

                case Expression e:
                    // evaluate head
                    var head = new Symbol(e.Head);
                    var newHead = Context.GetElement(head);
                    if (newHead is Symbol nhead)
                    {
                        e.Head = nhead.Value;
                    }

                    // apply attributes
                    if (e.Head != nameof(set) && e.Head != nameof(delayed))
                    {
                        var tmp = e;
                        foreach (var attribute in e.Attributes)
                        {
                            tmp = attribute.Apply(tmp);
                        }
                        e = tmp;
                    }

                    // evaluate each child
                    // TODO: Hold logic
                    for ( int i = 0; i < e.Operands.Count; i++)
                    {
                        e.Operands[i] = Evaluate(e.Operands[i]);
                    }

                    if (e.Head == nameof(set) || e.Head == nameof(delayed))
                    {
                        Context.AddRule(e.Operands[0], e.Operands[1]);
                    } 

                    // apply rules
                    return Context.GetElement(e);

                default:
                    return element;
            }
        }

        public static IElement Run(IElement element)
        {
            var iteration = 0;
            IElement pre = null;
            var post = new Expression("Evaluate", new List<IElement>()
            {
                element
            });
            while (Comparer.Compare(pre, post) != 0 && iteration <= MaxIterationsAmount)
            {
                pre = post;
                post = (Expression)Evaluate(pre);
                iteration++;
            }
            if (iteration == MaxIterationsAmount) throw new Exception("Amount of iterations exceeded");
            return post.Operands.FirstOrDefault();
        }
    }
}
