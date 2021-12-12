using System;

namespace MiswGame2008
{
    public class Level8 : SkyBackgroundGame
    {
        public Level8(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new OrangeEnemy(this, 96, 48));
            AddEnemy(new OrangeEnemy(this, 192, 48));
            AddEnemy(new OrangeEnemy(this, 288, 48));
            AddEnemy(new OrangeEnemy(this, 384, 48));
            AddEnemy(new GreenEnemy(this, 48, 96));
            AddEnemy(new GreenEnemy(this, 112, 96));
            AddEnemy(new BlueEnemy(this, 144, 144));
            AddEnemy(new BlueEnemy(this, 208, 144));
            AddEnemy(new BlueEnemy(this, 272, 144));
            AddEnemy(new BlueEnemy(this, 336, 144));
            AddEnemy(new GreenEnemy(this, 368, 96));
            AddEnemy(new GreenEnemy(this, 432, 96));
            AddEnemy(new RedEnemy(this, 240, 96));
        }

        public override int Level
        {
            get
            {
                return 8;
            }
        }
    }
}
