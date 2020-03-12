using ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifmo_ca_lab_3.Evaluation.Commons
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
            if (left is Value && right is Value)
            {
                return (int)left.Key - (int)right.Key;
            }
            // If both elements are Symbols return the result of string comparing
            if (left is Symbol && right is Symbol)
            {
                return string.Compare(left.Key.ToString(), right.Key.ToString());
            }
            // No operands means that Expresiion is Symbol
            if (Converter.ToExpression(left).Operands.Count == 0 && Converter.ToExpression(right).Operands.Count == 0)
            {
                return string.Compare(left.Key.ToString(), right.Key.ToString());
            }
            // Expression with lesser operands is less than the other one
            if (Converter.ToExpression(left).Operands.Count != Converter.ToExpression(left).Operands.Count)
            {
                return Converter.ToExpression(left).Operands.Count - Converter.ToExpression(left).Operands.Count;
            }
            // Compare each operand
            var zipedOperands = Converter.ToExpression(left).Operands.Zip(Converter.ToExpression(right).Operands,
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
