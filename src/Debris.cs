using System;

namespace MiswGame2008
{
    public class Debris : Effect
    {
        private int realAngle;
        private int drawAngle;
        private double speed;
        private int r, g, b;
        private int ticks;
        private int type;
        private int animation;

        public Debris(Game game, double x, double y, int r, int g, int b, double speed, int angle)
            : base(game, x, y)
        {
            this.speed = speed;
            this.r = r;
            this.g = g;
            this.b = b;
            ticks = 0;
            realAngle = angle;
            drawAngle = game.Random.Next(360);
            type = game.Random.Next(2);
            animation = game.Random.Next(4);
        }

        public override void Update()
        {
            X += speed * Utility.Cos(realAngle);
            Y -= speed * Utility.Sin(realAngle);
            if (ticks < 24)
            {
                ticks++;
            }
            animation++;
        }

        public override void Draw(IGraphics graphics)
        {
            if (animation >= 16 && animation % 2 == 0)
            {
                return;
            }
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, r, g, b);
            graphics.DrawObject(Image.Debris, drawX, drawY, 8, 8, type, animation % 4, drawAngle);
        }

        public override bool IsRemoved
        {
            get
            {
                return ticks == 24;
            }
        }
    }
}
