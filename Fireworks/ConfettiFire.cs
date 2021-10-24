using FactoryPattern.DrawPrimitives;
using System;

namespace FactoryPattern.Fireworks
{
    public class ConfettiFire : IFirework
    {
        public int X { get; private set; }

        public int ZIndex { get; private set; }

        private char[] _chars;
        private ConsoleColor _color;

        private int _frameId;
        private float _speed;
        private float _distanse;

        const int _lifetime = 26;
        const int _explosionHeight = 15;


        public ConfettiFire(int x, int zIndex,ConsoleColor color,char[] chars)
        {
            _frameId = 0;
            if (chars.Length<1)
            {
                throw new ArgumentException("Chars count must 1+");
            }
            _chars = chars;
            _color = color;

            X = x;
            ZIndex = zIndex;

            _distanse = 0;
            _speed = 1.1f;
        }

        

        public PixelList NextFrame()
        {
            if (_frameId++ <= _lifetime && _speed > 0.1f)
            {
                PixelList result = new PixelList();
                if (_frameId < _explosionHeight)
                {
                    result.Add(
                        new Coordinate
                        {
                            X = X,
                            Y = _frameId
                        },
                        new Pixel
                        {
                            Char = _chars[0],
                            Color = _color,
                            ZIndex = ZIndex
                        });

                    result.Add(
                        new Coordinate
                        {
                            X = X,
                            Y = _frameId-1
                        },
                        new Pixel
                        {
                            Char = '^',
                            Color = _color,
                            ZIndex = ZIndex
                        });
                }
                else
                {
                    Pixel star = new Pixel
                    {
                        Char = _chars[1],
                        Color = _color,
                        ZIndex = ZIndex
                    };

                    _distanse += (_speed -= 0.03f);
                    int explosionDistance = (int)_distanse;

                    result.Add(
                        new Coordinate
                        {
                            X = X,
                            Y = _explosionHeight+1 + (int)(explosionDistance * 0.3)
                        },
                        star
                        );

                    result.Add(
                        new Coordinate
                        {
                            X = X + explosionDistance,
                            Y = _explosionHeight + (int)(explosionDistance * -0.8)
                        },
                        star
                        );

                    result.Add(
                        new Coordinate
                        {
                            X = X  -explosionDistance,
                            Y = _explosionHeight + (int)(explosionDistance * -0.8)
                        },
                        star
                        );

                    result.Add(
                        new Coordinate
                        {
                            X = X - explosionDistance,
                            Y = _explosionHeight + (int)(explosionDistance * -0.3)
                        },
                        star
                        );

                    result.Add(
                        new Coordinate
                        {
                            X = X + explosionDistance,
                            Y = _explosionHeight + (int)(explosionDistance * -0.3)
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
