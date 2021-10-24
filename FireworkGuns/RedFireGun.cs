using FactoryPattern.Fireworks;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryPattern.FireworkGuns
{
    public class RedFireGun : FireworkCreator
    {



        public RedFireGun(int x, int zIndex) : base(x, zIndex) { }

        
        public override IFirework Fire()
        {
            return new RedFire(X, ZIndex);
        }
    }
}
