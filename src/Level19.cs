using System;

namespace MiswGame2008
{
    public class Level19 : StarBackgroundGame
    {
        public Level19(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            for (int i = 96; i <= 384; i += 96)
            {
                AddEnemy(new FastEnemy(this, i, 48));
            }
            for (int i = 96; i <= 384; i += 96)
            {
                AddEnemy(new RedEnemy(this, i - 16, 80));
                AddEnemy(new RedEnemy(this, i + 16, 80));
            }
        }

        public override int Level
        {
            get
            {
                return 19;
            }
        }
    }
}
