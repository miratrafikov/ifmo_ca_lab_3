using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation
{
    public class Expression : IExpression
    {
        public Expression(string head)
        {
            Head = head;
            Operands = new List<IExpression>();
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head, string key)
        {
            Head = head;
            Key = key;
            Operands = new List<IExpression>();
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public string Head { get; set; }
        public object Key { get; set; }

        public int Operation(int val1, int val2)
        {
            return Head switch
            {
                "Add" => val1 + val2,
                "Mul" => val1 * val2,
                "Pow" => val1 ^ val2,
                _ => default,
            };
        }

        public List<IExpression> Operands { get; set; }

        public List<IAttribute> Attributes { get; set; }

        public void Evaluate()
        {
            // evaluate the head of the expression
            // TODO: look for head name in the context, replace if needed
            switch (Head)
            {
                case nameof(Heads.Set):
                    // TODO: set stuff
                    break;
                case nameof(Heads.Delayed):
                    // TODO: delayed stuff
                    return;
                case nameof(Heads.Head):
                    Key = Head;
                    return;
                case nameof(Heads.Value):
                    return;
                case nameof(Heads.Symbol):
                    return;
                case nameof(Heads.Pow):
                    for (int i = 1; i < Operands.Count; i++)
                    {
                        if (Operands[i].Head != "Value")
                        {
                            throw new Exception("Symbol \"Pow\" can contain only integer exponent");
                        }
                    }
                    break;
                default:
                    // Add, Mul, Pow, default symbols
                    break;
            }
            // evaluate each element
            foreach (var operand in Operands)
            {
                switch (operand.Head)
                {
                    case nameof(Heads.Head):
                        throw new Exception("\"Head\" expression can not be used as inner expression");
                    case nameof(Heads.Set):
                        throw new Exception("\"Set\" expression can not be used as inner expression");
                    case nameof(Heads.Delayed):
                        throw new Exception("\"Delayed\" expression can not be used as inner expression");
                    default:
                        break;
                }
                operand.Evaluate();
            }

            // apply attributes
            foreach (var attr in Attributes)
            {
                Operands = attr.Apply(this);
            }
            // TODO: pseudo flat
            //RemovePrimitives();

            // apply given definitions
            // TODO: context stuff

            // apply built-in definitions
            // TODO: built ins

        }

        /*public bool IsPrimitive()
        {
            return (Operands.Count == 1);
        }

        /*public void RemovePrimitives()
        {
            foreach (var op in Operands.Where(op => op is Expression && ((Expression)op).IsPrimitive()))
            {
                Console.WriteLine(Head + " " + (op is Expression).ToString());
            }
            List<IExpression> var = Operands.Where(o => o is Expression && ((Expression)o).IsPrimitive())
                .Select(o => ((Expression)o).Operands.First()).ToList();
            Operands = Operands.Concat(var).ToList();
            Operands.RemoveAll(o => o is Expression && ((Expression)o).IsPrimitive());
            }*/
    }
}
