using System;

namespace MiswGame2008
{
    public class StarBackgroundGame : Game
    {
        private const int NUM_STARS = 128;

        private int[] starX;
        private int[] starY;

        public StarBackgroundGame(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
            starX = new int[NUM_STARS];
            starY = new int[NUM_STARS];
            for (int i = 0; i < NUM_STARS; i++)
            {
                starX[i] = random.Next(-2, Game.FieldWidth + 2 + 1);
                starY[i] = random.Next(-2, Game.FieldHeight + 2 + 1);
            }
        }

        public override void Update(GameCommand command)
        {
            base.Update(command);
            for (int i = 0; i < NUM_STARS / 4; i++)
            {
                starY[i] += 3;
                if (starY[i] > Game.FieldHeight + 2)
                {
                    starX[i] = Random.Next(-2, Game.FieldWidth + 2 + 1);
                    starY[i] = -2;
                }
            }
            for (int i = NUM_STARS / 4; i < NUM_STARS / 2; i++)
            {
                starY[i] += 2;
                if (starY[i] > Game.FieldHeight + 2)
                {
                    starX[i] = Random.Next(-2, Game.FieldWidth + 2 + 1);
                    starY[i] = -2;
                }
            }
            for (int i = NUM_STARS / 2; i < NUM_STARS; i++)
            {
                starY[i] += 1;
                if (starY[i] > Game.FieldHeight + 2)
                {
                    starX[i] = Random.Next(-2, Game.FieldWidth + 2 + 1);
                    starY[i] = -2;
                }
            }
        }

        public override void DrawBackground(IGraphics graphics)
        {
            graphics.SetColor(255, 0, 0, 0);
            graphics.DrawRectangle(0, 0, Game.FieldWidth, Game.FieldHeight);
            graphics.EnableAddBlend();
            graphics.SetColor(255, 255, 255, 255);
            /*
            for (int i = 0; i < NUM_STARS / 4; i++)
            {
                graphics.DrawBackground(Image.Star, starX[i] - 4, starY[i] - 4, 8, 8, 0, 0, BackgroundStretch);
            }
            */
            for (int i = NUM_STARS / 4; i < NUM_STARS / 2; i++)
            {
                graphics.DrawBackground(Image.Star, starX[i] - 4, starY[i] - 4, 8, 8, 0, 1, BackgroundStretch);
            }
            graphics.SetColor(255, 128, 128, 128);
            for (int i = NUM_STARS / 2; i < NUM_STARS; i++)
            {
                graphics.DrawBackground(Image.Star, starX[i] - 4, starY[i] - 4, 8, 8, 0, 1, BackgroundStretch);
            }
            graphics.DisableAddBlend();
        }
    }
}
