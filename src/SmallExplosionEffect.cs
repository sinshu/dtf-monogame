using System;

namespace MiswGame2008
{
    public class SmallExplosionEffect : Effect
    {
        private int angle;
        private int animation;

        public SmallExplosionEffect(Game game, double x, double y, int angle)
            : base(game, x, y)
        {
            this.angle = angle;
            animation = game.Random.Next(0, 2);
        }

        public override void Update()
        {
            animation += 2;
            if (animation > 16)
            {
                animation = 16;
            }
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.SmallExplosion, drawX, drawY, 32, 32, animation / 4, animation % 4, angle);
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
