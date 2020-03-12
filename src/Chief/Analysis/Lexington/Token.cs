namespace ShiftCo.ifmo_ca_lab_3.Analysis.Lexington
{
    struct Token
    {
        public int type;
        public string content;

        public Token(int tokenType, string tokenContent)
        {
            type = tokenType;
            content = tokenContent;
        }
    }
}
