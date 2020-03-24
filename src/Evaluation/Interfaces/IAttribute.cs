using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces
{
    public interface IAttribute
    {
        public Expression Apply(Expression expr);
    }
}
