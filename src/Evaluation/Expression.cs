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
        public override int GetHashCode()
        {
            int hashCode = Head.GetHashCode() + Operands.Count * 69;
            foreach (var operand in Operands)
            {
                hashCode += operand.GetHashCode();
            }
            return hashCode;
        }

        private static Dictionary<Heads, string> BasicExpressions = new Dictionary<Heads, string>();

        static Expression()
        {
            foreach (Heads head in Enum.GetValues(typeof(Heads)))
            {
                BasicExpressions.Add(head, head.ToString().ToLower());
            }
        }

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
                "add" => val1 + val2,
                "mul" => val1 * val2,
                "pow" => val1 ^ val2,
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
                case nameof(Heads.set):
                    // TODO: set stuff
                    Context.AddEntry(Operands[0],Operands[1]);
                    break;
                case nameof(Heads.delayed):
                    // TODO: delayed stuff
                    Context.AddEntry(Operands[0], Operands[1]);
                    return;
                case nameof(Heads.head):
                    Key = Head;
                    return;
                case nameof(Heads.value):
                    return;
                case nameof(Heads.symbol):
                    return;
                case nameof(Heads.pow):
                    for (int i = 1; i < Operands.Count; i++)
                    {
                        if (Operands[i].Head != nameof(Heads.value))
                        {
                            throw new Exception("symbol \"pow\" can contain only integer exponent");
                        }
                    }
                    break;
                default:
                    // add, mul, default symbols
                    break;
            }
            // evaluate each element
            foreach (var operand in Operands)
            {
                switch (operand.Head)
                {
                    case nameof(Heads.head):
                        throw new Exception("\"head\" expression can not be used as inner expression");
                    case nameof(Heads.set):
                        throw new Exception("\"set\" expression can not be used as inner expression");
                    case nameof(Heads.delayed):
                        throw new Exception("\"delayed\" expression can not be used as inner expression");
                    default:
                        operand.Evaluate();
                        break;
                }
            }

            // apply attributes
            ApplyAttributes();

            // apply given definitions
            // TODO: context stuff

            for (var i = 0; i < Operands.Count; i++)
            {
                Operands[i] = Context.GetSubstitute(Operands[i]);
            }

            // apply built-in definitions
            RemovePrimitives();
            ApplyBuiltins();
            ApplyAttributes();
            ToNormalForm();
            //Operands.RemoveAll(o => o.head == nameof(Heads.mul) &&
              //  (int)ToExpression(o).Operands.First().Key == 0);
        }

        // Replace all aaa with pow(a, 3)
        public void ToNormalForm()
        {
            RemoveSequences();
            ApplyAttributes();
            RemoveExtraCoefficients();
        }

        // Assuming attributes are applied
        // Returns if this expression is alike the given one
        // Will be used only on monomials
        public bool IsAlike(IExpression iexpr)
        {
            // Allways false
            if (iexpr is Value) return iexpr.IsAlike(this);
            if (Head == nameof(Heads.value) && iexpr.Head == nameof(Heads.value)) return true;
            // Expression ~ symbol
            if (iexpr is Symbol) return iexpr.IsAlike(this);
            // symbol ~ symbol but both are expressions
            if (iexpr.Head == nameof(Heads.symbol) && Head == iexpr.Head)
            {
                return Key.Equals(iexpr.Key);
            }
            // add ~ add | pow ~ pow
            if (iexpr.Head == Head &&
                (Head == nameof(Heads.add) || Head == nameof(Heads.pow)))
            {
                return AreOperandsAlike(Operands, ToExpression(iexpr).Operands);
            }
            // mul ~ Expression
            if (Head == nameof(Heads.mul) || iexpr.Head == nameof(Heads.mul))
            {
                var l = GetAlikeOperands(this);
                var r = GetAlikeOperands(ToExpression(iexpr));
                return AreOperandsAlike(l, r);
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
            if (Head == nameof(Heads.value)) return;
            if (Head == nameof(Heads.symbol)) return;
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
            if (Head == nameof(Heads.value)) return;
            if (Head == nameof(Heads.symbol)) return;
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
                    Operands.Add(new Expression(nameof(Heads.pow))
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
            if (Head == nameof(Heads.value)) return;
            if (Head == nameof(Heads.symbol)) return;
            if (Operands == null) return;
            var i = 0;
            while (i < Operands.Count)
            {
                if (Operands[i].Head == nameof(Heads.mul) &&
                    ToExpression(Operands[i]).Operands[0].Equals(new Expression(nameof(Heads.value)) { Key = 1}))
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
                        Operands.Add(new Expression(nameof(Heads.mul))
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

        // Returns list of operands except by coefficient aka value
        private List<IExpression> GetAlikeOperands(Expression expr)
        {
            // TODO:
            //if (expr.Operands.Count == 0) return new List<IExpression>() { expr };
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
            if (Head == nameof(Heads.pow))
            {
                result = ApplyPowBuiltin();
                Operands = result;
                RemovePrimitives();
                ApplyAttributes();
            }
            if (Head == nameof(Heads.mul))
            {
                result = ApplyMulBuiltin();
                Operands = result;
                RemovePrimitives();
                ApplyAttributes();
            }
            if (Head == nameof(Heads.add))
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
            var exponent = Operands.Skip(1).Where( o => o.Head == nameof(Heads.value)).Aggregate(1, (a, b) => a * (int)b.Key);
            if (Operands.First().Head == nameof(Heads.value))
            {
                var b = (int)Operands.First().Key;
                return new List<IExpression>
                {
                    new Value((int)Math.Pow((double)b, (double)exponent))
                };
            }
            result.RemoveAll(o => o.Head == nameof(Heads.value));
            result.Add(new Value(exponent));
            // Kind of pow(base, exponent)
            // Foe example pow(x, 2)
            if (result.Count > 1)
            {
                Head = nameof(Heads.mul);
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
                    result.Add(DoAlikeSimplifying(nameof(Heads.add), alikeOperand, operand));
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
                if (operand.Head != nameof(Heads.add) && operand.Head != nameof(Heads.symbol) && operand.Head != nameof(Heads.value))
                    throw new Exception("Alert!");
                if (result.Count == 1 && result.First().Head == nameof(Heads.add))
                {
                    if (operand.Head == nameof(Heads.add))
                    {
                        var tmp = new Expression(nameof(Heads.add));
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
                else if (result.Count > 0 && operand.Head == nameof(Heads.value))
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

        // Returns result of production of add Expression and IExpression
        private Expression Multiply(Expression expr, Expression l)
        {
            var res = new Expression(nameof(Heads.add));
            /*if (expr.Operands.Count == 0)
            {
                res.Operands.add(DoAlikeSimplifying(nameof(Heads.mul), l, expr));
            }*/
            foreach (var o in expr.Operands)
            {
                res.Operands.Add(DoAlikeSimplifying(nameof(Heads.mul), l, o));
            }
            return res;
        }

        // For example xy+xy -> 2xy | xy*xy -> xxyy
        private IExpression DoAlikeSimplifying(string head, IExpression left, IExpression right)
        {
            // kostili moe vse
            if (left is null) return right;
            if (right is null) return left;
            if (left.Head == nameof(Heads.value) && right.Head == nameof(Heads.value))
            {
                return new Value(Operation(head, (int)left.Key, (int)right.Key));
            }
            if (left.Head == nameof(Heads.value))
            {
                return new Expression(nameof(Heads.mul)) { Operands = new List<IExpression>() 
                {
                    left, right
                } };
            }
            if (right.Head == nameof(Heads.value))
            {
                return new Expression(nameof(Heads.mul))
                {
                    Operands = new List<IExpression>()
                    {
                        right, left
                    }
                };
            }
            // kostil
            if (!(left is Expression) || !(right is Expression))
                throw new Exception("No symbol logic");

            left = AddCoefficient(ToExpression(left));
            right = AddCoefficient(ToExpression(right));
            var res = new Expression(nameof(Heads.mul));
            switch (head)
            {
                case (nameof(Heads.add)):
                    res.Operands = new List<IExpression>
                    {
                        new Value((int)ToExpression(left).Operands.First().Key +
                                    (int)ToExpression(right).Operands.First().Key)
                    };
                    res.Operands = res.Operands.Concat(ToExpression(left).Operands.Skip(1)).ToList();
                    return res;
                case (nameof(Heads.mul)):
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
                case (nameof(Heads.pow)):
                    throw new Exception("Unexpected using of pow head");
                default:
                    throw new Exception("Unexpexted head on terms simplifying");
            }
        }

        private Expression AddCoefficient(Expression expr)
        {
            //if (expr.Operands.Count < 1) throw new Exception("No operands");
            if (expr.Head == nameof(Heads.mul) && expr.Operands.Count == 1 && expr.Operands.First().Equals(new Value(1)))
                return expr;
            if (expr.Operands.Count < 1)
            {
                var res = new Expression("mul");
                res.Operands = new List<IExpression>() { new Value(1), expr };
                return res;
            }
            if (expr.Operands.Count > 1 && (expr.Head != nameof(Heads.mul))) throw new Exception("Too many arguments");
            if (expr.Operands.Count == 1)
            {
                var res = new Expression(nameof(Heads.mul));
                res.Operands = new List<IExpression>() { new Value(1) };
                res.Operands.Add(expr);
                return res;
            }
            if (expr.Operands.First().Head != nameof(Heads.value))
            {
                expr.Operands.Add(new Value(1));
                expr.Operands = expr.Attributes[1].Apply(expr);
                return expr;
            }
            return expr;
        }
    }
}
