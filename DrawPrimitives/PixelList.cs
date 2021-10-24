using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FactoryPattern.DrawPrimitives
{
    public class PixelList : Dictionary<Coordinate, Pixel>
    {

        public static PixelList Empty = new PixelList();

        public new Pixel this[Coordinate key] { 
            get => base[key];
            set
            {
                if (base.ContainsKey(key))
                {
                    if (base[key]<value)
                    {
                        base[key] = value;
                    }
                }
                else
                {
                    base.Add(key, value);
                }
            } 
        }


        public new void Add(Coordinate key, Pixel value)
        {
            this[key] = value;
        }

        public void Add(KeyValuePair<Coordinate,Pixel> item)
        {
            this[item.Key] = item.Value;
        }

        public void Add(ICollection<KeyValuePair<Coordinate,Pixel>> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

    }
}
