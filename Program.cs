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

        static int windowWidth = 150;
        static int windowHeight = 32;
        static int frameDelayMs = 20;


        public static void Main(string[] args)
        {
            List<FireworkCreator> fireworkGuns = new List<FireworkCreator>();

            List<IFirework> fireworks = new List<IFirework>();
            List<IFirework> deadFireworks = new List<IFirework>();

            //charged fireworks list
            List<int> cannonsQueue = new List<int>(fireworkGuns.Count);

            //frame buffer
            PixelList currentFramePixels = new PixelList();
            
            bool run = true;

            Random random = new Random(34356); 

            //fps counter
            object fpsLock=new object();
            int fps=0;
            int fpsResult=0;
            Timer fpsTimer=new Timer((e)=>{lock(fpsLock){fpsResult=fps; fps=0;}},null,0,1000);


            Console.CursorVisible = false;

            InitFireCannons(fireworkGuns);                
            
            Console.Clear();
            
            //main routine
            while (run)
            {
                lock(fpsLock){
                    fps++;
                }

                //if all cannons has fired, reinit list
                if (cannonsQueue.Count==0)
                {
                    cannonsQueue.AddRange(Enumerable.Range(0, fireworkGuns.Count - 1));
                }

                //try clear previous frame
                try
                {
                   DrawFrame(currentFramePixels, true);  
                }
                catch (ArgumentOutOfRangeException)
                {
                    ReinitTerminalWindow();
                }
                
                //clear previous frame buffer
                currentFramePixels.Clear();

                //draw (to buffer) guns to 
                foreach (var item in fireworkGuns)
                {
                    currentFramePixels.Add(item.Draw());
                }

                //draw (to buffer) fireworks
                foreach (var fire in fireworks)
                {
                    var nextFrame = fire.NextFrame();

                    //if firework animation complete
                    if (nextFrame.Count == 0)
                    {
                        deadFireworks.Add(fire);
                    }
                    else //if there a new animation frame
                    {
                        //draw (to buffer) next frame animation
                        foreach (var item in nextFrame)
                        {
                            currentFramePixels.Add(item);
                        }
                    }
                }

                //remove completed fire from firework list
                fireworks=fireworks.Except(deadFireworks).ToList();
                deadFireworks.Clear();
                
                //display info
                Console.SetCursorPosition(0,0);
                Console.WriteLine($"fireworks count: {fireworks.Count}  ");
                Console.WriteLine($"fps: {fpsResult}  ");                
                
                //try draw current frame
                try
                {
                   DrawFrame(currentFramePixels);  
                }
                catch (ArgumentOutOfRangeException)
                {
                    ReinitTerminalWindow();
                }
                
                //frame delay
                Thread.Sleep(frameDelayMs);

                //read key
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Escape: run = false; break;
                        case ConsoleKey.F:
                        int nextGunQueueId = random.Next(0, cannonsQueue.Count - 1);
                            
                        fireworks.Add(fireworkGuns[cannonsQueue[nextGunQueueId]].Fire());
                        cannonsQueue.RemoveAt(nextGunQueueId);
                        break;
                        default: break;
                    }
                }

            }//while (run)

            //program complete routine
            Console.Clear();
            Console.WriteLine("Firework show complete");
            Console.CursorVisible = true;
        }

        private static void ReinitTerminalWindow(){
            Console.Clear();
            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine("Please, resize terminal and press any key...");
            Console.ReadKey(true);
            Console.ResetColor();
            Console.Clear();
            Console.CursorVisible = false;
        }

        /// <summary>
        /// init firework cannons
        /// </summary>
        /// <param name="fireworkCannons"></param>
        private static void InitFireCannons(List<FireworkCreator> fireworkCannons)
        {
            for (int i = 0; i < 9; i++)
                {
                    if (i % 2 == 0)
                    {
                        fireworkCannons.Add(new RedFireGun((i + 1) * 15, 1));
                    }
                    else
                    {
                        fireworkCannons.Add(new ConfettiGun((i + 1) * 15, 1, ConsoleColor.Green, new char[] { ':', 'x' }));
                    }
                }
        }


        /// <summary>
        /// Draw frame
        /// </summary>
        /// <param name="pixels">Pixel info list</param>
        /// <param name="clear">If True - pixels will clean</param>
        private static void DrawFrame(PixelList pixels,bool clear=false)
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
