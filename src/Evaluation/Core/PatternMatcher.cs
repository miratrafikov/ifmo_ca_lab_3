using System;
using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Commons;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public class PatternMatcher
    {
        private static Dictionary<string, IPattern> Patterns = new Dictionary<string, IPattern>();
        private static ElementComparer Comparer = new ElementComparer();

        public static Result Matches(IElement lhs, IElement obj)
        {
            var alikeMatch = AlikeMatch(lhs, obj);
            if (alikeMatch.Success == true) return alikeMatch;

            if (lhs == null || obj == null)
            {
                return new Result(false);
            }

            // Pattern is either Atom or Expression
            if (obj.Head != lhs.Head && lhs.Head != nameof(pattern))
            {
                return new Result(false);
            }

            // Pattern is kind of '_Integer'
            if (obj is Integer integer && lhs is IntegerPattern)
            {
                ((IntegerPattern)lhs).Element = integer;
                return new Result(true, lhs);
            }

            // Pattern is kind of '_'
            if (lhs is ElementPattern)
            {
                ((ElementPattern)lhs).Element = obj;
                return new Result(true, lhs);
            }

            // No pattern kinds required
            // Example: Symbol x and Symbol x
            if (Comparer.Compare(lhs, obj) == 0)
            {
                return new Result(true, lhs);
            }

            if (lhs is Expression p && obj is Expression o)
            {
                int j = 0;
                for (int i = 0; i < o.Operands.Count; i++)
                {
                    // Skip all nullable sequences in pattern
                    while (j < p.Operands.Count && p.Operands[j] is NullableSequencePattern) j++;
                    IElement tempPattern = null;
                    if (j < p.Operands.Count) tempPattern = p.Operands[j];
                    var matchResult = Matches(tempPattern, o.Operands[i]);
                    if (matchResult.Success == true)
                    {
                        ((Expression)lhs).Operands[j] = (IElement)matchResult.Value;
                        j++;
                    }
                    // If does not matches but previous is nullable sequence
                    else if (j > 0 && p.Operands[j - 1] is NullableSequencePattern)
                    {
                        ((NullableSequencePattern)((Expression)lhs).Operands[j - 1]).Operands.Add(o.Operands[i]);
                    }
                    else
                    {
                        return new Result(false);
                    }
                }

                while (j < ((Expression)lhs).Operands.Count &&
                    ((Expression)lhs).Operands[j] is NullableSequencePattern) j++;
                if (j == ((Expression)lhs).Operands.Count)
                {
                    Patterns = new Dictionary<string, IPattern>();
                    if (ArePatternsSame(lhs)) return new Result(true, lhs);
                }
                else
                {
                    return new Result(false);
                }
            }
            return new Result(false);
        }

        private static Result AlikeMatch(IElement el1, IElement el2)
        {
            if (el1 is Expression pattern && el2 is Expression expr)
            {
                if (pattern.Head == nameof(sum) &&
                    expr.Head == nameof(sum) &&
                    pattern.Operands.Count == 5 &&
                    pattern.Operands[0] is NullableSequencePattern seq1 &&
                    pattern.Operands[1] is ElementPattern x &&
                    pattern.Operands[2] is NullableSequencePattern seq2 &&
                    pattern.Operands[3] is ElementPattern y &&
                    pattern.Operands[4] is NullableSequencePattern seq3)
                {
                    for (var i = 0; i < expr.Operands.Count - 1; i++)
                    {
                        for (var j = i + 1; j < expr.Operands.Count; j++)
                        {
                            if (Comparer.Compare(expr.Operands[i], expr.Operands[j]) == 0)
                            {
                                seq1.Operands = expr.Operands.GetRange(0, i);
                                seq2.Operands = expr.Operands.GetRange(i + 1, j - i - 1);
                                if (j == expr.Operands.Count - 1)
                                {
                                    seq3.Operands = new List<IElement>();
                                }
                                else
                                {
                                    seq3.Operands = expr.Operands.GetRange(j + 1, expr.Operands.Count - j - 1);
                                }
                                x.Element = expr.Operands[j];
                                y.Element = expr.Operands[j];
                                return new Result(true, pattern);
                            }
                        }
                    }
                    return new Result(false);
                }
                else 
                if (pattern.Head == nameof(sum) &&
                    expr.Head == nameof(sum) &&
                    pattern.Operands.Count == 5 &&
                    pattern.Operands[0] is NullableSequencePattern seq4 &&
                    pattern.Operands[1] is Expression e1 &&
                    e1.Head == nameof(mul) &&
                    e1.Operands.Count == 1 &&
                    e1.Operands[0] is NullableSequencePattern seqX &&
                    pattern.Operands[2] is NullableSequencePattern seq5 &&
                    pattern.Operands[3] is Expression e2 &&
                    e2.Head == nameof(mul) &&
                    e2.Operands.Count == 2 &&
                    e2.Operands[0] is IntegerPattern integer &&
                    e2.Operands[1] is NullableSequencePattern seqY &&
                    pattern.Operands[4] is NullableSequencePattern seq6)
                {
                    for (var i = 0; i < expr.Operands.Count - 1; i++)
                    {
                        for (var j = 0; j < expr.Operands.Count; j++)
                        {
                            if (expr.Operands[i] is Expression &&
                                expr.Operands[j] is Expression &&
                                expr.Operands[i].Head == nameof(mul) && 
                                expr.Operands[j].Head == nameof(mul) &&
                                ((Expression)expr.Operands[j]).Operands.First() is Integer &&
                                AreOperandsSame(((Expression)expr.Operands[i]).Operands, 
                                    ((Expression)expr.Operands[j]).Operands.Skip(1).ToList()))
                            {
                                seq4.Operands = expr.Operands.GetRange(0, i);
                                seq5.Operands = expr.Operands.GetRange(i + 1, j - i - 1);
                                if (j == expr.Operands.Count - 1)
                                {
                                    seq6.Operands = new List<IElement>();
                                }
                                else
                                {
                                    seq6.Operands = expr.Operands.GetRange(j + 1, expr.Operands.Count - j - 1);
                                }
                                integer.Element = (Integer)((Expression)expr.Operands[j]).Operands.First();
                                seqX.Operands = ((Expression)expr.Operands[i]).Operands;
                                seqY.Operands = ((Expression)expr.Operands[i]).Operands;
                                return new Result(true, pattern);
                            }
                        }
                    }
                    return new Result(false);
                }
            }

            return new Result(false);
        }

        private static bool AreOperandsSame(List<IElement> l1, List<IElement> l2)
        {
            var list = l1.Zip(l2);
            foreach (var li in list)
            {
                if (Comparer.Compare(li.First, li.Second) != 0) return false;
            }
            return true;
        }

        // To see if all patterns with name 'x' are contain the same data.
        private static bool ArePatternsSame(IElement element)
        {
            if (element is IPattern p)
            {
                if (Patterns.ContainsKey(p.Name.Value))
                {
                    var comparer = new ElementComparer();
                    var dp = Patterns[p.Name.Value];
                    if (p is NullableSequencePattern l && dp is NullableSequencePattern r)
                    {
                        if (l.Operands.Count != r.Operands.Count) return false;
                        foreach (var o in l.Operands.Zip(r.Operands))
                        {
                            if (comparer.Compare(o.First, o.Second) != 0) return false;
                        }
                        return true;
                    }
                    else if (p is ElementPattern && dp is ElementPattern)
                    {
                        return comparer.Compare(((ElementPattern)p).Element, ((ElementPattern)dp).Element) == 0;
                    }
                    else if (p is IntegerPattern && dp is IntegerPattern)
                    {
                        return (((IntegerPattern)p).Element.Value - ((IntegerPattern)dp).Element.Value == 0);
                    }
                    return false;
                }
                else
                {
                    Patterns.Add(p.Name.Value, p);
                    return true;
                }
            }
            else if (element is Expression expr)
            {
                foreach (var o in expr.Operands)
                {
                    if (!ArePatternsSame(o)) return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
