using System;

namespace MiswGame2008
{
    public class Level24 : StarBackgroundGame
    {
        public Level24(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new LastBoss(this, 240, -32));
        }

        public override int Level
        {
            get
            {
                return 24;
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
