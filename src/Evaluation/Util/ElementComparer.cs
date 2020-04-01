using System;
using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types.Atoms;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Util
{
    public class ElementComparer : Comparer<IElement>
    {
        public override int Compare(IElement left, IElement right)
        {
            // To avoid exceptions
            if (left is IPattern || right is IPattern) return 0;
            if (right is null) return 1;
            if (left is null) return -1;

            // Compare Heads first
            if (CompareHeads(left.Head, right.Head) != 0) return CompareHeads(left.Head, right.Head);

            // If both elemements are Integers return value according to the arithmetical order
            if (left is Integer li && right is Integer ri)
            {
                return li.Value - ri.Value;
            }
            // If both elements are Symbols return the result of string comparing
            if (left is Symbol ls && right is Symbol rs)
            {
                return string.Compare(ls.Value, rs.Value);
            }
            // Expression with lesser operands is less than the other one
            if (left is Expression le && right is Expression re && le._operands.Count != re._operands.Count)
            {
                return le._operands.Count - re._operands.Count;
            }
            // Compare each operand
            var zipedOperands = ((Expression)left)._operands.Zip(((Expression)right)._operands,
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

        private int CompareHeads(string left, string right)
        {
            Enum.TryParse(typeof(Head), left, true, out var parsedLeft);
            Enum.TryParse(typeof(Head), right, true, out var parsedRight);

            if (parsedRight is null && parsedLeft is null) return 0;
            if (parsedLeft is null) return -1;
            if (parsedRight is null) return 1;

            return (int)parsedLeft - (int)parsedRight;
        }
    }
}
