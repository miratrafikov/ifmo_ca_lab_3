using System.Collections.Generic;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.AInterfaces
{
    public interface IAttribute
    {
        public List<IExpression> Apply(Expression expr);
    }
}
