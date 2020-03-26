using System;
using System.Collections.Generic;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.EnumUtilizer;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington
{
    static class Grammar
    {
        private static readonly Dictionary<string, string> _rules = new Dictionary<string, string>();

        public static Dictionary<TokenType, string> TokenDefinitions = new Dictionary<TokenType, string>();

        static Grammar()
        {
            // Правила грамматики
            _rules.Add("Comma", $"^,");
            _rules.Add("LeftBracket", $"^\\(");
            _rules.Add("RightBracket", $"^\\)");
            _rules.Add("Digit", $"^0|1|2|3|4|5|6|7|8|9");
            _rules.Add("Letter", $"^a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z");
            _rules.Add("Underscores", $"^___|_");
            _rules.Add("Number", $"^[-+]?({_rules["Digit"]})+");
            _rules.Add("Symbol", $"^({_rules["Letter"]})({_rules["Letter"]}|{_rules["Digit"]})*");

            // Соответствие между правилами и определениями токенов
            foreach (TokenType tokenType in Enum.GetValues(typeof(TokenType)))
            {
                if (tokenType != TokenType.EOF)
                {
                    TokenDefinitions.Add(tokenType, _rules[UniformEnumToString(tokenType, Capitalization.AsListed)]);
                }
            }
        }
    }
}
