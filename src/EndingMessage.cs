using System;

namespace MiswGame2008
{
    public class EndingMessage : Enemy
    {
        private string[] message;
        private int hitPoints;
        private int width, height;

        public EndingMessage(Game game, double x, double y, string[] message)
            : base(game, x, y)
        {
            this.message = message;
            hitPoints = 16;
            int maxWidth = 0;
            foreach (string s in message)
            {
                if (s.Length * 16 > maxWidth)
                {
                    maxWidth = s.Length * 16;
                }
            }
            width = maxWidth;
            height = message.Length * 16;
        }

        public override void Update()
        {
            X = Game.FieldWidth / 2 * 0.0625 + X * 0.9375;
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            for (int i = 0; i < message.Length; i++)
            {
                int targetX = drawX - message[i].Length * 8;
                int targetY = drawY - message.Length * 8 + i * 16;
                graphics.DrawString(message[i], targetX, targetY);
            }
        }

        public override bool Hit()
        {
            if (hitPoints > 0)
            {
                hitPoints--;
                if (hitPoints > 0)
                {
                    Game.PlaySound(Sound.EnemyDamage);
                }
            }
            if (hitPoints == 0)
            {
                for (int i = 0; i < message.Length; i++)
                {
                    for (int j = 0; j < message[i].Length; j++)
                    {
                        double targetX = X - message[i].Length * 8 + j * 16 + 8;
                        double targetY = Y - message.Length * 8 + i * 16 + 8;
                        Effect effect = new SmallExplosionEffect(Game, targetX, targetY, Game.Random.Next(0, 360));
                        Effect debris1 = new Debris(Game, targetX, targetY, 255, 255, 255, Game.Random.Next(1, 5), Game.Random.Next(0, 360));
                        Effect debris2 = new Debris(Game, targetX, targetY, 255, 255, 255, Game.Random.Next(1, 5), Game.Random.Next(0, 360));
                        Game.AddEffect(effect);
                        Game.AddEffect(debris1);
                        Game.AddEffect(debris2);
                    }
                }
                Game.PlaySound(Sound.Explosion);
                hitPoints--;
            }
            return true;
        }

        public override bool IsRemoved
        {
            get
            {
                return hitPoints <= 0;
            }
        }

        public override int HalfWidth
        {
            get
            {
                return width / 2;
            }
        }

        public override int HalfHeight
        {
            get
            {
                return height / 2;
            }
        }

        public override bool IsObstacle
        {
            get
            {
                return false;
            }
        }

        public override void OnRemove()
        {
            Console.WriteLine("AAA");
        }
    }
}
