using System;

namespace FactoryPattern.DrawPrimitives
{
    public class Pixel
    {
        public char Char { get; set; }
        public ConsoleColor Color { get; set; }

        public int ZIndex { get; set; }

        public static bool operator <(Pixel A, Pixel B)
        {
            return A.ZIndex < B.ZIndex;
        }

        public static bool operator >(Pixel A, Pixel B)
        {
            return A.ZIndex > B.ZIndex;
        }
    }
}