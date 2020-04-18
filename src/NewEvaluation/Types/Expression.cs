using System;
using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Utils;
using Attribute = System.Attribute;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types
{
    public class Expression : IElement
    {
        public IElement Head { get; set; }
        public List<IElement> Elements { get; set; }
        public List<Attribute> Attributes { get; set; }

        public Expression(IElement head)
        {
            Head = head;
            Elements = new List<IElement>();
        }

        public Expression(IElement head, List<IElement> elements)
        {
            Head = head;
            Elements = elements;
        }

        public override int GetHashCode()
        {
            var hashCode = 17 * (int)Element.Expression;
            hashCode += 23 * Head.GetHashCode();
            foreach (var element in Elements)
            {
                hashCode += 23 * element.GetHashCode();
            }

            return hashCode;
        }

        public override bool Equals(object? obj)
        {
            // TODO Implement Equals() for Expression
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var str = Head + "[";
            for (var itElements = 0; itElements < Elements.Count; itElements++)
            {
                var element = Elements[itElements];
                str += element.ToString();
                if (itElements == Elements.Count - 1)
                {
                    str += "]";
                }
                else
                {
                    str += ", ";
                }
            }

            return str;
        }

        public void AddAttribute(Attribute attribute)
        {
            if (!Attributes.Contains(attribute))
            {
                Attributes.Add(attribute);
            }
        }
    }
}
