using System;

namespace MiswGame2008
{
    public class Level25 : StarBackgroundGame
    {
        private bool endGame;
        private int endGameCount;
        private bool endingGameOver;

        public Level25(Random random, int highScore, int score, int left)
            : base(random, highScore, score, left)
        {
            endGame = false;
            endGameCount = 0;
            endingGameOver = false;
        }

        public override void InitEnemies()
        {
            AddEnemy(new EndingMessage1(this));
        }

        public void EndGame()
        {
            endGame = true;
        }

        public override void Update(GameCommand command)
        {
            base.Update(command);
            if (endGame)
            {
                if (endGameCount <= 60)
                {
                    endGameCount++;
                }
                else
                {
                    endingGameOver = true;
                }
            }
        }

        public override int Level
        {
            get
            {
                return 25;
            }
        }

        public override bool GameOver
        {
            get
            {
                return false;
            }
        }

        public override bool GoingToNextStage
        {
            get
            {
                return false;
            }
        }

        public override bool EndingGameOver
        {
            get
            {
                return endingGameOver;
            }
        }
    }
}
