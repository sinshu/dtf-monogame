using System;

namespace MiswGame2008
{
    public class SkyBackgroundGame : Game
    {
        private int scroll;

        public SkyBackgroundGame(Random random, int highScore, int score, int left)
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
            if (scroll / 512 % 2 == 0)
            {
                graphics.DrawBackground(Image.Background2, 0, scroll % 512 - 512, 512, 512, 0, 0, BackgroundStretch);
                graphics.DrawBackground(Image.Background2, 0, scroll % 512, 512, 512, 1, 0, BackgroundStretch);
            }
            else
            {
                graphics.DrawBackground(Image.Background2, 0, scroll % 512 - 512, 512, 512, 1, 0, BackgroundStretch);
                graphics.DrawBackground(Image.Background2, 0, scroll % 512, 512, 512, 0, 0, BackgroundStretch);
            }
        }
    }
}
