﻿using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Commons
{
    public static class Converter
    {
        public static Expression ToExpression(IExpression obj)
        {
            if (obj is Expression)
            {
                return (Expression)obj;
            }
            else
            {
                return default;
            }
        }
        public static Symbol ToSymbol(IExpression obj)
        {
            if (obj is Symbol)
            {
                return (Symbol)obj;
            }
            else
            {
                return default;
            }
        }
        public static Value ToValue(IExpression obj)
        {
            if (obj is Value)
            {
                return (Value)obj;
            }
            else
            {
                return default;
            }
        }
    }
}