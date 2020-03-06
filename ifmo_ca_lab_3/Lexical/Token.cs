using System;

namespace ifmo_ca_lab_3.Lexical
{
    struct Token
    {
        public int typeId;
        public string content;
        public int startPos;
        public string typeDescription;

        public Token(int tokenType, string tokenContent, int tokenStartPos)
        {
            typeId = tokenType;
            content = tokenContent;
            startPos = tokenStartPos;
            typeDescription = Enum.GetName(typeof(TokenTypes), typeId);
        }
    }
}
