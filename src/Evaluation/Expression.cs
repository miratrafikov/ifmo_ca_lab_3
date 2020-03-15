using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Commons.Converter;

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
                    // Add, Mul, default symbols
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
                        operand.Evaluate();
                        break;
                }
            }

            // apply attributes
            foreach (var attr in Attributes)
            {
                Operands = attr.Apply(this);
            }

            // apply given definitions
            // TODO: context stuff

            // apply built-in definitions            
            //Operands = ApplyBuiltins();

        }

        // Assuming attributes are applied
        // Returns if this expression is alike the given one
        // Will be used only on monomials
        public bool IsAlike(IExpression iexpr)
        {
            // Allways false
            if (iexpr is Value) return iexpr.IsAlike(this);
            // Expression ~ Symbol
            if (iexpr is Symbol) return iexpr.IsAlike(this);
            // Add ~ Add | Pow ~ Pow
            if (iexpr.Head == Head && 
                (Head == nameof(Heads.Add) || Head == nameof(Heads.Pow)))
            {
                return AreOperandsAlike(Operands, ToExpression(iexpr).Operands);
            }
            // Mul ~ Expression
            if (Head == nameof(Heads.Mul))
            {
                var l = GetAlikeOperands(this);
                var r = GetAlikeOperands(ToExpression(iexpr));
                return AreOperandsAlike(l ,r);
            }
            return false;
        }

        // Returns list of operands except by coefficient aka Value
        private List<IExpression> GetAlikeOperands(Expression expr)
        {
            var operands = new List<IExpression>();
            if (Operands.First() is Value)
            {
                operands = Operands.Skip(1).ToList();
            }
            else
            {
                operands = Operands;
            }
            return operands;
        }

        private bool AreOperandsAlike(List<IExpression> left, List<IExpression> right)
        {
            if (left.Count != right.Count) return false;
            var zipedOperands = left.Zip(right,
                (l, r) => new { Left = l, Right = r });
            foreach (var zip in zipedOperands)
            {
                if (!zip.Left.IsAlike(zip.Right)) return false;
            }
            return true;
        }

        private List<IExpression> ApplyBuiltins()
        {
            var result = Operands;
            if (Head == nameof(Heads.Pow))
            {
                var exponent = Operands.OfType<Value>().Aggregate(1, (a, b) => a * (int)b.Key);
                result.RemoveAll(o => o is Value);
                result.Add(new Value(exponent));
                if (result.Count > 1)
                {
                    Head = nameof(Heads.Mul);
                    result = Enumerable.Repeat(result.First(), (int)ToValue(result[1]).Key).ToList();
                }
            }
            if (Head == nameof(Heads.Add))
            {
                
            }

            return result;
        }
    }
}
