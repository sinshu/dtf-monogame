using System;

namespace MiswGame2008
{
    public class EndingMessage5 : EndingMessage
    {
        public EndingMessage5(Game game)
            : base(game, -128, Game.FieldHeight / 4, new string[] { "SPECIAL THANKS", "", "NRY" })
        {
        }

        public override void OnRemove()
        {
            Game.AddEnemyInGame(new EndingMessage6(Game));
        }
    }
}
