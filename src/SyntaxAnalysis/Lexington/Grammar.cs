using System.Collections.Generic;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington
{
    static class Grammar
    {
        public static Dictionary<string, string> Rules = new Dictionary<string, string>();

        static Grammar()
        {
            Rules.Add("comma", $",");
            Rules.Add("leftbracket", $",");
            Rules.Add("rightbracket", $",");
            Rules.Add("digit", $"0|1|2|3|4|5|6|7|8|9");
            Rules.Add("letter", $"a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z");
            Rules.Add("number", $"[{Rules["digit"]}]+");
            Rules.Add("symbol", $"({Rules["letter"]})({Rules["letter"]}|{Rules["digit"]})*");
        }
    }
}
