using System;
using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Util
{
    public class ElementComparer : Comparer<IElement>
    {
        public override int Compare(IElement left, IElement right)
        {
            // To avoid exceptions
            if (left is IPattern && right is IPattern) return 0;
            if (left is IPattern || right is IPattern) return int.MaxValue;
            if (right is null) return 1;
            if (left is null) return -1;

            // Compare Heads first
            if (CompareHeads(left.GetHead(), right.GetHead()) != 0) return CompareHeads(left.GetHead(), right.GetHead());

            // If both elements are Numbers return value according to the arithmetical order
            if (left is Number ln && right is Number rn)
            {
                if (ln.Value == rn.Value) return 0;
                else if (ln.Value > rn.Value) return 1;
                else return - 1;
            }
            // If both elements are Symbols return the result of string comparing
            if (left is Symbol ls && right is Symbol rs)
            {
                return string.Compare(ls.Value, rs.Value);
            }
            // Expression with lesser operands is less than the other one
            if (left is Expression le && right is Expression re && le.Operands.Count != re.Operands.Count)
            {
                return le.Operands.Count - re.Operands.Count;
            }
            // Compare each operand
            var zipedOperands = ((Expression)left).Operands.Zip(((Expression)right).Operands,
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

        private int CompareHeads(IElement lh, IElement rh)
        {
            if (lh is Symbol && rh is Symbol)
            {
                var left = ((Symbol)lh).Value;
                var right = ((Symbol)rh).Value;
                Enum.TryParse(typeof(Head), left, true, out var parsedLeft);
                Enum.TryParse(typeof(Head), right, true, out var parsedRight);

                if (parsedRight is null && parsedLeft is null) return 0;
                if (parsedLeft is null) return -1;
                if (parsedRight is null) return 1;

                return (int)parsedLeft - (int)parsedRight;
            }
            else
            {
                return Compare(lh, rh);
            }
        }
    }
}
