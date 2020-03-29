using System;

using CliFx;

namespace ShiftCo.ifmo_ca_lab_3.Chief.IOUtils
{
    public static class IOBeautifier
    {
        private static readonly int s_columnIndent = 20;
        private static readonly ConsoleColor s_headerColor = ConsoleColor.Magenta;
        private static readonly ConsoleColor s_mainColumnColor = ConsoleColor.White;
        private static readonly ConsoleColor s_secondaryColumnColor = ConsoleColor.Gray;

        public static void PrintHeader(IConsole console, string content)
        {
            console.WithForegroundColor(s_headerColor, () => console.Output.WriteLine(content));
        }

        public static void PrintPair(IConsole console, string main, string secondary)
        {
            console.Output.Write("  ");
            console.WithForegroundColor(s_mainColumnColor, () => console.Output.Write(main.PadRight(s_columnIndent)));
            console.WithForegroundColor(s_secondaryColumnColor, () => console.Output.WriteLine(secondary));
        }
    }
}
