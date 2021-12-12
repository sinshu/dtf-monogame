using System;

namespace MiswGame2008
{
    public class Level21 : StarBackgroundGame
    {
        public Level21(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new Kurage(this, 80, 48, 0));
            AddEnemy(new Kurage(this, 144, 48, 1));
            AddEnemy(new Kurage(this, 208, 48, 2));
            AddEnemy(new Kurage(this, 272, 48, 2));
            AddEnemy(new Kurage(this, 336, 48, 1));
            AddEnemy(new Kurage(this, 400, 48, 0));
            AddEnemy(new Kurage(this, 80, 144, 0));
            AddEnemy(new Kurage(this, 144, 144, 1));
            AddEnemy(new Kurage(this, 208, 144, 2));
            AddEnemy(new Kurage(this, 272, 144, 2));
            AddEnemy(new Kurage(this, 336, 144, 1));
            AddEnemy(new Kurage(this, 400, 144, 0));
            for (int i = 112; i <= 368; i += 64)
            {
                AddEnemy(new FastEnemy(this, i, 96));
            }
        }

        public override int Level
        {
            get
            {
                return 21;
            }
        }
    }
}
