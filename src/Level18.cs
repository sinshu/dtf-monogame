using System;

namespace MiswGame2008
{
    public class Level18 : GrayBackgroundGame
    {
        public Level18(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new Clipper(this, -128, Game.FieldHeight / 4));
        }

        public override int Level
        {
            get
            {
                return 18;
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
