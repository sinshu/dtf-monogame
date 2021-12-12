using System;

namespace MiswGame2008
{
    public class Level23 : StarBackgroundGame
    {
        public Level23(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
        }

        public override void InitEnemies()
        {
            AddEnemy(new FirstBoss2(this, Game.FieldWidth + 64, -32));
        }

        public override int Level
        {
            get
            {
                return 23;
            }
        }
    }
}
