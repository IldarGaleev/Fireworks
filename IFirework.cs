using FactoryPattern.DrawPrimitives;

namespace FactoryPattern
{
    /// <summary>
    /// Firework interface
    /// </summary>
    public interface IFirework
    {
        /// <summary>
        /// X position
        /// </summary>
        int X { get; }

        /// <summary>
        /// Layer. Higher value is closer 
        /// </summary>
        int ZIndex { get; }

        /// <summary>
        /// Get next frame
        /// </summary>
        /// <returns>Pixel info list for draw new frame</returns>
        PixelList NextFrame();
    }
}
