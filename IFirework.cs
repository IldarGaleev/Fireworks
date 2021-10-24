using FactoryPattern.DrawPrimitives;

namespace FactoryPattern
{
    public interface IFirework
    {

        int X { get; }
        int ZIndex { get; }

        PixelList NextFrame();
    }
}
