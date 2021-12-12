using System;
using System.Collections.Generic;

namespace MiswGame2008
{
    public class Enemy : GameObject
    {
        public Enemy(Game game, double x, double y)
            : base(game, x, y)
        {
        }

        public virtual void Update()
        {
        }

        public virtual bool Hit()
        {
            return false;
        }

        public static bool ShouldBeRemoved(Enemy enemy)
        {
            return enemy.IsRemoved;
        }

        public virtual int HalfWidth
        {
            get
            {
                return 16;
            }
        }

        public virtual int HalfHeight
        {
            get
            {
                return 16;
            }
        }

        public virtual bool IsRemoved
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsDead
        {
            get
            {
                return IsRemoved;
            }
        }

        public virtual IEnumerable<Enemy> Children
        {
            get
            {
                yield break;
            }
        }

        public virtual bool IsObstacle
        {
            get
            {
                return true;
            }
        }

        public virtual void OnRemove()
        {
        }
    }
}
