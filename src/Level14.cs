using System;

namespace MiswGame2008
{
    public class Level14 : GrayBackgroundGame
    {
        public Level14(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new Borg(this, 240, 64));
            AddEnemy(new Mushi(this, 80, 128, 0));
            AddEnemy(new Mushi(this, 144, 128, 0));
            AddEnemy(new Mushi(this, 208, 128, 0));
            AddEnemy(new Mushi(this, 272, 128, 0));
            AddEnemy(new Mushi(this, 336, 128, 0));
            AddEnemy(new Mushi(this, 400, 128, 0));
            AddEnemy(new Mushi(this, 112, 160, 0));
            AddEnemy(new Mushi(this, 176, 160, 0));
            AddEnemy(new Mushi(this, 240, 160, 0));
            AddEnemy(new Mushi(this, 304, 160, 0));
            AddEnemy(new Mushi(this, 368, 160, 0));
            AddEnemy(new Mushi(this, 144, 192, 0));
            AddEnemy(new Mushi(this, 208, 192, 0));
            AddEnemy(new Mushi(this, 272, 192, 0));
            AddEnemy(new Mushi(this, 336, 192, 0));
            AddEnemy(new RedEnemy(this, 48, 64));
            AddEnemy(new RedEnemy(this, 80, 64));
            AddEnemy(new RedEnemy(this, 112, 64));
            AddEnemy(new RedEnemy(this, 368, 64));
            AddEnemy(new RedEnemy(this, 400, 64));
            AddEnemy(new RedEnemy(this, 432, 64));
        }

        public override int Level
        {
            get
            {
                return 14;
            }
        }
    }
}
