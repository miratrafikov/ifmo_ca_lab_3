using System;
using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces.Markers;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public class PatternMatcher
    {
        private static Dictionary<string, IPattern> Patterns;

        public static MatchingResult TryMatch(IElement obj, IElement lhs)
        {
            var retrievedVariables = new Dictionary<string, List<IElement>>();

            // Null
            if (obj == null || lhs == null)
            {
                return new MatchingResult(false);
            }

            // Simply equal
            if (ReferenceEquals(obj, lhs))
            {
                return new MatchingResult(true);
            }

            // Not even a pattern!
            if (lhs.Head != "Pattern" && obj.Head != lhs.Head)
            {
                return new MatchingResult(false);
            }

            // General pattern
            if (lhs is ElementPattern lhsAsElement)
            {
                retrievedVariables.Add(lhsAsElement.Name, new List<IElement> { obj });
                return new MatchingResult(true, retrievedVariables);
            }

            // Integer
            if (lhs is IntegerPattern lhsAsInteger && obj is Integer integer)
            {
                retrievedVariables.Add(lhsAsInteger.Name, new List<IElement> { obj });
                return new MatchingResult(true, retrievedVariables);
            }

            // Expression, epic
            if (!(lhs is Expression lhsAsExpression) || !(obj is Expression objAsExpression))
            {
                throw new Exception("Wrong pattern, boyo.");
            }
            // Привести в начальное состояние
            var lhsIterator = 0;
            IPattern lhsChild = null, lhsChildPrev = null;
            NextPattern(ref lhsChild, ref lhsChildPrev, ref lhsIterator, lhsAsExpression);
            // Для каждого операнда в объекте...
            for (var objIterator = 0; objIterator < objAsExpression.Operands.Count; objIterator++)
            {
                // Если больше паттернов нет...
                if (lhsIterator == lhsAsExpression.Operands.Count)
                {
                    return new MatchingResult(false);
                }
                // Получить объект
                var objChild = objAsExpression.Operands[objIterator];
                // Получить текущий паттерн
                NextPattern(ref lhsChild, ref lhsChildPrev, lhsIterator, lhsAsExpression);
                // Попробовать их заматчить...
                var matchResult = TryMatch(objChild, (IElement)lhsChild);
                // И если получилось, то...
                if (matchResult.IsSuccess)
                {
                    // Дополнить список переменных...
                    retrievedVariables.Concat(matchResult.RetrievedVariables);
                    // И указывать на следующий паттерн...
                    lhsIterator++;
                }
                // А если нет, то...
                else
                {
                    // Если предыдущий паттерн — нулосек, то...
                    if (lhsIterator > 0 && lhsAsExpression.Operands[lhsIterator - 1] is NullableSequencePattern)
                    {
                        // Добавить объект в список под имя нулосека...
                        var previousVariable = (IPattern)lhsAsExpression.Operands[lhsIterator - 1];
                        retrievedVariables.Add(previousVariable.Name, new List<IElement> { objChild });
                    }
                    // А если и то не вышло, то фиаско.
                    else
                    {
                        return new MatchingResult(false);
                    }
                }
            }

            // У объекта закончились детишки? Пробуем напропускать нулосеки паттерна!
            while (lhsIterator < lhsAsExpression.Operands.Count &&
                lhsAsExpression.Operands[lhsIterator] is NullableSequencePattern)
            {
                lhsIterator++;
            }
            if (lhsIterator == lhsAsExpression.Operands.Count)
            {
                // TODO
            }
            return new MatchingResult(false);
        }

        private static int SkipNullables(Expression lhs)
        {
            var patternsSkipped = 0;
            while (lhs.Operands[patternsSkipped] is NullableSequencePattern && patternsSkipped < lhs.Operands.Count)
            {
                patternsSkipped++;
            }
            return patternsSkipped;
        }

        private static void NextPattern(ref IPattern lhsChild, ref IPattern lhsChildPrev, ref int lhsIterator, Expression lhsAsExpression)
        {
            lhsChildPrev = lhsChild;
            lhsChild = (IPattern)lhsAsExpression.Operands[lhsIterator];
            lhsIterator++;
        }

        //private static bool ArePatternsSame(IElement element)
        //{
        //    if (element is IPattern p)
        //    {
        //        if (Patterns.ContainsKey(p.Name))
        //        {
        //            var comparer = new ElementComparer();
        //            var dp = Patterns[p.Name];
        //            if (p is NullableSequencePattern l && dp is NullableSequencePattern r)
        //            {
        //                if (l.StoredElements.Count != r.StoredElements.Count) return false;
        //                foreach (var o in l.StoredElements.Zip(r.StoredElements))
        //                {
        //                    if (comparer.Compare(o.First, o.Second) != 0) return false;
        //                }
        //                return true;
        //            }
        //            else if (p is ElementPattern && dp is ElementPattern)
        //            {
        //                return comparer.Compare(((ElementPattern)p).Element, ((ElementPattern)dp).Element) == 0;
        //            }
        //            else if (p is IntegerPattern && dp is IntegerPattern)
        //            {
        //                return (((IntegerPattern)p).Element.Value - ((IntegerPattern)dp).Element.Value == 0);
        //            }
        //            return false;
        //        }
        //        else
        //        {
        //            Patterns.Add(p.Name, p);
        //            return true;
        //        }
        //    }
        //    else if (element is Expression expr)
        //    {
        //        foreach (var o in expr.Operands)
        //        {
        //            if (!ArePatternsSame(o)) return false;
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
