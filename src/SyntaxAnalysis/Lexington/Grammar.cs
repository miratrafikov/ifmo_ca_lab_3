using System;
using System.Collections.Generic;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington
{
    static class Grammar
    {
        private static readonly Dictionary<string, string> Rules = new Dictionary<string, string>();

        public static Dictionary<TokenType, string> TokenDefinitions = new Dictionary<TokenType, string>();

        static Grammar()
        {
            // Правила грамматики
            Rules.Add("Comma", $"^,");
            Rules.Add("LeftBracket", $"^\\(");
            Rules.Add("RightBracket", $"^\\)");
            Rules.Add("Digit", $"^0|1|2|3|4|5|6|7|8|9");
            Rules.Add("Letter", $"^a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z");
            Rules.Add("Number", $"^[-+]?({Rules["Digit"]})+");
            Rules.Add("Symbol", $"^({Rules["Letter"]})({Rules["Letter"]}|{Rules["Digit"]})*");
            
            // Соответствие между правилами и определениями токенов
            foreach (TokenType TokenType in Enum.GetValues(typeof(TokenType)))
            {
                if (TokenType != TokenType.EOF)
                {
                    TokenDefinitions.Add(TokenType, Rules[TokenType.ToString()]);
                }
            }
        }
    }
}
