using System;

namespace MiswGame2008
{
    public class Level2 : GreenBackgroundGame
    {
        public Level2(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new RedEnemy(this, 240, 48));
            AddEnemy(new BlueEnemy(this, 192, 48));
            AddEnemy(new BlueEnemy(this, 288, 48));
            AddEnemy(new BlueEnemy(this, 240, 96));
            AddEnemy(new GreenEnemy(this, 144, 48));
            AddEnemy(new GreenEnemy(this, 192, 96));
            AddEnemy(new GreenEnemy(this, 240, 144));
            AddEnemy(new GreenEnemy(this, 288, 96));
            AddEnemy(new GreenEnemy(this, 336, 48));
        }

        public override int Level
        {
            get
            {
                return 2;
            }
        }
    }
}
