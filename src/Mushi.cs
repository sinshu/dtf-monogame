using System;

namespace MiswGame2008
{
    public class Mushi : Enemy
    {
        private const double MAX_SPEED = 8;

        private bool isDead;
        private int angle;
        private double vx, vy;
        private int waitCount;
        private double waitX, waitY;
        private double swingWidth, swingHeight;
        private int type;
        private int animation;

        public Mushi(Game game, double x, double y, int type)
            : base(game, x, y)
        {
            isDead = false;
            angle = 90;
            vx = vy = 0;
            waitCount = game.Random.Next(15, 120);
            waitX = x;
            waitY = y;
            swingWidth = (x - Game.FieldWidth / 2) / 8;
            swingHeight = (y - 48) / 16;
            this.type = type;
            animation = 0;
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
                vx += Utility.Cos(angle);
                vy -= Utility.Sin(angle);
                Move();
                animation++;
            }
        }

        public override bool Hit()
        {
            isDead = true;
            Effect effect = new BigExplosionEffect(Game, X, Y, Game.Random.Next(360), 255, 255, 255);
            Game.AddEffect(effect);
            Game.PlaySound(Sound.Explosion);
            return true;
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.Mushi, drawX, drawY, 32, 64, type, animation % 3 == 1 ? 1 : 0, angle);
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
                return 12;
            }
        }

        public override int HalfHeight
        {
            get
            {
                return 12;
            }
        }

        public override bool IsRemoved
        {
            get
            {
                return isDead;
            }
        }
    }
}
