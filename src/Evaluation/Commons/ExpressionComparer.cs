using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Heads;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Commons.Converter;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Commons
{
    class ExpressionComparer : Comparer<IExpression>
    {
        public override int Compare(IExpression left, IExpression right)
        {
            // Compare Heads first
            if ((Heads)Enum.Parse(typeof(Heads), left.Head) > (Heads)Enum.Parse(typeof(Heads), right.Head))
            {
                return 1;
            }
            if ((Heads)Enum.Parse(typeof(Heads), left.Head) < (Heads)Enum.Parse(typeof(Heads), right.Head))
            {
                return -1;
            }
            // If both elemements are Values return value according to the arithmetical order
            if (left.Head == nameof(Heads.Value) && right.Head == nameof(Heads.Value))
            {
                return (int)left.Key - (int)right.Key;
            }
            // If both elements are Symbols return the result of string comparing
            if (left is Symbol && right is Symbol)
            {
                return string.Compare(left.Key.ToString(), right.Key.ToString());
            }
            // No operands means that Expression is Symbol
            if (ToExpression(left).Operands.Count == 0 && ToExpression(right).Operands.Count == 0)
            {
                return string.Compare(left.Key.ToString(), right.Key.ToString());
            }
            // Expression with lesser operands is less than the other one
            if (ToExpression(left).Operands.Count != ToExpression(left).Operands.Count)
            {
                return ToExpression(left).Operands.Count - ToExpression(left).Operands.Count;
            }
            // Compare each operand
            var zipedOperands = ToExpression(left).Operands.Zip(ToExpression(right).Operands,
                (l, r) => new { Left = l, Right = r });
            foreach (var operand in zipedOperands)
            {
                var compare = Compare(operand.Left, operand.Right);
                if (compare != 0)
                {
                    return compare;
                }
            }
            // No difference found
            return default;
        }
    }
}
