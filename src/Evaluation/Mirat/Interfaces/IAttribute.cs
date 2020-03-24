using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces
{
    public interface IAttribute
    {
        public Expression Apply(Expression expr);
    }
}
