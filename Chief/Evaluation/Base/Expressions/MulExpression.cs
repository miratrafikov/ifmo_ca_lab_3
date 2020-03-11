using ShiftCo.ifmo_ca_lab_3.Evaluation.Base.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Base.Expressions
{
    public class MulExpression : Expression
    {
        public MulExpression()
        {
            this.Operands = new List<IOperand> { };
            this.Attributes = new List<IAttribute> { };
        }
        public MulExpression(List<IOperand> operands)
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
                    ((Expression)operand).Evaluate();
                    if (((Expression)operand).IsPrimitive())
                    {
                        Operands.Add(((Expression)operand).Operands.First());
                    }
                }
            }
            /*int mulOfPrimitives = Operands.Where(o => o is Expression && ((Expression)o).IsPrimitive()).Aggregate(1, (acc, val) => acc * ((Value)((Expression)val).Operands.First()).Key);
            Operands.Add(new Value(mulOfPrimitives));
            Operands.RemoveAll(o => o is Expression && ((Expression)o).IsPrimitive());*/
            RemovePrimitives();
            // apply attributes
            foreach (IAttribute attr in Attributes)
            {
                attr.Apply(this);
            }
            // apply given defenitions
            // apply built-in definitions
            // primitive product
            var product = 1;
            foreach (IOperand operand in Operands)
            {
                if (operand is Value) product *= ((Value)operand).Key;
            }
            Operands.RemoveAll(o => o is Value);
            Operands.Add(new Value(product));
            // раскрытие скобачег
        }

        public bool IsPrimitive() => (Operands.Count == 1 && Operands.First() is Value) ? true : false;
    }
}
