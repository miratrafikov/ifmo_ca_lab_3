using System;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public static class Evaluator
    {
        private static readonly int MaxIterationsAmount = 1000;
        private static readonly ElementComparer Comparer = new ElementComparer();

        private static IElement Evaluate(IElement element)
        {
            switch (element)
            {
                case Integer i:
                    return i;

                case Symbol s:
                    // lookup in context
                    return s;

                case Expression e:
                    // evaluate head
                    //Evaluate(e.Head);

                    // apply attributes
                    foreach (var attribute in e.Apributes)
                    {
                        var expr = attribute.Apply(e);
                    }

                    // evaluate each child
                    foreach (IElement ie in e.Operands)
                    {
                        Evaluate(ie);
                    }

                    // apply rules
                    return e;

                default:
                    return element;
            }
        }

        public static IElement Run(IElement element)
        {
            var iteration = 0;
            var pre = element;
            IElement post = null;
            while (Comparer.Compare(pre, post) != 0 && iteration <= MaxIterationsAmount)
            {
                post = Evaluate(pre);
                iteration++;
            }
            if (iteration == MaxIterationsAmount) throw new Exception("Amount of iterations exceeded");
            return post;
        }
    }
}
