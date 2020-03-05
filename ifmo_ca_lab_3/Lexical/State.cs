namespace ifmo_ca_lab_3.Lexical
{
    static class State
    {
        public static int currPos = -1;
        public static int tokenExpected = (int)TokenExpectations.No;
        public static int tokenStartPos;
    }
}
