using System;

namespace MiswGame2008
{
    public class GameObject
    {
        private Game game;
        private double x, y;

        public GameObject(Game game, double x, double y)
        {
            this.game = game;
            this.x = x;
            this.y = y;
        }

        public virtual void Draw(IGraphics graphics)
        {
        }

        public Game Game
        {
            get
            {
                return game;
            }
        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }
    }
}
