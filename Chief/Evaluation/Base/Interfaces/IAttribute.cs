namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Base.Interfaces
{
    public interface IAttribute
    {
        public string Key { get; set; }
        public IOperand Apply(IOperand expr);
    }
}
