using System;

namespace MiswGame2008
{
    public class Level17 : GrayBackgroundGame
    {
        public Level17(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            for (int i = 0; i < 48; i++)
            {
                AddEnemy(new GreenEnemy(this, 240, 64));
            }
        }

        public override int Level
        {
            get
            {
                return 17;
            }
        }
    }
}
