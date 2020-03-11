namespace ifmo_ca_lab_3.Analysis.Lexington
{
    static class LexerState
    {
        public static int currPos = 0;
        public static int tokenExpected = (int)TokenExpectations.No;
        public static int tokenStartPos;
    }
}
