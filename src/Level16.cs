using System;

namespace MiswGame2008
{
    public class Level16 : GrayBackgroundGame
    {
        public Level16(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new Opera(this, 240, 32));
            AddEnemy(new Firefox(this, 224, 64));
            AddEnemy(new Firefox(this, 256, 64));
            for (int i = 80; i <= 400; i += 64)
            {
                AddEnemy(new Ie(this, i, 96));
            }
            for (int i = 48; i <= 432; i += 64)
            {
                AddEnemy(new Ie(this, i, 128));
            }
            for (int i = 80; i <= 400; i += 64)
            {
                AddEnemy(new Ie(this, i, 160));
            }
            for (int i = 48; i <= 432; i += 64)
            {
                AddEnemy(new Ie(this, i, 192));
            }
        }

        public override int Level
        {
            get
            {
                return 16;
            }
        }
    }
}
