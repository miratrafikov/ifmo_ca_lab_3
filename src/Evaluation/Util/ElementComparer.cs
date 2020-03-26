using System;
using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces.Markers;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Util
{
    internal class ElementComparer : Comparer<IElement>
    {
        public override int Compare(IElement left, IElement right)
        {
            // Null check
            if (right is null) return (int)GreaterElement.Left;
            if (left is null) return (int)GreaterElement.Right;

            // 1. Heads
            var greaterElement = CompareHeadPrecedences(left, right);
            if (greaterElement != GreaterElement.Equal)
            {
                return (int)greaterElement;
            }

            // 2.1. As Integers
            if (left is Integer leftAsInteger && right is Integer rightAsInteger)
            {
                return leftAsInteger.Value - rightAsInteger.Value;
            }

            // 2.2. As Symbols
            if (left is Symbol leftAsSymbol && right is Symbol rightAsSymbol)
            {
                return string.Compare(leftAsSymbol.Value, rightAsSymbol.Value);
            }

            // 2.3. As Expressions
            if (left is Expression leftAsExpression && right is Expression rightAsExpression)
            {
                // 2.3.1. Operands count
                if (leftAsExpression.Operands.Count != rightAsExpression.Operands.Count)
                {
                    return leftAsExpression.Operands.Count - rightAsExpression.Operands.Count;
                }

                // 2.3.2. Operands comparison
                var operandPair = leftAsExpression.Operands.Zip(rightAsExpression.Operands, (l, r) => new { Left = l, Right = r });
                foreach (var operand in operandPair)
                {
                    var compare = Compare(operand.Left, operand.Right);
                    if (compare != (int)GreaterElement.Equal)
                    {
                        return compare;
                    }
                }
            }

            return (int)GreaterElement.Equal;
        }

        private GreaterElement CompareHeadPrecedences(IElement left, IElement right)
        {
            var leftPrecedence = (HeadsPrecedence)Enum.Parse(typeof(HeadsPrecedence), left.Head);
            var rightPrecedence = (HeadsPrecedence)Enum.Parse(typeof(HeadsPrecedence), right.Head);
            if (leftPrecedence < rightPrecedence)
            {
                return GreaterElement.Left;
            }
            if (rightPrecedence > leftPrecedence)
            {
                return GreaterElement.Right;
            }
            return GreaterElement.Equal;
        }

        private enum GreaterElement
        {
            Left = -1,
            Right = -1,
            Equal = 0
        }
    }
}
