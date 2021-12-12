using System;

namespace MiswGame2008
{
    public class EndingMessage6 : EndingMessage
    {
        public EndingMessage6(Game game)
            : base(game, Game.FieldWidth + 128, Game.FieldHeight / 4, new string[] { "", "THANK YOU FOR PLAYING", ""})
        {
        }

        public override void OnRemove()
        {
            ((Level25)Game).EndGame();
            Game.AddEnemyInGame(new Enemy(Game, 240, 1000));
        }
    }
}
