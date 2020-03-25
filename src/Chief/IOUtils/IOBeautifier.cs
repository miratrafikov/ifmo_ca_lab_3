using System;

using CliFx;

namespace ShiftCo.ifmo_ca_lab_3.Chief.IOUtils
{
    public static class IOBeautifier
    {
        private static int columnIndent = 20;
        private static readonly ConsoleColor headerColor = ConsoleColor.Magenta;
        private static readonly ConsoleColor mainColumnColor = ConsoleColor.White;
        private static readonly ConsoleColor secondaryColumnColor = ConsoleColor.Gray;

        public static void PrintHeader(IConsole console, string content)
        {
            console.WithForegroundColor(headerColor, () => console.Output.WriteLine(content));
        }

        public static void PrintPair(IConsole console, string main, string secondary)
        {
            console.Output.Write("  ");
            console.WithForegroundColor(mainColumnColor, () => console.Output.Write(main.PadRight(columnIndent)));
            console.WithForegroundColor(secondaryColumnColor, () => console.Output.WriteLine(secondary));
        }
    }
}
