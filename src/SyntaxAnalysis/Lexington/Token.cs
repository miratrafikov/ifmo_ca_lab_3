namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington
{
    public struct Token
    {
        public TokenType Type { get; }
        public string Content { get; }

        public Token(TokenType type, string content)
        {
            Type = type;
            Content = content;
        }
    }
}
