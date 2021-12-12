using System;

namespace MiswGame2008
{
    public class EndingMessage3 : EndingMessage
    {
        public EndingMessage3(Game game)
            : base(game, -128, Game.FieldHeight / 4, new string[] { "SOUND EFFECTS", "", "HA810" })
        {
        }

        public override void OnRemove()
        {
            Game.AddEnemyInGame(new EndingMessage4(Game));
        }
    }
}
