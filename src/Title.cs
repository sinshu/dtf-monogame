using System;

namespace MiswGame2008
{
    public class Title
    {
        private const int NUM_STARS = 128;

        private Random random;

        private bool startGame;
        private bool exitGame;
        private int startCount;

        private int[] starX;
        private int[] starY;

        private int ticks;

        public Title(Random random)
        {
            this.random = random;
            startGame = false;
            exitGame = false;
            startCount = 0;
            starX = new int[NUM_STARS];
            starY = new int[NUM_STARS];
            for (int i = 0; i < NUM_STARS; i++)
            {
                starX[i] = random.Next(-2, 640 + 2 + 1);
                starY[i] = random.Next(-2, 480 + 2 + 1);
            }
            ticks = 0;
        }

        public void Update(UserCommand command)
        {
            if (command.Exit)
            {
                exitGame = true;
            }
            else if (command.Button1)
            {
                startGame = true;
            }

            if (startGame)
            {
                if (startCount < 9)
                {
                    startCount++;
                }
            }

            for (int i = 0; i < NUM_STARS / 4; i++)
            {
                starY[i] += 3;
                if (starY[i] > 480 + 2)
                {
                    starX[i] = random.Next(-2, 640 + 2 + 1);
                    starY[i] = -2;
                }
            }
            for (int i = NUM_STARS / 4; i < NUM_STARS / 2; i++)
            {
                starY[i] += 2;
                if (starY[i] > 480 + 2)
                {
                    starX[i] = random.Next(-2, 640 + 2 + 1);
                    starY[i] = -2;
                }
            }
            for (int i = NUM_STARS / 2; i < NUM_STARS; i++)
            {
                starY[i] += 1;
                if (starY[i] > 480 + 2)
                {
                    starX[i] = random.Next(-2, 640 + 2 + 1);
                    starY[i] = -2;
                }
            }

            ticks++;
        }

        public void Draw(IGraphics graphics)
        {
            graphics.SetColor(255, 0, 0, 0);
            graphics.Fill();
            int c = ticks % 384;
            int r, g, b;
            if (c < 64)
            {
                r = 255;
                g = c * 4;
                b = 0;
            }
            else if (c < 128)
            {
                r = (63 - (c - 64)) * 4;
                g = 255;
                b = 0;
            }
            else if (c < 192)
            {
                r = 0;
                g = 255;
                b = (c - 128) * 4;
            }
            else if (c < 256)
            {
                r = 0;
                g = (63 - (c - 192)) * 4;
                b = 255;
            }
            else if (c < 320)
            {
                r = (c - 256) * 4;
                g = 0;
                b = 255;
            }
            else
            {
                r = 255;
                g = 0;
                b = (63 - (c - 320)) * 4;
            }
            graphics.SetColor(255, r, g, b);
            graphics.EnableAddBlend();
            for (int i = 0; i < 128; i++)
            {
                int w = (int)Math.Round(8 * Utility.Sin(i * 2 + ticks * 4));
                graphics.DrawImage(Image.Dtf, 64 + w, 96 + i, 512, 1, i, 0);
            }
            graphics.SetColor(255, 255, 255, 255);
            /*
            for (int i = 0; i < NUM_STARS / 4; i++)
            {
                graphics.DrawImage(Image.Star, starX[i] - 4, starY[i] - 4, 8, 8, 0, 0);
            }
            */
            for (int i = NUM_STARS / 4; i < NUM_STARS / 2; i++)
            {
                graphics.DrawImage(Image.Star, starX[i] - 4, starY[i] - 4, 8, 8, 0, 1);
            }
            graphics.SetColor(255, 128, 128, 128);
            for (int i = NUM_STARS / 2; i < NUM_STARS; i++)
            {
                graphics.DrawImage(Image.Star, starX[i] - 4, starY[i] - 4, 8, 8, 0, 1);
            }
            graphics.DisableAddBlend();
            if (startGame)
            {
                int a = startCount * 32;
                if (a > 255)
                {
                    a = 255;
                }
                graphics.SetColor(a, 255, 255, 255);
                graphics.Fill();
            }
            else
            {
                if (ticks / 15 % 2 == 0)
                {
                    graphics.SetColor(255, 255, 255, 255);
                    graphics.DrawString("PRESS ANY BUTTON TO START", 120, 480 - 128 - 8);
                }
            }
        }

        public bool StartGame
        {
            get
            {
                return startCount == 9;
            }
        }

        public bool ExitGame
        {
            get
            {
                return exitGame;
            }
        }

        public bool Ranking
        {
            get
            {
                return !(ticks < 600);
            }
        }
    }
}
