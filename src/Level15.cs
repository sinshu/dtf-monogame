using System;

namespace MiswGame2008
{
    public class Level15 : GrayBackgroundGame
    {
        public Level15(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new Teki(this, 144, 48, 0));
            AddEnemy(new Teki(this, 208, 48, 0));
            AddEnemy(new Teki(this, 272, 48, 0));
            AddEnemy(new Teki(this, 336, 48, 0));
            AddEnemy(new Teki(this, 144, 96, 2));
            AddEnemy(new Teki(this, 208, 96, 2));
            AddEnemy(new Teki(this, 272, 96, 2));
            AddEnemy(new Teki(this, 336, 96, 2));
            AddEnemy(new Teki(this, 144, 144, 1));
            AddEnemy(new Teki(this, 208, 144, 1));
            AddEnemy(new Teki(this, 272, 144, 1));
            AddEnemy(new Teki(this, 336, 144, 1));
        }

        public override int Level
        {
            get
            {
                return 15;
            }
        }
    }
}
