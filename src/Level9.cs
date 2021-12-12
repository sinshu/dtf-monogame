using System;

namespace MiswGame2008
{
    public class Level9 : SkyBackgroundGame
    {
        public Level9(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new MissileEnemy(this, 176, 48));
            AddEnemy(new MissileEnemy(this, 240, 48));
            AddEnemy(new MissileEnemy(this, 304, 48));
            AddEnemy(new BlueEnemy(this, 176, 128));
            AddEnemy(new BlueEnemy(this, 208, 160));
            AddEnemy(new BlueEnemy(this, 240, 192));
            AddEnemy(new BlueEnemy(this, 272, 160));
            AddEnemy(new BlueEnemy(this, 304, 128));
        }

        public override int Level
        {
            get
            {
                return 9;
            }
        }
    }
}
