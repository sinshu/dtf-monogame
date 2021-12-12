using System;

namespace MiswGame2008
{
    public class Funnel : Enemy
    {
        public Funnel(Game game, double x, double y)
            : base(game, x, y)
        {
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.Clipper, drawX, drawY, 32, 32, 1, 0, 0);
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
    }
}
