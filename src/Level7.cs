using System;

namespace MiswGame2008
{
    public class Level7 : SkyBackgroundGame
    {
        public Level7(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new OrangeEnemy(this, 208, 48));
            AddEnemy(new OrangeEnemy(this, 272, 48));
            AddEnemy(new RedEnemy(this, 224, 96));
            AddEnemy(new RedEnemy(this, 256, 96));
            AddEnemy(new BlueEnemy(this, 192, 96));
            AddEnemy(new BlueEnemy(this, 224, 128));
            AddEnemy(new BlueEnemy(this, 256, 128));
            AddEnemy(new BlueEnemy(this, 288, 96));
            AddEnemy(new GreenEnemy(this, 192, 128));
            AddEnemy(new GreenEnemy(this, 224, 160));
            AddEnemy(new GreenEnemy(this, 256, 160));
            AddEnemy(new GreenEnemy(this, 288, 128));
        }

        public override int Level
        {
            get
            {
                return 7;
            }
        }
    }
}
