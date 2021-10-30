using FactoryPattern.DrawPrimitives;
using System;

namespace FactoryPattern.Fireworks
{
    public class RedFire : IFirework
    {
        private int _frameId;
        readonly PixelColor _color = PixelColor.Red;

        const int _lifetime = 10;
        const int _explosionHeight = 5;

        public RedFire(int x,int zIndex)
        {
            _frameId = 0;

            X = x;
            ZIndex = zIndex;
        }

        public int X {get;private set;}

        public int ZIndex { get; private set; }

        public PixelList NextFrame()
        {
            if (_frameId++ <= _lifetime)
            {
                PixelList result = new PixelList();
                if (_frameId< _explosionHeight)
                {
                    result.Add(
                        new Coordinate { 
                            X = X, 
                            Y = _frameId 
                        }, 
                        new Pixel { 
                            Char = '|', 
                            Color = _color, 
                            ZIndex = ZIndex 
                        });
                }
                else
                {
                    Pixel star = new Pixel
                    {
                        Char = '*',
                        Color = _color,
                        ZIndex = ZIndex
                    };

                    int explosionDistance = _frameId - _explosionHeight;
                    
                    result.Add(
                        new Coordinate
                        {
                            X = X+ explosionDistance,
                            Y = _frameId
                        },
                        star
                        );

                    result.Add(
                        new Coordinate
                        {
                            X = X - explosionDistance,
                            Y = _frameId
                        },
                        star
                        );

                    result.Add(
                         new Coordinate
                         {
                             X = X + explosionDistance,
                             Y = _frameId - explosionDistance
                         },
                         star
                         );

                    result.Add(
                        new Coordinate
                        {
                            X = X - explosionDistance,
                            Y = _frameId - explosionDistance
                        },
                        star
                        );
                }
                return result;
            }

            return PixelList.Empty;            
        }
    }
}
