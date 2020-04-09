namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Util
{
    public static class EnumUtilizer
    {
        public static string UniformEnumToString(object field, Capitalization mode = Capitalization.LowerCase)
        {
            return (mode == Capitalization.LowerCase) ? field.ToString().ToLower() : field.ToString();
        }

        public enum Capitalization
        {
            LowerCase,
            AsListed
        }
    }
}
