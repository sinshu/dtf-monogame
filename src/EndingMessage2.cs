using System;

namespace MiswGame2008
{
    public class EndingMessage2 : EndingMessage
    {
        public EndingMessage2(Game game)
            : base(game, Game.FieldWidth + 128, Game.FieldHeight / 4, new string[] { "GRAPHICS", "", "EXRD", "IORI", "LASTEA", "RIKKA", "SINSHU" })
        {
        }

        public override void OnRemove()
        {
            Game.AddEnemyInGame(new EndingMessage3(Game));
        }
    }
}
