using ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static ifmo_ca_lab_3.Evaluation.Heads;

namespace ifmo_ca_lab_3.Evaluation.Commons
{
    class ExpressionComparer : Comparer<IExpression>
    {
        public override int Compare(IExpression left, IExpression right)
        {
            if (left is Value) return -1;
            if (right is Value) return 1;
            string leftKey, rightKey;
            if (left is Expression && ((Expression)left).Head == nameof(Heads.Symbol))
            {
                leftKey = ((Expression)left).Key.ToString();
            }
            else
            {
                leftKey = ((Expression)left).Operands.First().Key.ToString();
            }
            if (right is Expression && ((Expression)right).Head == nameof(Heads.Symbol))
            {
                rightKey = ((Expression)right).Key.ToString();
            }
            else
            {
                rightKey = ((Expression)right).Operands.First().Key.ToString();
            }
            if (leftKey == rightKey) return 0;
            return string.Compare(leftKey, rightKey);
        }
    }
}
