using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes
{
    public class FlatAttribute : IAttribute
    {
        public Expression Apply(Expression expr)
        {
            var head = expr.Head;
            var elements = new List<IElement>();
            foreach (var element in expr.Elements)
            {
                if (element is Expression e)
                {
                    if (e.Elements.Count == 1)
                    {
                        elements.Add(e.Elements.First());
                    }
                    else if (e.Head == head)
                    {
                        elements = elements.Concat(((Expression)element).Elements).ToList();
                    }
                    else
                    {
                        elements.Add(element);
                    }
                }
                else
                {
                    elements.Add(element);
                }
            }
            return new Expression(head, elements);
        }
    }
}
