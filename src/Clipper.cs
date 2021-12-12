using System;
using System.Collections.Generic;

namespace MiswGame2008
{
    public class Clipper : Enemy
    {
        private const int NUM_FUNNEL = 8;

        private int shieldPhase;
        private int shieldRadius;
        private int targetRadius;
        private Funnel[] funnels;

        private int moveCount;
        private int targetX;
        private int targetY;

        private int attackCount;

        private int hitPoints;
        private bool damaged;
        private bool isDead;

        private int animation;

        public Clipper(Game game, double x, double y)
            : base(game, x, y)
        {
            shieldPhase = 0;
            shieldRadius = 128;
            targetRadius = 128;
            funnels = new Funnel[NUM_FUNNEL];
            for (int i = 0; i < NUM_FUNNEL; i++)
            {
                double fx = x + Utility.Cos(shieldPhase + 360 / NUM_FUNNEL * i) * shieldRadius;
                double fy = y - Utility.Sin(shieldPhase + 360 / NUM_FUNNEL * i) * shieldRadius / 2;
                funnels[i] = new Funnel(game, fx, fy);
            }
            moveCount = game.Random.Next(30, 45);
            targetX = game.Random.Next(Game.FieldWidth / 2 - 192, Game.FieldWidth / 2 + 192 + 1);
            targetY = game.Random.Next(Game.FieldHeight / 4 - 64, Game.FieldHeight / 4 + 64 + 1);
            attackCount = game.Random.Next(30, 60);
            hitPoints = 32;
            damaged = false;
            isDead = false;
            animation = 0;
        }

        public override void Update()
        {
            if (hitPoints <= 16)
            {
                targetRadius = 96;
            }
            if (hitPoints <= 8)
            {
                targetRadius = 64;
            }
            if (shieldRadius < targetRadius)
            {
                shieldRadius++;
            }
            else if (shieldRadius > targetRadius)
            {
                shieldRadius--;
            }
            shieldPhase -= 4;
            if (shieldPhase < 0)
            {
                shieldPhase += 360;
            }

            if (moveCount > 0)
            {
                moveCount--;
            }
            else
            {
                moveCount = Game.Random.Next(30, 45);
                targetX = Game.Random.Next(Game.FieldWidth / 2 - 192, Game.FieldWidth / 2 + 192 + 1);
                targetY = Game.Random.Next(Game.FieldHeight / 4 - 64, Game.FieldHeight / 4 + 64 + 1);
            }
            X = targetX * 0.03125 + X * 0.96875;
            Y = targetY * 0.03125 + Y * 0.96875;

            for (int i = 0; i < NUM_FUNNEL; i++)
            {
                funnels[i].X = X + Utility.Cos(shieldPhase + 360 / NUM_FUNNEL * i) * shieldRadius;
                funnels[i].Y = Y - Utility.Sin(shieldPhase + 360 / NUM_FUNNEL * i) * shieldRadius / 2;
            }

            if (attackCount > 0)
            {
                attackCount--;
            }
            else
            {
                if (!Game.Player.IsDead)
                {
                    Funnel funnel = funnels[Game.Random.Next(0, NUM_FUNNEL)];
                    Bullet bullet = new ClipperBullet(Game, funnel.X, funnel.Y + 32, 16, 270);
                    Game.AddEnemyBullet(bullet);
                    Game.PlaySound(Sound.EnemyFire);
                }
                if (hitPoints > 16)
                {
                    attackCount = Game.Random.Next(30, 60);
                }
                else if (hitPoints > 8)
                {
                    attackCount = Game.Random.Next(15, 30);
                }
                else
                {
                    attackCount = Game.Random.Next(10, 20);
                }
            }

            damaged = false;
            animation = (animation + 1) % 32;
        }

        public override bool Hit()
        {
            if (hitPoints > 0)
            {
                hitPoints--;
                damaged = true;
                if (hitPoints > 0)
                {
                    Game.PlaySound(Sound.EnemyDamage);
                }
            }
            if (hitPoints == 0)
            {
                isDead = true;
                foreach (Funnel funnel in funnels)
                {
                    /*
                    int startAngle = Game.Random.Next(90);
                    for (int i = 0; i < 8; i++)
                    {
                        int angle = startAngle + i * 45 + Game.Random.Next(-45, 46);
                        double x = funnel.X + 16 * Utility.Cos(angle);
                        double y = funnel.Y - 16 * Utility.Sin(angle);
                        double speed = 0.5 + 1.5 * Game.Random.NextDouble();
                        Effect debris = new Debris(Game, x, y, 255, 128, 0, speed, angle);
                        Game.AddEffect(debris);
                    }
                    */
                    Effect effect = new BigExplosionEffect(Game, funnel.X, funnel.Y, Game.Random.Next(360), 255, 224, 192);
                    Game.AddEffect(effect);
                }
                {
                    int startAngle = Game.Random.Next(90);
                    for (int i = 0; i < 8; i++)
                    {
                        int angle = startAngle + i * 45 + Game.Random.Next(-45, 46);
                        double x = X + 16 * Utility.Cos(angle);
                        double y = Y - 16 * Utility.Sin(angle);
                        double speed = 0.5 + 1.5 * Game.Random.NextDouble();
                        Effect debris = new Debris(Game, x, y, 0, 0, 255, speed, angle);
                        Game.AddEffect(debris);
                    }
                    Effect effect = new BigExplosionEffect(Game, X, Y, Game.Random.Next(360), 192, 192, 255);
                    Game.AddEffect(effect);
                    Game.PlaySound(Sound.Explosion);
                }
                hitPoints--;
            }
            return true;
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            int index;
            if (animation < 16)
            {
                index = animation / 4;
            }
            else
            {
                index = (31 - animation) / 4;
            }
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.Clipper, drawX, drawY, 32, 32, 0, index, 0);
            if (damaged)
            {
                graphics.EnableAddBlend();
                graphics.DrawObject(Image.Clipper, drawX, drawY, 32, 32, 0, index, 0);
                graphics.DisableAddBlend();
            }
            foreach (Funnel funnel in funnels)
            {
                funnel.Draw(graphics);
                if (damaged)
                {
                    graphics.EnableAddBlend();
                    funnel.Draw(graphics);
                    graphics.DisableAddBlend();
                }
            }
        }

        public override bool IsRemoved
        {
            get
            {
                return isDead;
            }
        }

        public override int HalfWidth
        {
            get
            {
                return 12;
            }
        }

        public override int HalfHeight
        {
            get
            {
                return 12;
            }
        }

        public override IEnumerable<Enemy> Children
        {
            get
            {
                return funnels;
            }
        }
    }
}
