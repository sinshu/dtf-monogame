using System;

namespace MiswGame2008
{
    public class Effect : GameObject
    {
        public Effect(Game game, double x, double y)
            : base(game, x, y)
        {
        }

        public virtual void Update()
        {
        }

        public static bool ShouldBeRemoved(Effect effect)
        {
            return effect.IsRemoved;
        }

        public virtual bool IsRemoved
        {
            get
            {
                return false;
            }
        }
    }
}
