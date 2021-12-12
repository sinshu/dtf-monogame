using System;

namespace MiswGame2008
{
    public class Borg : Enemy
    {
        private const double MAX_SPEED = 8;

        private int angle;
        private double vx, vy;
        private int waitCount;
        private double waitX, waitY;
        private double swingWidth, swingHeight;

        private int hitPoints;
        private bool damaged;

        public Borg(Game game, double x, double y)
            : base(game, x, y)
        {
            angle = 90;
            vx = vy = 0;
            waitCount = game.Random.Next(15, 30);
            waitX = x;
            waitY = y;
            swingWidth = (x - Game.FieldWidth / 2) / 8;
            swingHeight = (y - 48) / 16;
            hitPoints = 16;
            damaged = false;
        }

        public override void Update()
        {
            if (waitCount > 0)
            {
                double newX = waitX + swingWidth * Utility.Sin(Game.Ticks * 8);
                double newY = waitY + swingHeight * Utility.Sin(Game.Ticks * 8);
                vx = newX - X;
                vy = newY - Y;
                X = newX;
                Y = newY;
                waitCount--;
            }
            if (waitCount == 0)
            {
                int deg = Utility.NormalizeDeg(Utility.Atan2(Y - Game.Player.Y, Game.Player.X - X) - angle);
                if (deg == -180)
                {
                    angle += Game.Random.Next(2) == 0 ? 1 : -1;
                }
                else if (Math.Abs(deg) < 8)
                {
                    angle = Utility.Atan2(Y - Game.Player.Y, Game.Player.X - X);
                }
                else
                {
                    if (deg < 0)
                    {
                        angle -= 8;
                    }
                    else if (deg > 0)
                    {
                        angle += 8;
                    }
                }
                vx += 4 * Utility.Cos(angle);
                vy -= 4 * Utility.Sin(angle);
                Move();
            }
            damaged = false;
        }

        public override bool Hit()
        {
            if (hitPoints > 0)
            {
                hitPoints--;
                damaged = true;
                if (hitPoints > 0)
                {
                    Game.PlaySound(Sound.EnemyDamage);
                }
            }
            if (hitPoints == 0)
            {
                int startAngle = Game.Random.Next(90);
                for (int i = 0; i < 12; i++)
                {
                    int angle = startAngle + i * 45 + Game.Random.Next(-30, 31);
                    double x = X + 24 * Utility.Cos(angle);
                    double y = Y - 24 * Utility.Sin(angle);
                    double speed = 0.5 + 1.5 * Game.Random.NextDouble();
                    Effect debris = new Debris(Game, x, y, 255, 255, 255, speed, angle);
                    Game.AddEffect(debris);
                }
                Effect effect = new BigExplosionEffect(Game, X, Y, Game.Random.Next(360), 255, 255, 255);
                Game.AddEffect(effect);
                Game.PlaySound(Sound.Explosion);
                hitPoints--;
            }
            return true;
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.Borg, drawX, drawY, 64, 64, 0, 0, angle);
            if (damaged)
            {
                graphics.EnableAddBlend();
                graphics.DrawObject(Image.Borg, drawX, drawY, 64, 64, 0, 0, angle);
                graphics.DisableAddBlend();
            }
        }

        private void Move()
        {
            if (vx * vx + vy * vy > MAX_SPEED * MAX_SPEED)
            {
                double rad = Math.Atan2(vy, vx);
                vx = MAX_SPEED * Math.Cos(rad);
                vy = MAX_SPEED * Math.Sin(rad);
            }
            X += vx;
            Y += vy;
            if (X < -16)
            {
                X = Game.FieldWidth + 16;
            }
            else if (X > Game.FieldWidth + 16)
            {
                X = -16;
            }
            if (Y > Game.FieldHeight + 16)
            {
                Y = -16;
            }
        }

        public override int HalfWidth
        {
            get
            {
                return 24;
            }
        }

        public override int HalfHeight
        {
            get
            {
                return 24;
            }
        }

        public override bool IsRemoved
        {
            get
            {
                return hitPoints <= 0;
            }
        }
    }
}
