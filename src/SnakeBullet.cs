using System;

namespace MiswGame2008
{
    public class SnakeBullet : Bullet
    {
        private bool isDead;
        private int spin;
        private int dirr;

        public SnakeBullet(Game game, double x, double y, double speed, int angle)
            : base(game, x, y, speed, angle)
        {
            isDead = false;
            spin = game.Random.Next(4, 17);
            if (game.Random.Next(0, 2) == 0)
            {
                spin = -spin;
            }
            dirr = game.Random.Next(0, 360);
        }

        public override void Update()
        {
            base.Update();
            dirr += spin;
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
            graphics.DrawObject(Image.Bullet, drawX, drawY, 16, 16, 4, 0, dirr);
        }
    }
}
