using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Commons.Converter;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation
{
    public class Symbol : IExpression
    {
        public Symbol(string key)
        {
            Head = "Symbol";
            Key = key;
        }

        public string Head { get; set; }
        public object Key { get; set; }
        public void Evaluate() { }
        public bool IsAlike(IExpression iexpr)
        {
            if (iexpr is Symbol) return Key.Equals(iexpr.Key);
            if (iexpr is Expression && ToExpression(iexpr).Head == nameof(Heads.Mul))
            {
                if (ToExpression(iexpr).Operands.Count == 2 &&
                    ToExpression(iexpr).Operands.First().Head == nameof(Heads.Value))
                    return IsAlike(ToExpression(iexpr).Operands[1]);
            }
            if (iexpr is Expression)
            {
                if (ToExpression(iexpr).Operands.Count > 0) return false;
                return Key.Equals(iexpr.Key);
            }
            return false;
        }
    }
}
