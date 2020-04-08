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

                    //return AreExpressionsMatches(p, o);
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

        private static Result AreExpressionsMatches(Expression pattern, Expression expr)
        {
            Stack<int> matches = new Stack<int>();
            Stack<int> pos = new Stack<int>();
            int i = 0, j = 0;
            while (i < expr.Operands.Count)
            {
                while (j < pattern.Operands.Count && expr.Operands[j] is NullableSequencePattern) j++;
                var matchResult = new Result(false);
                if (j < expr.Operands.Count) matchResult = Matches(pattern.Operands[j], expr.Operands[i]);
                if (matchResult.Success == true)
                {
                    pattern.Operands[j] = (IElement)matchResult.Value;
                    matches.Push(i);
                    pos.Push(j);
                    j++;
                } 
                else if (j > 0 && pattern.Operands[j - 1] is NullableSequencePattern)
                {
                    ((NullableSequencePattern)pattern.Operands[j - 1]).Operands.Add(expr.Operands[i]);
                }
                else if (matches.Count > 0 && pos.Count > 0)
                {
                    var lastMatch = matches.Pop();
                    // TODO: reset all patterns after lastmatch

                    // move all elements into seq from last match to his closest nullable seq, 
                    // which goes before him
                    var c = lastMatch;
                    while (!(pattern.Operands[c] is NullableSequencePattern)) c--;

                }
                else
                {
                    return new Result(false);
                }
                i++;
            }

            // skip the rest of nullable seqs
            if (j == pattern.Operands.Count)
            {
                if (ArePatternsSame(pattern)) return new Result(true, pattern);
            }
            else if (matches.Count > 0 && pos.Count > 0)
            {

            }
            return new Result(false);

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
