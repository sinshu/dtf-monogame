using System;

namespace MiswGame2008
{
    public class Level12 : SkyBackgroundGame
    {
        public Level12(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new Snake(this, Game.FieldWidth + 32, -32));
        }

        public override int Level
        {
            get
            {
                return 12;
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
