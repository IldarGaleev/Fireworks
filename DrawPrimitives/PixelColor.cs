using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryPattern.DrawPrimitives
{
    public struct PixelColor
    {

        public static readonly PixelColor Black = new PixelColor(0, 0, 0);
        public static readonly PixelColor Red = new PixelColor(255, 0, 0);
        public static readonly PixelColor Green = new PixelColor(0, 255, 0);
        public static readonly PixelColor Blue = new PixelColor(0, 0, 255);
                      
        public static readonly PixelColor Yellow = Red + Green;
        public static readonly PixelColor Magenta = Red + Blue;
        public static readonly PixelColor White = Red + Green + Blue;



        /// <summary>
        /// Red component
        /// </summary>
        public byte R;

        /// <summary>
        /// Green component
        /// </summary>
        public byte G;

        /// <summary>
        /// Blue component
        /// </summary>
        public byte B;

        public PixelColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static PixelColor operator +(PixelColor a, PixelColor b)
        {
            int newR = (a.R + b.R);
            int newG = (a.G + b.G);
            int newB = (a.B + b.B);

            return new PixelColor(
                                    (byte)(newR > 0xff ? 0xff : newR),
                                    (byte)(newG > 0xff ? 0xff : newG),
                                    (byte)(newB > 0xff ? 0xff : newB)
                                );
        }

    }


}
