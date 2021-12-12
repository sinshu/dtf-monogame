using System;

namespace MiswGame2008
{
    public class Smoke : Effect
    {
        private int angle;
        private int animation;

        public Smoke(Game game, double x, double y, int angle)
            : base(game, x, y)
        {
            this.angle = angle;
            animation = 0;
        }

        public override void Update()
        {
            if (animation < 32)
            {
                animation++;
            }
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.Smoke, drawX, drawY, 32, 32, animation / 4, animation % 4, angle);
        }

        public override bool IsRemoved
        {
            get
            {
                return animation == 32;
            }
        }
    }
}
