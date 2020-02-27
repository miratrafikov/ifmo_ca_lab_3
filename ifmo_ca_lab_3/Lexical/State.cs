namespace ifmo_ca_lab_3.Lexical
{
    static class State
    {
        public static int currPos = -1;
        public static int tokenExpected = (int)TokenExpectations.No;
        public static int tokenStartPos;

        // Перечисление возможных ожиданий токена при считывании следующего символа
        public enum TokenExpectations
        {
            No,
            Letter,
            Number
        }
    }
}
