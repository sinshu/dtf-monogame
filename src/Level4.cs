using System;

namespace MiswGame2008
{
    public class Level4 : GreenBackgroundGame
    {
        public Level4(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new RedEnemy(this, 224, 32));
            AddEnemy(new RedEnemy(this, 256, 32));
            AddEnemy(new BlueEnemy(this, 224, 64));
            AddEnemy(new BlueEnemy(this, 256, 64));
            AddEnemy(new GreenEnemy(this, 224, 96));
            AddEnemy(new GreenEnemy(this, 256, 96));
            AddEnemy(new GreenEnemy(this, 224, 128));
            AddEnemy(new GreenEnemy(this, 256, 128));
            AddEnemy(new GreenEnemy(this, 224, 160));
            AddEnemy(new GreenEnemy(this, 256, 160));
            AddEnemy(new GreenEnemy(this, 224, 192));
            AddEnemy(new GreenEnemy(this, 256, 192));
        }

        public override int Level
        {
            get
            {
                return 4;
            }
        }
    }
}
