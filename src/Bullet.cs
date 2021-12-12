using System;

namespace MiswGame2008
{
    public class Bullet : GameObject
    {
        private double speed;
        private int angle;

        public Bullet(Game game, double x, double y, double speed, int angle)
            : base(game, x, y)
        {
            this.speed = speed;
            this.angle = angle;
        }

        public virtual void Update()
        {
            X += speed * Utility.Cos(angle);
            Y -= speed * Utility.Sin(angle);
        }

        public virtual bool IsRemoved
        {
            get
            {
                return false;
            }
        }

        public virtual void Hit()
        {
        }

        public static bool ShouldBeRemoved(Bullet bullet)
        {
            return bullet.IsRemoved;
        }

        public double Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public int Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
            }
        }
    }
}
