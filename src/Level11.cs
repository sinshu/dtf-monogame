using System;

namespace MiswGame2008
{
    public class Level11 : SkyBackgroundGame
    {
        public Level11(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new MissileEnemy(this, 112, 48));
            AddEnemy(new OrangeEnemy(this, 176, 48));
            AddEnemy(new MissileEnemy(this, 240, 48));
            AddEnemy(new OrangeEnemy(this, 304, 48));
            AddEnemy(new MissileEnemy(this, 368, 48));
            AddEnemy(new BlueEnemy(this, 144, 96));
            AddEnemy(new BlueEnemy(this, 208, 96));
            AddEnemy(new BlueEnemy(this, 272, 96));
            AddEnemy(new BlueEnemy(this, 336, 96));
            AddEnemy(new RedEnemy(this, 80, 96));
            // AddEnemy(new GreenEnemy(this, 96, 128));
            AddEnemy(new GreenEnemy(this, 112, 144));
            // AddEnemy(new GreenEnemy(this, 144, 160));
            AddEnemy(new GreenEnemy(this, 176, 144));
            // AddEnemy(new GreenEnemy(this, 208, 160));
            AddEnemy(new GreenEnemy(this, 240, 144));
            // AddEnemy(new GreenEnemy(this, 272, 160));
            AddEnemy(new GreenEnemy(this, 304, 144));
            // AddEnemy(new GreenEnemy(this, 336, 160));
            AddEnemy(new GreenEnemy(this, 368, 144));
            // AddEnemy(new GreenEnemy(this, 384, 128));
            AddEnemy(new RedEnemy(this, 400, 96));
        }

        public override int Level
        {
            get
            {
                return 11;
            }
        }
    }
}
