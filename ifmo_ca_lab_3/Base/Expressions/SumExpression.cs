using ifmo_ca_lab_3.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ifmo_ca_lab_3.Base.Expressions
{
    class SumExpression : Expression
    {
        public SumExpression()
        {
            this.Operands = new List<IOperand> { };
            this.Attributes = new List<IAttribute> { };
        }
        public SumExpression(List<IOperand> operands)
        {
            this.Operands = operands;
            this.Attributes = new List<IAttribute> { };
        }

        public override void Evaluate()
        {
            //evaluate each elemnt
            foreach (IOperand operand in Operands)
            {
                if (operand is Expression)
                {
                    ((Expression) operand).Evaluate();
                }
            }
            /*int sumOfPrimitives = Operands.Where(o => o is Expression && ((Expression)o).IsPrimitive()).Aggregate(0, (acc, val) => acc + ((Value) ((Expression) val).Operands.First()).Key);
            Operands.Add(new Value(sumOfPrimitives));*/

            RemovePrimitives();

            
            // apply attributes
            foreach (IAttribute attr in Attributes)
            {
                attr.Apply(this);
            }
            // apply given defenitions
            // apply built-in definitions
            var sum = 0;
            foreach (IOperand operand in Operands)
            {
                if (operand is Value) sum += ((Value)operand).Key;
            }
            Operands.RemoveAll(o => o is Value);
            Operands.Add(new Value(sum));
            // приведение слогаемых
        }
    }
}
