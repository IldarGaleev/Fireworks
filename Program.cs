using FactoryPattern.FireworkGuns;
using FactoryPattern.DrawPrimitives;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;

namespace FactoryPattern
{
    class Program
    {

        static int windowWidth = 132;
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

            InitFireCannons(fireworkGuns);                
            
            using (Stream stdOutputStream = Console.OpenStandardOutput())
            {
                using (StreamWriter stdOutStreamWriter = new StreamWriter(stdOutputStream))
                {
                    stdOutStreamWriter.Write("\x1b[?1049h");//vt-100 set alternative screen buffer
                    stdOutStreamWriter.Write("\x1b[?25l");//vt-100 hide cursor
                    stdOutStreamWriter.Write("\x1b[?3l");//vt-100 buffer width 132 column
                    
                    //main routine
                    while (run)
                    {
                        lock (fpsLock)
                        {
                            fps++;
                        }

                        //if all cannons has fired, reinit list
                        if (cannonsQueue.Count == 0)
                        {
                            cannonsQueue.AddRange(Enumerable.Range(0, fireworkGuns.Count - 1));
                        }

                        stdOutStreamWriter.Write("\x1b[2J");//vt-100 clear screen
                        stdOutStreamWriter.Write("\x1b[?25l");//vt-100 hide cursor

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
                        fireworks = fireworks.Except(deadFireworks).ToList();
                        deadFireworks.Clear();

                        //display info
                        stdOutStreamWriter.Write($"\x1b[0;0f");//vt-100 set cursor position 0,0
                        stdOutStreamWriter.WriteLine($"fireworks count: {fireworks.Count}  ");
                        stdOutStreamWriter.WriteLine($"fps: {fpsResult}  ");

                        DrawFrame(stdOutStreamWriter,currentFramePixels);
                        
                        //try draw current frame
                        try
                        {
                            stdOutStreamWriter.Flush();//write all data to std stream
                        }
                        catch (Exception)
                        {
                            ReinitTerminalWindow(stdOutStreamWriter);
                        }
                        

                        //frame delay
                        Thread.Sleep(frameDelayMs);

                        //read key
                        try
                        {
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
                        }
                        catch (Exception ex)
                        {
                            run = false;
                            stdOutStreamWriter.Close();
                            return;
                            //Console.WriteLine(ex.Message);
                        }

                    }//while (run)

                    stdOutStreamWriter.Write("\x1b[?1049l");//vt-100 set main screen buffer

                }//using (stdOutStreamWriter)
            }//using (stdOutputStream)

            //program complete routine
            Console.WriteLine("Firework show complete");            
        }

        private static void ReinitTerminalWindow(StreamWriter stdOutStreamWriter)
        {
            stdOutStreamWriter.Write("\x1b[2J");//vt-100 clear screen
            stdOutStreamWriter.Write("\x1b[33m");//vt-100 set default text format
            stdOutStreamWriter.Write("Please, resize terminal and press any key...");
            //Console.ReadKey(true);
            stdOutStreamWriter.Write("\x1b[0m");//vt-100 set default text format
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
                        fireworkCannons.Add(new ConfettiGun((i + 1) * 15, 1, PixelColor.Green, new char[] { ':', 'x' }));
                    }
                }
        }


        /// <summary>
        /// Draw frame
        /// </summary>
        /// <param name="pixels">Pixel info list</param>
        /// <param name="clear">If True - pixels will clean</param>
        private static void DrawFrame(StreamWriter stdOutStreamWriter,PixelList pixels,bool clear=false)
        { 
            foreach (var item in pixels)
            {
                if (item.Key.X >= 0&& item.Key.X<windowWidth && item.Key.Y >= 0 && item.Key.Y<windowHeight)
                {
                    stdOutStreamWriter.Write($"\x1b[{30 - item.Key.Y};{item.Key.X}f");
                    if (clear)
                    {
                        stdOutStreamWriter.Write(' ');
                    }
                    else
                    {
                        stdOutStreamWriter.Write($"\x1b[38;2;{item.Value.Color.R};{item.Value.Color.G};{item.Value.Color.B}m");
                        stdOutStreamWriter.Write(item.Value.Char);
                    }                    
                }
            }
            stdOutStreamWriter.Write("\x1b[0m");//vt-100 set default text format
        }
    }
}
