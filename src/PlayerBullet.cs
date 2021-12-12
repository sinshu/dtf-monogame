using System;

namespace MiswGame2008
{
    public class PlayerBullet : Bullet
    {
        private bool isDead;

        public PlayerBullet(Game game, double x, double y, double speed, int angle)
            : base(game, x, y, speed, angle)
        {
            isDead = false;
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
            graphics.DrawBullet(Image.Bullet, drawX, drawY, 16, 16, 0, 0, Angle);
        }
    }
}
