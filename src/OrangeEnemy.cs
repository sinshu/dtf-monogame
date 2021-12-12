using System;

namespace MiswGame2008
{
    public class OrangeEnemy : Enemy
    {
        private const double MAX_SPEED = 8;

        private int angle;
        private double vx, vy;
        private int waitCount;
        private double waitX, waitY;
        private double swingWidth, swingHeight;
        private int playerXTraceOffset;
        private int fireCount;
        private bool attacking;

        private int hitPoints;
        private bool damaged;

        public OrangeEnemy(Game game, double x, double y)
            : base(game, x, y)
        {
            angle = 90;
            vx = vy = 0;
            waitCount = game.Random.Next(15, 120);
            waitX = x;
            waitY = y;
            swingWidth = (x - Game.FieldWidth / 2) / 8;
            swingHeight = (y - 48) / 16;
            playerXTraceOffset = game.Random.Next(-96, 96 + 1);
            fireCount = game.Random.Next(30, 60);
            attacking = false;
            hitPoints = 2;
            damaged = false;
        }

        public override void Update()
        {
            if (waitCount > 0)
            {
                double newX = waitX + swingWidth * Utility.Sin(Game.Ticks * 8);
                double newY = waitY + swingHeight * Utility.Sin(Game.Ticks * 8);
                vx = newX - X;
                vy = newY - Y;
                X = newX;
                Y = newY;
                waitCount--;
            }
            if (waitCount == 0)
            {
                if (!attacking)
                {
                    if (angle == 90)
                    {
                        angle += Game.Random.Next(2) == 0 ? 1 : -1;
                    }
                    else
                    {
                        int deg = Utility.NormalizeDeg(270 - angle);
                        if (Math.Abs(deg) < 8)
                        {
                            angle = 270;
                            attacking = true;
                        }
                        else
                        {
                            if (deg < 0)
                            {
                                angle -= 8;
                            }
                            else if (deg > 0)
                            {
                                angle += 8;
                            }
                        }
                    }
                    vx += 0.5 * Utility.Cos(angle);
                    vy -= 0.5 * Utility.Sin(angle);
                    Move();
                }
                else
                {
                    if (X < Game.Player.X + playerXTraceOffset)
                    {
                        vx += 0.25;
                    }
                    else if (X > Game.Player.X + playerXTraceOffset)
                    {
                        vx -= 0.25;
                    }
                    vy += 0.25;
                    Move();
                    {
                        int deg = Utility.NormalizeDeg(Utility.Atan2(Y - Game.Player.Y, Game.Player.X - X) - angle);
                        if (deg == -180)
                        {
                            angle += Game.Random.Next(2) == 0 ? 1 : -1;
                        }
                        else if (Math.Abs(deg) < 2)
                        {
                            angle = Utility.Atan2(Y - Game.Player.Y, Game.Player.X - X);
                        }
                        else
                        {
                            if (deg < 0)
                            {
                                angle -= 2;
                            }
                            else if (deg > 0)
                            {
                                angle += 2;
                            }
                        }
                        angle = (angle % 360 + 360) % 360;
                        if (angle < 225)
                        {
                            angle = 225;
                        }
                        else if (angle > 315)
                        {
                            angle = 315;
                        }
                    }
                    if (fireCount > 0)
                    {
                        fireCount--;
                    }
                    else if (!Game.Player.IsDead)
                    {
                        if (Y < Game.Player.Y)
                        {
                            Bullet bullet1 = new OrangeBullet(Game, X + 32 * Utility.Cos(angle), Y - 32 * Utility.Sin(angle), 12, angle - 15);
                            Bullet bullet2 = new OrangeBullet(Game, X + 32 * Utility.Cos(angle), Y - 32 * Utility.Sin(angle), 12, angle);
                            Bullet bullet3 = new OrangeBullet(Game, X + 32 * Utility.Cos(angle), Y - 32 * Utility.Sin(angle), 12, angle + 15);
                            Game.AddEnemyBullet(bullet1);
                            Game.AddEnemyBullet(bullet2);
                            Game.AddEnemyBullet(bullet3);
                            Game.PlaySound(Sound.OrangeFire);
                            fireCount = Game.Random.Next(60, 90);
                            playerXTraceOffset = Game.Random.Next(-96, 96 + 1);
                        }
                        else
                        {
                            fireCount = Game.Random.Next(15, 30);
                            playerXTraceOffset = Game.Random.Next(-96, 96 + 1);
                        }
                    }
                }
            }
            damaged = false;
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
                int startAngle = Game.Random.Next(90);
                for (int i = 0; i < 8; i++)
                {
                    int angle = startAngle + i * 45 + Game.Random.Next(-45, 46);
                    double x = X + 16 * Utility.Cos(angle);
                    double y = Y - 16 * Utility.Sin(angle);
                    double speed = 0.5 + 1.5 * Game.Random.NextDouble();
                    Effect debris = new Debris(Game, x, y, 255, 128, 0, speed, angle);
                    Game.AddEffect(debris);
                }
                Effect effect = new BigExplosionEffect(Game, X, Y, Game.Random.Next(360), 255, 224, 192);
                Game.AddEffect(effect);
                Game.PlaySound(Sound.Explosion);
                // ˆê‰ž
                hitPoints--;
            }
            return true;
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.OrangeEnemy, drawX, drawY, 32, 32, 0, 0, angle);
            if (damaged)
            {
                graphics.EnableAddBlend();
                graphics.DrawObject(Image.OrangeEnemy, drawX, drawY, 32, 32, 0, 0, angle);
                graphics.DisableAddBlend();
            }
        }

        private void Move()
        {
            if (vx < -MAX_SPEED)
            {
                vx = -MAX_SPEED;
            }
            else if (vx > MAX_SPEED)
            {
                vx = MAX_SPEED;
            }
            if (vy < -MAX_SPEED)
            {
                vy = -MAX_SPEED;
            }
            else if (vy > MAX_SPEED / 2)
            {
                vy = MAX_SPEED / 2;
            }
            X += vx;
            Y += vy;
            if (X < -16)
            {
                X = Game.FieldWidth + 16;
            }
            else if (X > Game.FieldWidth + 16)
            {
                X = -16;
            }
            if (Y > Game.FieldHeight + 16)
            {
                Y = -16;
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

        public override bool IsRemoved
        {
            get
            {
                return hitPoints <= 0;
            }
        }
    }
}
