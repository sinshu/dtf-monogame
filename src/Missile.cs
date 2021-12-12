using System;

namespace MiswGame2008
{
    public class Missile : Bullet
    {
        private static double MAX_SPEED = 12;
        private static int LIFE_TICKS = 60;

        private bool isDead;
        private double vx, vy;
        private int ticks;

        public Missile(Game game, double x, double y, int angle)
            : base(game, x, y, 0, angle)
        {
            isDead = false;
            vx = vy = 0;
            ticks = 0;
        }

        public override bool IsRemoved
        {
            get
            {
                return !(-16 < X && X < Game.FieldWidth + 16 && -16 < Y && Y < Game.FieldHeight + 16) || isDead;
            }
        }

        public override void Hit()
        {
            isDead = true;
            Game.AddEffect(new SmallExplosionEffect(Game, X, Y, Game.Random.Next(360)));
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawBullet(Image.Bullet, drawX, drawY, 16, 16, 2, 0, Angle);
        }

        public override void Update()
        {
            double x = X - 16 * Utility.Cos(Angle);
            double y = Y + 16 * Utility.Sin(Angle);
            Effect smoke = new Smoke(Game, x, y, Game.Random.Next(360));
            Game.AddEffect(smoke);

            if (ticks < LIFE_TICKS)
            {
                int deg = Utility.NormalizeDeg(Utility.Atan2(Y - Game.Player.Y, Game.Player.X - X) - Angle);
                if (deg == -180)
                {
                    Angle += Game.Random.Next(2) == 0 ? 1 : -1;
                }
                else if (Math.Abs(deg) < 6)
                {
                    Angle = Utility.Atan2(Y - Game.Player.Y, Game.Player.X - X);
                }
                else
                {
                    if (deg < 0)
                    {
                        Angle -= 6;
                    }
                    else if (deg > 0)
                    {
                        Angle += 6;
                    }
                }
            }

            vx += Utility.Cos(Angle);
            vy -= Utility.Sin(Angle);

            if (vx * vx + vy * vy > MAX_SPEED * MAX_SPEED)
            {
                double rad = Math.Atan2(vy, vx);
                vx = MAX_SPEED * Math.Cos(rad);
                vy = MAX_SPEED * Math.Sin(rad);
            }

            X += vx;
            Y += vy;

            ticks++;
        }
    }
}
