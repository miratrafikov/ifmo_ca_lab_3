using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Util
{
    internal class ElementComparer : Comparer<IElement>
    {
        public override int Compare(IElement left, IElement right)
        {
            // To avoid exceptions
            if (left is IPattern || right is IPattern) return 0;
            if (right is null) return 1;
            if (left is null) return -1;

            // Compare Heads first
            if (string.Compare(left.Head, right.Head) != 0) return string.Compare(left.Head, right.Head);

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
    }
}
