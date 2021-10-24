using FactoryPattern.DrawPrimitives;
using FactoryPattern.Fireworks;
using System;
using System.Collections.Generic;

namespace FactoryPattern.FireworkGuns
{
    public class ConfettiGun : FireworkCreator
    {

        private ConsoleColor _color;
        private char[] _chars;

        public ConfettiGun(int x,int zIndex,ConsoleColor color, char[] chars):base(x,zIndex)
        {
            if (chars.Length<1)
            {
                throw new ArgumentException("Char count must be >1");
            }
            _chars = chars;
            _color = color;

            _creatorDrawPixels = new PixelList() {
                new KeyValuePair<Coordinate, Pixel> (
                    
                    new Coordinate {
                        X=X,
                        Y=0
                    },
                    new Pixel {
                        Char='U',
                        Color=_color,
                        ZIndex=zIndex+1
                    } ) 
            };
        }

        public override IFirework Fire()
        {
            return new ConfettiFire(X, ZIndex, _color, _chars);
        }

        
    }
}
