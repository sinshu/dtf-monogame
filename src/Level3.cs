using System;

namespace MiswGame2008
{
    public class Level3 : GreenBackgroundGame
    {
        public Level3(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new BlueEnemy(this, 48, 48));
            AddEnemy(new BlueEnemy(this, 432, 48));
            AddEnemy(new GreenEnemy(this, 48, 96));
            AddEnemy(new GreenEnemy(this, 112, 96));
            AddEnemy(new GreenEnemy(this, 176, 96));
            AddEnemy(new GreenEnemy(this, 240, 96));
            AddEnemy(new GreenEnemy(this, 304, 96));
            AddEnemy(new GreenEnemy(this, 368, 96));
            AddEnemy(new GreenEnemy(this, 432, 96));
        }

        public override int Level
        {
            get
            {
                return 3;
            }
        }
    }
}
