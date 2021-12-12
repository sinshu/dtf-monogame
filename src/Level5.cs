using System;

namespace MiswGame2008
{
    public class Level5 : GreenBackgroundGame
    {
        public Level5(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new RedEnemy(this, 240, 32));
            AddEnemy(new BlueEnemy(this, 224, 64));
            AddEnemy(new BlueEnemy(this, 256, 64));
            AddEnemy(new RedEnemy(this, 80, 64));
            AddEnemy(new BlueEnemy(this, 64, 96));
            AddEnemy(new BlueEnemy(this, 96, 96));
            AddEnemy(new RedEnemy(this, 400, 64));
            AddEnemy(new BlueEnemy(this, 384, 96));
            AddEnemy(new BlueEnemy(this, 416, 96));
        }

        public override int Level
        {
            get
            {
                return 5;
            }
        }
    }
}
