    using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Core
{
    public static class Evaluator
    {
        public static int Evaluate(Integer element)
        {
            return element.Value;
        }

        public static string Evaluate(Symbol element)
        {
            return element.Value;
        }

        public static IElement Evaluate(Expression element)
        {
            return default;
        }

        //public static ElementsList Evaluate(ElementsList elementsList)
        //{
        //    for (var index = 0; index < elementsList.Count; index++)
        //    {
        //        elementsList[index] = Evaluate(elementsList[index]);
        //    }
        //    return elementsList;
        //}
    }
}
