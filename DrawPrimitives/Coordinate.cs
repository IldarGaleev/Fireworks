using System;

namespace FactoryPattern.DrawPrimitives
{
    public class Coordinate: IComparable<Coordinate>
    {
        public int X { get; set; }
        public int Y { get; set; }
        

        public int CompareTo(Coordinate other)
        {

            if (other.X != X)
            {
                return X.CompareTo(other.X);
            }

            if (other.Y != Y)
            {
                return Y.CompareTo(other.Y);
            }

            return 0;
        }

        
    }
}