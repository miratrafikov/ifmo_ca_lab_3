namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces
{
    public interface IAttribute
    {
        public string Key { get; set; }
        public IOperand Apply(IOperand expr);
    }
}
