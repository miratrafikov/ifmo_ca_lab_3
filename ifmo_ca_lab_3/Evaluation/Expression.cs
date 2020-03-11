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
            // look for head name in the context, replace if needed

            // evaluate each element
            foreach (var operand in Operands)
            {
                operand.Evaluate();
            }

            // apply attributes
            // pseudo flat
            //RemovePrimitives();
            
            // apply given definitions
            // context stuff

            // apply built-in definitions
            switch (Enum.Parse(typeof(HeadsEnum), Head))
            {
                case Heads.Add:
                    break;
                case Heads.Mul:
                    break;
                case Heads.Pow:
                    break;
                case Heads.Set:
                    break;
                case Heads.Delayed:
                    break;
                case Heads.Head:
                    break;
                case Heads.Value:
                    break;
                default:
                    break;
            }
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
