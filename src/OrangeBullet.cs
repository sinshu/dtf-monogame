using System;

namespace MiswGame2008
{
    public class OrangeBullet : EnemyBullet
    {
        public OrangeBullet(Game game, double x, double y, double speed, int angle)
            : base(game, x, y, speed, angle)
        {
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawBullet(Image.Bullet, drawX, drawY, 16, 16, 3, 0, Angle);
        }
    }
}
