using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util
{
    class ElementsList : List<IElement>
    {
        public IElement GetHead()
        {
            return this[0];
        }

        public ElementsList GetTail()
        {
            return (ElementsList)this.GetRange(1, this.Count - 1);
        }
    }
}
