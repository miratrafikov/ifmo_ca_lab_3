using ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ifmo_ca_lab_3.Evaluation.Attributes;
using static ifmo_ca_lab_3.Base.HeadsEnum;

namespace ifmo_ca_lab_3.Base
{
    public class Expression : IExpression
    {
        public Expression (string head)
        {
            Head = head;
            Operands = new List<Expression>();
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
                "Pow" =>val1 ^ val2,
                _ => default,
            };
        }

        public List<Expression> Operands { get; set; }

        public List<IAttribute> Attributes { get; set; }

        public void Evaluate()
        {
            // evaluate the head of the expression
            // TODO: look for head name in the context, replace if needed
            switch (Enum.Parse(typeof(HeadsEnum), Head))
            {
                case Heads.Set:
                    // TODO: set stuff
                    break;
                case Heads.Delayed:
                    // TODO: delayed stuff
                    return;
                case Heads.Head:
                    Key = Head;
                    return;
                case Heads.Value:
                    return;
                case Heads.Symbol:
                    return;
                default:
                    break;
            }
            // evaluate each element
            foreach (var operand in Operands)
            {
                switch (Enum.Parse(typeof(HeadsEnum), operand.Head))
                {
                    case Heads.Head:
                        throw new Exception("\"Head\" expression can not be used as inner expression");
                    case Heads.Set:
                        throw new Exception("\"Set\" expression can not be used as inner expression");
                    case Heads.Delayed:
                        throw new Exception("\"Delayed\" expression can not be used as inner expression");
                    default:
                        break;
                }
                operand.Evaluate();
            }

            // apply attributes
            foreach (var attr in Attributes)
            {
                attr.Apply(this);
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
