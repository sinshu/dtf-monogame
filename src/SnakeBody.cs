using System;

namespace MiswGame2008
{
    public class SnakeBody : Enemy
    {
        private bool tail;
        private int angle;
        private bool isDead;

        public SnakeBody(Game game, double x, double y, bool tail)
            : base(game, x, y)
        {
            this.tail = tail;
            angle = 0;
            isDead = false;
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.SnakeBody, drawX, drawY, 64, 32, tail ? 1 : 0, 0, angle);
        }

        public int Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
            }
        }

        public override bool IsRemoved
        {
            get
            {
                return isDead;
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

        public void Destroy()
        {
            isDead = true;
            for (int i = 0; i < 2; i++)
            {
                int a = Utility.Atan2(Y - Game.Player.Y, Game.Player.X - X) + Game.Random.Next(-60, 60 + 1);
                Bullet bullet = new SnakeBullet(Game, X, Y, Game.Random.Next(8, 12 + 1), a);
                Game.AddEnemyBullet(bullet);
            }
            int startAngle = Game.Random.Next(90);
            for (int i = 0; i < 8; i++)
            {
                int angle = startAngle + i * 45 + Game.Random.Next(-45, 46);
                double x = X + 16 * Utility.Cos(angle);
                double y = Y - 16 * Utility.Sin(angle);
                double speed = 0.5 + 1.5 * Game.Random.NextDouble();
                Effect debris = new Debris(Game, x, y, 128, 128, 128, speed, angle);
                Game.AddEffect(debris);
            }
            Effect effect = new BigExplosionEffect(Game, X, Y, Game.Random.Next(360), 255, 255, 255);
            Game.AddEffect(effect);
            Game.PlaySound(Sound.Explosion);
        }
    }
}
