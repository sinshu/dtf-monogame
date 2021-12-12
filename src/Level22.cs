using System;

namespace MiswGame2008
{
    public class Level22 : StarBackgroundGame
    {
        public Level22(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new FastEnemy(this, 240, 32));
            AddEnemy(new FastEnemy(this, 208, 64));
            AddEnemy(new FastEnemy(this, 272, 64));
            AddEnemy(new FastEnemy(this, 176, 96));
            AddEnemy(new FastEnemy(this, 304, 96));
            AddEnemy(new Kurage(this, 144, 128, 0));
            AddEnemy(new Kurage(this, 336, 128, 0));
            AddEnemy(new OrangeEnemy(this, 176, 160));
            AddEnemy(new OrangeEnemy(this, 304, 160));
            AddEnemy(new OrangeEnemy(this, 208, 192));
            AddEnemy(new OrangeEnemy(this, 272, 192));
            AddEnemy(new OrangeEnemy(this, 240, 224));
        }

        public override int Level
        {
            get
            {
                return 22;
            }
        }
    }
}
