using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.AInterfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Tony
{
    public static class Context
    {
        private static Dictionary<IExpression, IExpression> Entries = new Dictionary<IExpression, IExpression>();

        public static void AddEntry(IExpression key, IExpression value)
        {
            Entries.Add(key, value);
        }

        public static IExpression GetSubstitute(IExpression expr)
        {
            var substitute = expr;
            while (Entries.ContainsKey(substitute))
            {
                substitute = Entries[substitute];
            }
            return substitute;
        }
    }
}
