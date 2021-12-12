using System;

namespace MiswGame2008
{
    public class FirstBoss2 : Enemy
    {
        private int targetX;
        private int targetY;
        private double x2;
        private double y2;
        private int moveCount;
        private int attackMode;

        private int hitPoints;
        private bool damaged;
        private bool isDying;
        private int dyingCount;

        public FirstBoss2(Game game, double x, double y)
            : base(game, x, y)
        {
            targetX = Game.FieldWidth / 2;
            targetY = Game.FieldHeight / 4;
            x2 = X;
            y2 = y;
            moveCount = game.Random.Next(30, 60);
            attackMode = 0;
            hitPoints = 96;
            damaged = false;
            isDying = false;
            dyingCount = 0;
        }

        public override void Update()
        {
            damaged = false;

            if (isDying)
            {
                if (dyingCount < 30)
                {
                    if (dyingCount % 2 == 0)
                    {
                        Effect effect = new SmallExplosionEffect(Game, X + Game.Random.Next(-64, 64 + 1), Y + Game.Random.Next(-32, 32 + -+1), Game.Random.Next(0, 360));
                        Game.AddEffect(effect);
                        if (dyingCount % 4 == 0)
                        {
                            Game.PlaySound(Sound.Explosion);
                        }
                    }
                    dyingCount++;
                    if (dyingCount == 30)
                    {
                        int startAngle = Game.Random.Next(90);
                        for (int i = 0; i < 20; i++)
                        {
                            int angle = startAngle + i * 18 + Game.Random.Next(-18, 19);
                            double x = X + 64 * Utility.Cos(angle);
                            double y = Y - 32 * Utility.Sin(angle);
                            double speed = 0.5 + 1.5 * Game.Random.NextDouble();
                            Effect debris = new Debris(Game, x, y, 80, 80, 80, speed, angle);
                            Game.AddEffect(debris);
                        }
                        for (int i = -1; i <= 1; i++)
                        {
                            Effect effect = new BigExplosionEffect(Game, X - i * 48, Y, Game.Random.Next(0, 360), 255, 255, 255);
                            Game.AddEffect(effect);
                        }
                        Game.PlaySound(Sound.Explosion);
                    }
                }
                return;
            }

            if (moveCount > 0)
            {
                moveCount--;
                if (attackMode == 0)
                {
                    if (!(Math.Abs(X - targetX) < 32 && Math.Abs(Y - targetY) < 32))
                    {
                        Effect smoke1 = new Smoke(Game, X - 24, Y - 24, Game.Random.Next(0, 360));
                        Effect smoke2 = new Smoke(Game, X + 24, Y - 24, Game.Random.Next(0, 360));
                        Game.AddEffect(smoke1);
                        Game.AddEffect(smoke2);
                    }
                }
                if (!Game.Player.IsDead)
                {
                    if (attackMode == 1)
                    {
                        if (moveCount == 4)
                        {
                            Bullet bullet1 = new OrangeBullet(Game, X - 32 + 32 * Utility.Cos(255), Y - 32 * Utility.Sin(255), 12, 255);
                            Bullet bullet2 = new OrangeBullet(Game, X - 32 + 32 * Utility.Cos(270), Y - 32 * Utility.Sin(270), 12, 270);
                            Bullet bullet3 = new OrangeBullet(Game, X - 32 + 32 * Utility.Cos(285), Y - 32 * Utility.Sin(285), 12, 285);
                            Game.AddEnemyBullet(bullet1);
                            Game.AddEnemyBullet(bullet2);
                            Game.AddEnemyBullet(bullet3);
                            Game.PlaySound(Sound.OrangeFire);
                        }
                        else if (moveCount == 8)
                        {
                            Bullet bullet1 = new OrangeBullet(Game, X + 32 + 32 * Utility.Cos(255), Y - 32 * Utility.Sin(255), 12, 255);
                            Bullet bullet2 = new OrangeBullet(Game, X + 32 + 32 * Utility.Cos(270), Y - 32 * Utility.Sin(270), 12, 270);
                            Bullet bullet3 = new OrangeBullet(Game, X + 32 + 32 * Utility.Cos(285), Y - 32 * Utility.Sin(285), 12, 285);
                            Game.AddEnemyBullet(bullet1);
                            Game.AddEnemyBullet(bullet2);
                            Game.AddEnemyBullet(bullet3);
                            Game.PlaySound(Sound.OrangeFire);
                        }
                    }
                    else if (attackMode == 2)
                    {
                        if (moveCount == 4)
                        {
                            Bullet bullet1 = new OrangeBullet(Game, X + 32 + 32 * Utility.Cos(255), Y - 32 * Utility.Sin(255), 12, 255);
                            Bullet bullet2 = new OrangeBullet(Game, X + 32 + 32 * Utility.Cos(270), Y - 32 * Utility.Sin(270), 12, 270);
                            Bullet bullet3 = new OrangeBullet(Game, X + 32 + 32 * Utility.Cos(285), Y - 32 * Utility.Sin(285), 12, 285);
                            Game.AddEnemyBullet(bullet1);
                            Game.AddEnemyBullet(bullet2);
                            Game.AddEnemyBullet(bullet3);
                            Game.PlaySound(Sound.OrangeFire);
                        }
                        else if (moveCount == 8)
                        {
                            Bullet bullet1 = new OrangeBullet(Game, X - 32 + 32 * Utility.Cos(255), Y - 32 * Utility.Sin(255), 12, 255);
                            Bullet bullet2 = new OrangeBullet(Game, X - 32 + 32 * Utility.Cos(270), Y - 32 * Utility.Sin(270), 12, 270);
                            Bullet bullet3 = new OrangeBullet(Game, X - 32 + 32 * Utility.Cos(285), Y - 32 * Utility.Sin(285), 12, 285);
                            Game.AddEnemyBullet(bullet1);
                            Game.AddEnemyBullet(bullet2);
                            Game.AddEnemyBullet(bullet3);
                            Game.PlaySound(Sound.OrangeFire);
                        }
                    }
                    else if (attackMode == 3)
                    {
                        if (moveCount == 12)
                        {
                            Bullet missile = new Missile(Game, X, Y + 16 - 32 * Utility.Sin(255), 270);
                            Game.AddEnemyBullet(missile);
                            Game.PlaySound(Sound.MissileFire);
                        }
                    }
                }
            }
            else
            {
                if (attackMode > 0)
                {
                    attackMode = Game.Random.Next(0, 4);
                }
                else
                {
                    attackMode = Game.Random.Next(1, 4);
                }
                targetX = (int)Math.Round(Game.Player.X) + Game.Random.Next(-32, 32 + 1);
                if (attackMode == 0)
                {
                    targetY = Game.FieldHeight - 32;
                }
                else
                {
                    targetY = Game.FieldHeight / 4 + Game.Random.Next(-32, 64 + 1);
                }
                /*
                if (hitPoints > 48)
                {
                    moveCount = Game.Random.Next(30, 60);
                }
                else if (hitPoints > 24)
                {
                    moveCount = Game.Random.Next(25, 50);
                }
                else
                {
                    moveCount = Game.Random.Next(20, 40);
                }
                */
                moveCount = Game.Random.Next(20, 30);
                if (targetX < 80)
                {
                    targetX = 80;
                }
                else if (targetX > Game.FieldWidth - 80)
                {
                    targetX = Game.FieldWidth - 80;
                }
            }
            x2 = targetX * 0.125 + x2 * 0.875;
            y2 = targetY * 0.125 + y2 * 0.875;
            X = x2 * 0.125 + X * 0.875;
            Y = y2 * 0.125 + Y * 0.875;
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
                hitPoints--;
                isDying = true;
                return true;
            }
            return !isDying;
        }

        public override bool IsRemoved
        {
            get
            {
                return dyingCount == 30;
            }
        }

        public override bool IsDead
        {
            get
            {
                return isDying;
            }
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            int angle = (int)Math.Round(3 * Utility.Sin(Game.Ticks * 8));
            int index;
            index = Game.Ticks / 2 % 6;
            if (index > 3)
            {
                index = 6 - index;
            }
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.FirstBoss, drawX, drawY, 128, 64, 0, index, angle);
            if (damaged)
            {
                graphics.EnableAddBlend();
                graphics.DrawObject(Image.FirstBoss, drawX, drawY, 128, 64, 0, index, angle);
                graphics.DisableAddBlend();
            }
        }

        public override int HalfWidth
        {
            get
            {
                return 60;
            }
        }

        public override int HalfHeight
        {
            get
            {
                return 16;
            }
        }
    }
}
