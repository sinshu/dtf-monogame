using System;

namespace MiswGame2008
{
    public class Level20 : StarBackgroundGame
    {
        public Level20(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            for (int i = 80; i <= 400; i += 64)
            {
                AddEnemy(new Kurage(this, i, 48, 0));
            }
            for (int i = 112; i <= 368; i += 64)
            {
                AddEnemy(new Kurage(this, i, 96, 1));
            }
            for (int i = 144; i <= 336; i += 64)
            {
                AddEnemy(new Kurage(this, i, 144, 2));
            }
        }

        public override int Level
        {
            get
            {
                return 20;
            }
        }
    }
}
