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
        public Expression()
        {
            Operands = new List<IExpression>();
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

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
                        if (Operands[i].Head != nameof(Heads.Value))
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
            ApplyAttributes();

            // apply given definitions
            // TODO: context stuff

            // apply built-in definitions
            RemovePrimitives();
            ApplyBuiltins();
            ApplyAttributes();
            //Operands.RemoveAll(o => o.Head == nameof(Heads.Mul) &&
              //  (int)ToExpression(o).Operands.First().Key == 0);
        }

        // Replace all aaa with Pow(a, 3)
        public void ToNormalForm()
        {
            RemoveSequences();
            RemoveExtraCoefficients();
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
                return Key.Equals(iexpr.Key);
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

        public void ApplyAttributes()
        {
            if (Attributes == null) return;
            foreach (var attr in Attributes)
            {
                Operands = attr.Apply(this);
            }
        }

        public void RemovePrimitives()
        {
            if (Operands.Count == 1)
            {
                Head = Operands.First().Head;
                Key = Operands.First().Key;
                if (ToExpression(Operands.First()) != default && Operands.First() is Expression)
                {
                    if (ToExpression(Operands.First()).Attributes != null)
                        Attributes = ToExpression(Operands.First()).Attributes;
                    if (ToExpression(Operands.First()).Operands != null)
                        Operands = ToExpression(Operands.First()).Operands;
                    var primitives = Operands.Where(o => o is Expression && IsPrimitive(ToExpression(o)));
                    // Remove inner primitives
                    foreach (var primitive in primitives)
                    {
                        var tmp = ToExpression(primitive);
                        tmp.RemovePrimitives();
                        Operands.Remove(primitive);
                        Operands.Add(tmp);
                    }
                }
                else
                {
                    Operands = null;
                    Attributes = null;
                }
            }
        }

        private void RemoveSequences()
        {
            if (Operands == null) return;
            var i = 0;
            while (i < Operands.Count)
            {
                var seqLength = 1;
                while (i + seqLength < Operands.Count && 
                    Operands[i].Equals(Operands[i + seqLength]))
                {
                    seqLength++;
                }
                if (seqLength > 1)
                {
                    var operand = Operands[i];
                    Operands.RemoveRange(i, seqLength);
                    Operands.Add(new Expression(nameof(Heads.Pow))
                    {
                        Operands = new List<IExpression>()
                        {
                            operand,
                            new Value(seqLength)
                        }
                    });
                }
                i++;
            }
            foreach (var e in Operands.OfType<Expression>())
            {
                e.RemoveSequences();
            }
        }

        private void RemoveExtraCoefficients()
        {
            if (Operands == null) return;
            var i = 0;
            while (i < Operands.Count)
            {
                if (Operands[i].Head == nameof(Heads.Mul) &&
                    ToExpression(Operands[i]).Operands[0].Equals(new Expression(nameof(Heads.Value)) { Key = 1}))
                {
                    if (ToExpression(Operands[i]).Operands.Count == 2)
                    {
                        var tmp = ToExpression(Operands[i]).Operands[1];
                        Operands.RemoveAt(i);
                        Operands.Add(tmp);
                    }
                    else
                    {
                        var tmp = ToExpression(Operands[i]).Operands.Skip(1);
                        Operands.Add(new Expression(nameof(Heads.Mul))
                        {
                            Operands = tmp.ToList()
                        });
                    }
                }
                else
                {
                    i++;
                }
            }
            foreach (var e in Operands.OfType<Expression>())
            {
                e.RemoveExtraCoefficients();
            }
        }

        // Returns list of operands except by coefficient aka Value
        private List<IExpression> GetAlikeOperands(Expression expr)
        {
            var operands = new List<IExpression>();
            if (expr.Operands.First() is Value)
            {
                operands = expr.Operands.Skip(1).ToList();
            }
            else
            {
                operands = expr.Operands;
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

        private bool IsPrimitive(Expression expr)
        {
            return expr.Operands.Count == 1;
        }

        private void ApplyBuiltins()
        {
            var result = new List<IExpression>();
            if (Head == nameof(Heads.Pow))
            {
                result = ApplyPowBuiltin();
                Operands = result;
                RemovePrimitives();
                ApplyAttributes();
            }
            if (Head ==nameof(Heads.Mul))
            {
                result = ApplyMulBuiltin();
                Operands = result;
                RemovePrimitives();
                ApplyAttributes();
            }
            if (Head == nameof(Heads.Add))
            {
                result = ApplyAddBuiltin();
                Operands = result;
                RemovePrimitives();
                ApplyAttributes();
            }
        }

        private List<IExpression> ApplyPowBuiltin()
        {
            var result = Operands;
            var exponent = Operands.Skip(1).Where( o => o.Head == nameof(Heads.Value)).Aggregate(1, (a, b) => a * (int)b.Key);
            if (Operands.First().Head == nameof(Heads.Value))
            {
                var b = (int)Operands.First().Key;
                return new List<IExpression>
                {
                    new Value((int)Math.Pow((double)b, (double)exponent))
                };
            }
            result.RemoveAll(o => o.Head == nameof(Heads.Value));
            result.Add(new Value(exponent));
            // Kind of Pow(base, exponent)
            // Foe example Pow(x, 2)
            if (result.Count > 1)
            {
                Head = nameof(Heads.Mul);
                result = Enumerable.Repeat(result.First(), (int)ToValue(result[1]).Key).ToList();
            }
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
                if (operand.Head != nameof(Heads.Add) && operand.Head != nameof(Heads.Symbol) && operand.Head != nameof(Heads.Value))
                    throw new Exception("Alert!");
                if (result.Count == 1 && result.First().Head == nameof(Heads.Add))
                {
                    if (operand.Head == nameof(Heads.Add))
                    {
                        var tmp = new Expression(nameof(Heads.Add));
                        foreach (var o in ToExpression(operand).Operands)
                        {
                            tmp.Operands.Add(Multiply(ToExpression(result.First()), ToExpression(o)));
                        }
                        tmp.Operands = tmp.Attributes[0].Apply(tmp);
                        result[0] = tmp;
                    }
                    else
                    {
                        result = new List<IExpression>() { Multiply(ToExpression(result.First()), ToExpression(operand)) };
                    }
                }
                else if (result.Count > 0 && operand.Head == nameof(Heads.Value))
                {
                    var found = false;
                    for (int i = 0; i < result.Count; i ++)
                    {
                        if (result[i] is Value)
                        {
                            found = true;
                            result[i]= new Value (((int)result[i].Key) * (int)operand.Key);
                        }
                    }
                    if (!found) result.Add(operand);
                }
                else
                {
                    result.Add(operand);
                }
            }
            return result;
        }

        // Returns result of production of Add Expression and IExpression
        private Expression Multiply(Expression expr, Expression l)
        {
            var res = new Expression(nameof(Heads.Add));
            /*if (expr.Operands.Count == 0)
            {
                res.Operands.Add(DoAlikeSimplifying(nameof(Heads.Mul), l, expr));
            }*/
            foreach (var o in expr.Operands)
            {
                res.Operands.Add(DoAlikeSimplifying(nameof(Heads.Mul), l, o));
            }
            return res;
        }

        // For example xy+xy -> 2xy | xy*xy -> xxyy
        private IExpression DoAlikeSimplifying(string head,IExpression left, IExpression right)
        {
            // kostili moe vse
            if (left is null) return right;
            if (right is null) return left;
            if (left.Head == nameof(Heads.Value) && right.Head == nameof(Heads.Value))
            {
                return new Value(Operation(head, (int)left.Key, (int)right.Key));
            }
            if (left.Head == nameof(Heads.Value))
            {
                return new Expression(nameof(Heads.Mul)) { Operands = new List<IExpression>() 
                {
                    left, right
                } };
            }
            if (right.Head == nameof(Heads.Value))
            {
                return new Expression(nameof(Heads.Mul))
                {
                    Operands = new List<IExpression>()
                    {
                        right, left
                    }
                };
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
            //if (expr.Operands.Count < 1) throw new Exception("No operands");
            if (expr.Head == nameof(Heads.Mul) && expr.Operands.Count == 1 && expr.Operands.First().Equals(new Value(1)))
                return expr;
            if (expr.Operands.Count < 1)
            {
                var res = new Expression("Mul");
                res.Operands = new List<IExpression>() { new Value(1), expr };
                return res;
            }
            if (expr.Operands.Count > 1 && (expr.Head != nameof(Heads.Mul))) throw new Exception("Too many arguments");
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
