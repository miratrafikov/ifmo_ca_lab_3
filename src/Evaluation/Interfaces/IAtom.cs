namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces
{
    internal interface IAtom<T> : IElement
    {
        public T Value { get; set; }
    }
}
