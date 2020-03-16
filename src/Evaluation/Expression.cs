using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Commons.Converter;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Commons;

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
        
        public override bool Equals(object obj)
        {
            if (!(obj is IExpression)) return false;
            var comparer = new ExpressionComparer();
            return comparer.Compare(this, (IExpression)obj) == 0;
        }

        public int Operation(string head, int val1, int val2)
        {
            return head switch
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
            // TODO: look for the head name in the context, replace if needed
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
            Operands = ApplyBuiltins();
            Operands = Attributes[1].Apply(this);
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
            // Symbol ~ Symbol but both are expressions
            if (iexpr is Expression && iexpr.Head == nameof(Heads.Symbol) && Head == iexpr.Head)
            {
                return Head.Equals(iexpr.Head);
            }
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
            var result = new List<IExpression>();
            if (Head == nameof(Heads.Pow))
            {
                var exponent = Operands.OfType<Value>().Aggregate(1, (a, b) => a * (int)b.Key);
                result.RemoveAll(o => o is Value);
                result.Add(new Value(exponent));
                // Kind of Pow(base, exponent)
                if (result.Count > 1)
                {
                    Head = nameof(Heads.Mul);
                    result = Enumerable.Repeat(result.First(), (int)ToValue(result[1]).Key).ToList();
                }
            }
            if (Head ==nameof(Heads.Mul))
            {
                result = ApplyMulBuiltin();
            }
            if (Head == nameof(Heads.Add))
            {
                result = ApplyAddBuiltin();
            }
            return result;
        }

        private List<IExpression> ApplyPowBuiltin()
        {
            var result = Operands;
            return result;
        }

        private List<IExpression> ApplyAddBuiltin()
        {
            var result = new List<IExpression>();
            foreach (var operand in Operands)
            {
                IExpression alikeOperand = default;
                foreach (var resultOperand in result)
                {
                    if (resultOperand.IsAlike(operand))
                    {
                        alikeOperand = resultOperand;
                        break;
                    }
                }
                if (alikeOperand != default)
                {
                    result.Add(DoAlikeSimplifying(nameof(Heads.Add), alikeOperand, operand));
                    result.Remove(alikeOperand);
                }
                else
                {
                    result.Add(operand);
                }
            }
            return result;
        }

            private List<IExpression> ApplyMulBuiltin()
        {
            var result = new List<IExpression>();
            foreach (var operand in Operands)
            {
                if (operand is Expression && ToExpression(operand).Head == nameof(Heads.Add))
                {
                    var add = new Expression(nameof(Heads.Add));
                    var mul = new Expression(nameof(Heads.Mul));
                    mul.Operands = result;
                    foreach (var o in ToExpression(operand).Operands)
                    {
                        add.Operands.Add(DoAlikeSimplifying(nameof(Heads.Mul), mul, o));
                    }
                    result = add.Operands;
                }
                else
                {
                    result.Add(operand);
                    /*var alikeFound = false;
                    foreach (var resultOperand in result)
                    {
                        if (resultOperand.IsAlike(operand))
                        {
                            // Alike stuff
                            alikeFound = true;
                            break;
                        }
                    }
                    if (!alikeFound)
                    {
                        result.Add(operand);
                    }*/
                }
            }
            return result;
        }

        // For example xy+xy -> 2xy | xy*xy -> xxyy
        private IExpression DoAlikeSimplifying(string head,IExpression left, IExpression right)
        {
            if (left.Head == nameof(Heads.Value) && right.Head == nameof(Heads.Value))
            {
                return new Value(Operation(head, (int)left.Key, (int)right.Key));
            }
            // kostil
            if (!(left is Expression) || !(right is Expression))
                throw new Exception("No Symbol logic");

            left = AddCoefficient(ToExpression(left));
            right = AddCoefficient(ToExpression(right));
            var res = new Expression(nameof(Heads.Mul));
            switch (head)
            {
                case (nameof(Heads.Add)):
                    res.Operands = new List<IExpression>
                    {
                        new Value((int)ToExpression(left).Operands.First().Key +
                                    (int)ToExpression(right).Operands.First().Key)
                    };
                    res.Operands = res.Operands.Concat(ToExpression(left).Operands.Skip(1)).ToList();
                    return res;
                case (nameof(Heads.Mul)):
                    res.Operands = new List<IExpression>
                    {
                            new Value((int)ToExpression(left).Operands.First().Key *
                                        (int)ToExpression(right).Operands.First().Key)
                    };
                    res.Operands = res.Operands
                        .Concat(GetAlikeOperands(ToExpression(left)))
                        .Concat(GetAlikeOperands(ToExpression(right)))
                        .ToList();
                    res.Operands = res.Attributes[1].Apply(res);
                    return res;
                case (nameof(Heads.Pow)):
                    throw new Exception("Unexpected using of Pow head");
                default:
                    throw new Exception("Unexpexted head on terms simplifying");
            }
        }

        private Expression AddCoefficient(Expression expr)
        {
            if (expr.Operands.Count < 1) throw new Exception("No operands");
            if (expr.Operands.Count > 1 && expr.Head != nameof(Heads.Mul)) throw new Exception("Too many arguments");
            if (expr.Operands.Count == 1)
            {
                var res = new Expression(nameof(Heads.Mul));
                res.Operands = new List<IExpression>() { new Value(1) };
                res.Operands.Add(expr);
                return res;
            }
            if (expr.Operands.First().Head != nameof(Heads.Value))
            {
                expr.Operands.Add(new Value(1));
                expr.Operands = expr.Attributes[1].Apply(expr);
                return expr;
            }
            return expr;
        }
    }
}
