using System;

namespace MiswGame2008
{
    public class MissileEnemySpawner : Effect
    {
        private int angle;
        private int animation;

        public MissileEnemySpawner(Game game, double x, double y)
            : base(game, x, y)
        {
            this.angle = game.Random.Next(0, 360);
            animation = 0;
        }

        public override void Update()
        {
            if (animation < 16)
            {
                animation++;
            }
            if (animation == 16)
            {
                Game.AddEnemyInGame(new MissileEnemy(Game, X, Y));
            }
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            int a = 15 - animation;
            graphics.SetColor(255, 128, 128, 128);
            graphics.DrawObject(Image.BigExplosion, drawX, drawY, 64, 64, a / 4, a % 4, angle);
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
