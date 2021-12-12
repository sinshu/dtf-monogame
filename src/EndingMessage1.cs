using System;

namespace MiswGame2008
{
    public class EndingMessage1 : EndingMessage
    {
        public EndingMessage1(Game game)
            : base(game, -128, Game.FieldHeight / 4, new string[] { "PROGRAM", "", "SINSHU" })
        {
        }

        public override void OnRemove()
        {
            Game.AddEnemyInGame(new EndingMessage2(Game));
        }
    }
}
