using FactoryPattern.DrawPrimitives;
using System;
using System.Collections.Generic;

namespace FactoryPattern
{
    public abstract class FireworkCreator
    {
        public virtual int X { get; protected set; }
        public virtual int ZIndex { get; protected set; }

        protected PixelList _creatorDrawPixels;

        public FireworkCreator(int x, int zIndex)
        {
            _creatorDrawPixels=new PixelList() {
                new KeyValuePair<Coordinate, Pixel>
                (
                    new Coordinate{X=x,Y=0},
                    new Pixel{Char='Y',Color=PixelColor.Yellow,ZIndex=zIndex+1}
                )
            };

            X = x;
            ZIndex = zIndex;
        }

        public virtual PixelList Draw()
        {
            return _creatorDrawPixels;
        }
        public abstract IFirework Fire();
    }
}
