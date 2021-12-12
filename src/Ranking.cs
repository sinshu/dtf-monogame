using System;

namespace MiswGame2008
{
    public class Ranking
    {
        private static string[] RANK = {
            "1ST",
            "2ND",
            "3RD",
            "4TH",
            "5TH",
            "6TH",
            "7TH",
            "8TH",
            "9TH",
            "#TH"
        };

        private int CIRCLE_COUNT = 192;
        private int CIRCLE_RADIUS = 32;
        private double CIRCLE_MAX_SPEED = 4;

        private TopPlayerInfo[] top10Players;

        private bool returnToTitle;

        private Random random;
        private double[] circleX;
        private double[] circleY;
        private double[] circleVX;
        private double[] circleVY;

        private int ticks;

        public Ranking(TopPlayerInfo[] top10Players, Random random)
        {
            this.top10Players = top10Players;
            this.random = random;
            circleX = new double[CIRCLE_COUNT];
            circleY = new double[CIRCLE_COUNT];
            circleVX = new double[CIRCLE_COUNT];
            circleVY = new double[CIRCLE_COUNT];
            for (int i = 0; i < CIRCLE_COUNT; i++)
            {
                circleX[i] = random.NextDouble() * (640 + CIRCLE_RADIUS * 2) - CIRCLE_RADIUS;
                circleY[i] = random.NextDouble() * (480 + CIRCLE_RADIUS * 2) - CIRCLE_RADIUS;
                circleVX[i] = random.NextDouble() * CIRCLE_MAX_SPEED * 2 - CIRCLE_MAX_SPEED;
                circleVY[i] = random.NextDouble() * CIRCLE_MAX_SPEED * 2 - CIRCLE_MAX_SPEED;
            }
            ticks = 0;
        }

        public void Update(UserCommand command)
        {
            if (command.Button1)
            {
                returnToTitle = true;
            }
            for (int i = 0; i < CIRCLE_COUNT; i++)
            {
                circleVX[i] += 2 * random.NextDouble() - 1;
                circleVY[i] += 2 * random.NextDouble() - 1;
                if (Math.Abs(circleVX[i]) > CIRCLE_MAX_SPEED)
                {
                    circleVX[i] = Math.Sign(circleVX[i]) * CIRCLE_MAX_SPEED;
                }
                if (Math.Abs(circleVY[i]) > CIRCLE_MAX_SPEED)
                {
                    circleVY[i] = Math.Sign(circleVY[i]) * CIRCLE_MAX_SPEED;
                }
                circleX[i] += circleVX[i];
                circleY[i] += circleVY[i];
                if (circleX[i] < -CIRCLE_RADIUS)
                {
                    circleX[i] = 640 + CIRCLE_RADIUS;
                }
                else if (circleX[i] > 640 + CIRCLE_RADIUS)
                {
                    circleX[i] = -CIRCLE_RADIUS;
                }
                if (circleY[i] < -CIRCLE_RADIUS)
                {
                    circleY[i] = 480 + CIRCLE_RADIUS;
                }
                else if (circleY[i] > 480 + CIRCLE_RADIUS)
                {
                    circleY[i] = -CIRCLE_RADIUS;
                }
            }
            if (ticks < 450)
            {
                ticks++;
            }
            if (ticks == 450)
            {
                returnToTitle = true;
            }
        }

        public void Draw(IGraphics graphics)
        {
            if (ticks < 445)
            {
                graphics.SetColor(255, 0, 0, 0);
                graphics.Fill();

                graphics.EnableAddBlend();
                for (int i = 0; i < CIRCLE_COUNT; i++)
                {
                    int drawX = (int)Math.Round(circleX[i]) - CIRCLE_RADIUS;
                    int drawY = (int)Math.Round(circleY[i]) - CIRCLE_RADIUS;
                    switch (i % 3)
                    {
                        case 0:
                            graphics.SetColor(255, 32, 0, 0);
                            break;
                        case 1:
                            graphics.SetColor(255, 0, 32, 0);
                            break;
                        case 2:
                            graphics.SetColor(255, 0, 0, 32);
                            break;
                    }
                    graphics.DrawImage(Image.Circle, drawX, drawY);
                }
                graphics.DisableAddBlend();

                graphics.SetColor(255, 255, 255, 255);
                graphics.DrawString("TOP SCORES", (640 - 10 * 16) / 2, 48);
                graphics.DrawString("  RANK   SCORE   LEVEL         PLAYER   ", 0, 80);
                for (int i = 0; i < 10; i++)
                {
                    graphics.DrawString(RANK[i], 40, i * 32 + 112);
                    {
                        string s = top10Players[i].Score.ToString();
                        int drawX = 128 + (7 - s.Length) * 16;
                        graphics.DrawString(s, drawX, i * 32 + 112);
                    }
                    {
                        string s = top10Players[i].Level.ToString();
                        int drawX = 296 + (2 - s.Length) * 16;
                        graphics.DrawString(s, drawX, i * 32 + 112);
                    }
                    graphics.DrawString(top10Players[i].Name, 480, i * 32 + 112);
                }
            }
            else
            {
                graphics.SetColor(255, 128, 128, 128);
                graphics.Fill();
                graphics.SetColor(255, 255, 255, 255);
                graphics.DrawImage(Image.Yome, 64, -16, 128, 128, 0, 0, 0, 4);
            }
        }

        public bool ReturnToTitle
        {
            get
            {
                return returnToTitle;
            }
        }
    }
}
