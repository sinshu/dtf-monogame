using System;

namespace MiswGame2008
{
    public class Level1 : GreenBackgroundGame
    {
        public Level1(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new RedEnemy(this, 240, 48));
            AddEnemy(new BlueEnemy(this, 208, 96));
            AddEnemy(new BlueEnemy(this, 272, 96));
            AddEnemy(new GreenEnemy(this, 176, 144));
            AddEnemy(new GreenEnemy(this, 240, 144));
            AddEnemy(new GreenEnemy(this, 304, 144));
        }

        public override int Level
        {
            get
            {
                return 1;
            }
        }
    }
}
