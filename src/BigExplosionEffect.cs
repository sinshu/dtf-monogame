using System;

namespace MiswGame2008
{
    public class BigExplosionEffect : Effect
    {
        private int angle;
        private int r, g, b;
        private int animation;

        public BigExplosionEffect(Game game, double x, double y, int angle, int r, int g, int b)
            : base(game, x, y)
        {
            this.angle = angle;
            this.r = r;
            this.g = g;
            this.b = b;
            animation = 0;
        }

        public override void Update()
        {
            if (animation < 16)
            {
                animation++;
            }
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, r, g, b);
            graphics.DrawObject(Image.BigExplosion, drawX, drawY, 64, 64, animation / 4, animation % 4, angle);
        }

        public override bool IsRemoved
        {
            get
            {
                return animation == 16;
            }
        }
    }
}
