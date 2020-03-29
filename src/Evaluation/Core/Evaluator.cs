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
        private static readonly int s_maxIterationsAmount = 1000;
        private static readonly ElementComparer s_comparer = new ElementComparer();

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
                    for (var i = 0; i < e._operands.Count; i++)
                    {
                        e._operands[i] = Evaluate(e._operands[i]);
                    }

                    if (e.Head == nameof(set) || e.Head == nameof(delayed))
                    {
                        Context.AddRule(e._operands[0], e._operands[1]);
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
            while (s_comparer.Compare(pre, post) != 0 && iteration <= s_maxIterationsAmount)
            {
                pre = post;
                post = (Expression)Evaluate(pre);
                iteration++;
            }
            if (iteration == s_maxIterationsAmount) throw new TooManyIterationsException();
            return post._operands.FirstOrDefault();
        }
    }
}
