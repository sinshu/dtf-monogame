using System;

namespace MiswGame2008
{
    public class Level10 : SkyBackgroundGame
    {
        public Level10(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new RedEnemy(this, 224, 32));
            AddEnemy(new RedEnemy(this, 256, 32));
            AddEnemy(new RedEnemy(this, 224, 64));
            AddEnemy(new RedEnemy(this, 256, 64));
            AddEnemy(new RedEnemy(this, 224, 96));
            AddEnemy(new RedEnemy(this, 256, 96));
            AddEnemy(new BlueEnemy(this, 144, 64));
            AddEnemy(new BlueEnemy(this, 336, 64));
            AddEnemy(new MissileEnemy(this, 64, 48));
            AddEnemy(new MissileEnemy(this, 416, 48));
            AddEnemy(new MissileEnemy(this, 64, 80));
            AddEnemy(new MissileEnemy(this, 416, 80));
        }

        public override int Level
        {
            get
            {
                return 10;
            }
        }
    }
}
