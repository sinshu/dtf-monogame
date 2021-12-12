using System;

namespace MiswGame2008
{
    public class Level6 : GreenBackgroundGame
    {
        public Level6(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new FirstBoss(this, -64, -32));
        }

        public override int Level
        {
            get
            {
                return 6;
            }
        }

        public override bool IsBossStage
        {
            get
            {
                return true;
            }
        }
    }
}
