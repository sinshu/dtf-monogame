using System;

namespace MiswGame2008
{
    public class GrayBackgroundGame : Game
    {
        private int scroll;

        public GrayBackgroundGame(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
            scroll = random.Next(1024);
        }

        public override void Update(GameCommand command)
        {
            base.Update(command);
            scroll += 3;
        }

        public override void DrawBackground(IGraphics graphics)
        {
            graphics.SetColor(255, 255, 255, 255);
            for (int i = -1; i < 16; i++)
            {
                graphics.DrawBackground(Image.Background3, 0, scroll % 32 + i * 32, 512, 32, 0, 0, BackgroundStretch);
            }
        }
    }
}
