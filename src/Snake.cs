using System;
using System.Collections.Generic;

namespace MiswGame2008
{
    public class Snake : Enemy
    {
        private const int NUM_BODIES = 8;
        private const int POS_BUF_LEN = 4;
        private const int POS_BUF_LEN_SUM = POS_BUF_LEN * NUM_BODIES;
        private const double MAX_SPEED = 8;

        private SnakeBody[] bodies;
        private double[] posBufX;
        private double[] posBufY;
        private int[] angleBuf;

        private double px;
        private double py;
        private double vx;
        private double vy;
        private int targetX;
        private int targetY;
        private int angle;

        private int attackCount;

        private int hitPoints;
        private bool damaged;
        private bool isDying;
        private int dyingCount;

        public Snake(Game game, double x, double y)
            : base(game, x, y)
        {
            bodies = new SnakeBody[NUM_BODIES];
            for (int i = 0; i < NUM_BODIES; i++)
            {
                bodies[i] = new SnakeBody(game, x, y, i == 0);
            }
            posBufX = new double[POS_BUF_LEN_SUM];
            posBufY = new double[POS_BUF_LEN_SUM];
            angleBuf = new int[POS_BUF_LEN_SUM];
            for (int i = 0; i < POS_BUF_LEN_SUM; i++)
            {
                posBufX[i] = x;
                posBufY[i] = y;
                angleBuf[i] = 315;
            }
            px = x;
            py = y;
            vx = vy = 0;
            targetX = game.Random.Next(Game.FieldWidth / 2 - 192, Game.FieldWidth / 2 + 192 + 1);
            targetY = game.Random.Next(Game.FieldHeight / 4 - 64, Game.FieldHeight / 4 + 192 + 1);
            angle = 315;
            attackCount = game.Random.Next(90, 120);
            hitPoints = 32;
            damaged = false;
            isDying = false;
            dyingCount = 0;
        }

        public override void Update()
        {
            damaged = false;

            if (isDying)
            {
                if (dyingCount < POS_BUF_LEN_SUM)
                {
                    if (dyingCount % POS_BUF_LEN == 0)
                    {
                        bodies[dyingCount / POS_BUF_LEN].Destroy();
                    }
                    dyingCount++;
                }
                if (dyingCount == POS_BUF_LEN_SUM)
                {
                    int startAngle = Game.Random.Next(90);
                    for (int i = 0; i < 12; i++)
                    {
                        int angle = startAngle + i * 30 + Game.Random.Next(-30, 31);
                        double x = X + 32 * Utility.Cos(angle);
                        double y = Y - 32 * Utility.Sin(angle);
                        double speed = 0.5 + 1.5 * Game.Random.NextDouble();
                        Effect debris = new Debris(Game, x, y, 128, 128, 128, speed, angle);
                        Game.AddEffect(debris);
                    }
                    Effect effect1 = new BigExplosionEffect(Game, X + 16, Y, Game.Random.Next(360), 255, 255, 255);
                    Effect effect2 = new BigExplosionEffect(Game, X, Y - 16, Game.Random.Next(360), 255, 255, 255);
                    Effect effect3 = new BigExplosionEffect(Game, X - 16, Y, Game.Random.Next(360), 255, 255, 255);
                    Effect effect4 = new BigExplosionEffect(Game, X, Y + 16, Game.Random.Next(360), 255, 255, 255);
                    Game.AddEffect(effect1);
                    Game.AddEffect(effect2);
                    Game.AddEffect(effect3);
                    Game.AddEffect(effect4);
                    Game.PlaySound(Sound.Explosion);
                }
                return;
            }

            px = X;
            py = Y;
            for (int i = 0; i < POS_BUF_LEN_SUM - 1; i++)
            {
                posBufX[i] = posBufX[i + 1];
                posBufY[i] = posBufY[i + 1];
                angleBuf[i] = angleBuf[i + 1];
            }
            posBufX[POS_BUF_LEN_SUM - 1] = X;
            posBufY[POS_BUF_LEN_SUM - 1] = Y;
            angleBuf[POS_BUF_LEN_SUM - 1] = this.angle;

            if (Game.Ticks % 30 == 0)
            {
                if (Game.Random.Next(0, 8) != 0)
                {
                    targetX = Game.Random.Next(Game.FieldWidth / 2 - 192, Game.FieldWidth / 2 + 192 + 1);
                    targetY = Game.Random.Next(Game.FieldHeight / 4 - 64, Game.FieldHeight / 4 + 192 + 1);
                }
                else
                {
                    targetX = (int)Math.Round(Game.Player.X);
                    targetY = (int)Math.Round(Game.Player.Y);
                }
            }
            if (Math.Abs(targetX - X) < 32 && Math.Abs(targetY - Y) < 32)
            {
                targetX = Game.Random.Next(Game.FieldWidth / 2 - 192, Game.FieldWidth / 2 + 192 + 1);
                targetY = Game.Random.Next(Game.FieldHeight / 4 - 64, Game.FieldHeight / 4 + 192 + 1);
            }

            if (X < targetX)
            {
                vx += 1;
            }
            else if (targetX < X)
            {
                vx -= 1;
            }
            if (Math.Abs(vx) > MAX_SPEED)
            {
                vx = Math.Sign(vx) * MAX_SPEED;
            }
            if (Y < targetY)
            {
                vy += 1;
            }
            else if (targetY < Y)
            {
                vy -= 1;
            }
            if (Math.Abs(vy) > MAX_SPEED)
            {
                vy = Math.Sign(vy) * MAX_SPEED;
            }
            X += vx;
            Y += vy;

            for (int i = 0; i < NUM_BODIES; i++)
            {
                int index = POS_BUF_LEN * i;
                bodies[i].X = posBufX[index];
                bodies[i].Y = posBufY[index];
                bodies[i].Angle = angleBuf[index];
            }

            {
                int deg = Utility.NormalizeDeg(Utility.Atan2(py - Y, X - px) - angle);
                if (deg == -180)
                {
                    angle += Game.Random.Next(2) == 0 ? 1 : -1;
                }
                else if (Math.Abs(deg) < 16)
                {
                    angle = Utility.Atan2(py - Y, X - px);
                }
                else
                {
                    if (deg < 0)
                    {
                        angle -= 16;
                    }
                    else if (deg > 0)
                    {
                        angle += 16;
                    }
                }
            }

            if (attackCount > 0)
            {
                attackCount--;
                if (!Game.Player.IsDead)
                {
                    if (attackCount / 2 < NUM_BODIES)
                    {
                        if (attackCount % 2 == 0)
                        {
                            SnakeBody body = bodies[attackCount / 2];
                            int a = Utility.Atan2(body.Y - Game.Player.Y, Game.Player.X - body.X) + Game.Random.Next(-45, 45 + 1);
                            Bullet bullet = new SnakeBullet(Game, body.X, body.Y, Game.Random.Next(4, 8 + 1), a);
                            Game.AddEnemyBullet(bullet);
                        }
                    }
                }
            }
            else
            {
                attackCount = Game.Random.Next(60, 90);
            }
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

        public override void Draw(IGraphics graphics)
        {
            foreach (SnakeBody body in bodies)
            {
                if (body.IsRemoved)
                {
                    continue;
                }
                body.Draw(graphics);
                if (damaged)
                {
                    graphics.EnableAddBlend();
                    body.Draw(graphics);
                    graphics.DisableAddBlend();
                }
            }
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.SnakeHead, drawX, drawY, 64, 64, 0, 0, angle);
            if (damaged)
            {
                graphics.EnableAddBlend();
                graphics.DrawObject(Image.SnakeHead, drawX, drawY, 64, 64, 0, 0, angle);
                graphics.DisableAddBlend();
            }
        }

        public override bool IsRemoved
        {
            get
            {
                return dyingCount == POS_BUF_LEN_SUM;
            }
        }

        public override bool IsDead
        {
            get
            {
                return isDying;
            }
        }

        public override int HalfWidth
        {
            get
            {
                return 28;
            }
        }

        public override int HalfHeight
        {
            get
            {
                return 28;
            }
        }

        public override IEnumerable<Enemy> Children
        {
            get
            {
                return bodies;
            }
        }
    }
}
