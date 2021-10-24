using FactoryPattern.FireworkGuns;
using FactoryPattern.DrawPrimitives;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace FactoryPattern
{
    class Program
    {

        static int windowWidth=150;
        static int windowHeight=32;


        public static void Main(string[] args)
        {
            List<FireworkCreator> fireworkGuns = new List<FireworkCreator>();

            List<IFirework> fireworks = new List<IFirework>();
            List<IFirework> deadFireworks = new List<IFirework>();

            List<int> gunId = new List<int>(fireworkGuns.Count);

            PixelList pixels = new PixelList();
            
            bool run = true;

            Random random = new Random(34356);    

            Console.CursorVisible = false;
            Console.SetWindowSize(windowWidth, windowHeight);    

            InitFireGuns(fireworkGuns);                
            
            Console.Clear();
            
            while (run)
            {
                if (gunId.Count==0)
                {
                    for (int i = 0; i < fireworkGuns.Count; i++)
                    {
                        gunId.Add(i);
                    }
                }

                RedrawConsole(pixels, true);
                
                pixels.Clear();

                foreach (var item in fireworkGuns)
                {
                    pixels.Add(item.Draw());
                }

                foreach (var fire in fireworks)
                {
                    var nextFrame = fire.NextFrame();
                    if (nextFrame.Count == 0)
                    {
                        deadFireworks.Add(fire);
                    }
                    else
                    {
                        foreach (var item in nextFrame)
                        {
                            pixels.Add(item);
                        }
                    }
                }


                fireworks=fireworks.Except(deadFireworks).ToList();
                deadFireworks.Clear();

                RedrawConsole(pixels);
                

                Thread.Sleep(50);

                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Escape: run = false; break;
                        case ConsoleKey.F:
                        int r = random.Next(0, gunId.Count - 1);
                            
                        fireworks.Add(fireworkGuns[gunId[r]].Fire());
                        gunId.RemoveAt(r);
                        break;
                        default: break;
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Firework show complete");
        }

        private static void InitFireGuns(List<FireworkCreator> fireworkGuns)
        {
            for (int i = 0; i < 9; i++)
                {
                    if (i % 2 == 0)
                    {
                        fireworkGuns.Add(new RedFireGun((i + 1) * 15, 1));
                    }
                    else
                    {
                        fireworkGuns.Add(new ConfettiGun((i + 1) * 15, 1, ConsoleColor.Green, new char[] { ':', 'x' }));
                    }
                }
        }

        private static void RedrawConsole(PixelList pixels,bool clear=false)
        { 
            foreach (var item in pixels)
            {
                if (item.Key.X >= 0&& item.Key.X<windowWidth && item.Key.Y >= 0 && item.Key.Y<windowHeight)
                {
                    Console.SetCursorPosition(item.Key.X, 30 - item.Key.Y);
                    if (clear)
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.ForegroundColor = item.Value.Color;
                        Console.Write(item.Value.Char);
                    }                    
                }
            }
            Console.ResetColor();
        }
    }
}
