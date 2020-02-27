using System;

namespace ifmo_ca_lab_3.Lexical
{
    struct Token
    {
        public int type;
        public string content;
        public int startPos;
        public string typeDescription;

        public Token(int tokenType, string tokenContent, int tokenStartPos)
        {
            type = tokenType;
            content = tokenContent;
            startPos = tokenStartPos;
            typeDescription = Enum.GetName(typeof(TokenTypes), type);
        }
    }

    public enum TokenTypes
    {
        Add,
        Sub,
        Mul,
        Pow,
        Variable,
        Number,
        LeftBracket,
        RightBracket,
        Comma,
        Space
    }
}
