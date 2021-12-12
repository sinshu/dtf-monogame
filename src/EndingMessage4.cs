using System;

namespace MiswGame2008
{
    public class EndingMessage4 : EndingMessage
    {
        public EndingMessage4(Game game)
            : base(game, Game.FieldWidth + 128, Game.FieldHeight / 4, new string[] { "BGM", "", "GIGAS" })
        {
        }

        public override void OnRemove()
        {
            Game.AddEnemyInGame(new EndingMessage5(Game));
        }
    }
}
