using System;

namespace MiswGame2008
{
    public class Level13 : GrayBackgroundGame
    {
        public Level13(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new Mushi(this, 240, 32, 2));
            AddEnemy(new Mushi(this, 224, 64, 1));
            AddEnemy(new Mushi(this, 256, 64, 1));
            AddEnemy(new Mushi(this, 208, 96, 0));
            AddEnemy(new Mushi(this, 240, 96, 0));
            AddEnemy(new Mushi(this, 272, 96, 0));
            AddEnemy(new MissileEnemy(this, 112, 160));
            AddEnemy(new MissileEnemy(this, 176, 160));
            AddEnemy(new MissileEnemy(this, 240, 160));
            AddEnemy(new MissileEnemy(this, 304, 160));
            AddEnemy(new MissileEnemy(this, 368, 160));
            AddEnemy(new Mushi(this, 80, 64, 1));
            AddEnemy(new Mushi(this, 400, 64, 1));
            AddEnemy(new Mushi(this, 144, 224, 0));
            AddEnemy(new Mushi(this, 208, 224, 0));
            AddEnemy(new Mushi(this, 272, 224, 0));
            AddEnemy(new Mushi(this, 336, 224, 0));
        }

        public override int Level
        {
            get
            {
                return 13;
            }
        }
    }
}
