using System;

namespace MiswGame2008
{
    public class Player : GameObject
    {
        private static double MAX_SPEED = 8;
        private static double SPEED_UP = 4;
        private static double SPEED_DOWN = 2;

        private double vx, vy;
        private int fireCount;
        private bool isDead;
        private bool freeze;
        private int warpSpeed;

        public Player(Game game, double x, double y)
            : base(game, x, y)
        {
            vx = vy = 0;
            fireCount = 0;
            isDead = false;
            freeze = false;
            warpSpeed = 8;
        }

        public void Update(GameCommand command)
        {
            if (isDead || freeze)
            {
                return;
            }

            if (command.Left != command.Right)
            {
                if (command.Left)
                {
                    vx -= SPEED_UP;
                    if (vx < -MAX_SPEED)
                    {
                        vx = -MAX_SPEED;
                    }
                }
                if (command.Right)
                {
                    vx += SPEED_UP;
                    if (vx > MAX_SPEED)
                    {
                        vx = MAX_SPEED;
                    }
                }
            }
            else
            {
                if (Math.Abs(vx) < SPEED_DOWN)
                {
                    vx = 0;
                }
                else
                {
                    vx -= Math.Sign(vx) * SPEED_DOWN;
                }
            }
            if (command.Up != command.Down)
            {
                if (command.Up)
                {
                    vy -= SPEED_UP;
                    if (vy < -MAX_SPEED)
                    {
                        vy = -MAX_SPEED;
                    }
                }
                if (command.Down)
                {
                    vy += SPEED_UP;
                    if (vy > MAX_SPEED)
                    {
                        vy = MAX_SPEED;
                    }
                }
            }
            else
            {
                if (Math.Abs(vy) < SPEED_DOWN)
                {
                    vy = 0;
                }
                else
                {
                    vy -= Math.Sign(vy) * SPEED_DOWN;
                }
            }

            X += vx;
            Y += vy;

            if (X < 16)
            {
                X = 16;
            }
            else if (X > Game.FieldWidth - 16)
            {
                X = Game.FieldWidth - 16;
            }
            if (Y < Game.FieldHeight / 4)
            {
                Y = Game.FieldHeight / 4;
            }
            else if (Y > Game.FieldHeight - 16)
            {
                Y = Game.FieldHeight - 16;
            }

            if (fireCount > 0)
            {
                fireCount--;
            }
            if (command.Button1)
            {
                if (fireCount == 0)
                {
                    if (Game.PlayerBulletList.Count < 8)
                    {
                        Bullet bullet = new PlayerBullet(Game, X, Y - 32, 16, 90);
                        Game.AddPlayerBullet(bullet);
                        Game.PlaySound(Sound.PlayerFire);
                        fireCount = 2;
                    }
                }
            }
        }

        public void Warp(bool go)
        {
            X = X * 0.9375 + (Game.FieldWidth / 2) * 0.0625;
            if (go)
            {
                Y -= warpSpeed;
                warpSpeed += 8;
                if (Y < -16)
                {
                    Y = -16;
                }
            }
            else
            {
                Y = Y * 0.75 + (Game.FieldHeight - 48) * 0.25;
            }
        }

        public void Hit()
        {
            isDead = true;
            Effect effect = new BigExplosionEffect(Game, X, Y, Game.Random.Next(360), 255, 255, 255);
            Game.AddEffect(effect);
            Game.PlaySound(Sound.Explosion);
        }

        public override void Draw(IGraphics graphics)
        {
            if (isDead)
            {
                return;
            }
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.Player, drawX, drawY, 32, 32, 0, 0, 0);
            if (warpSpeed > 8)
            {
                graphics.EnableAddBlend();
                int phase = Game.Ticks % 3;
                for (int i = 0; i < 11; i++)
                {
                    graphics.SetColor(255, (i + phase) % 3 == 0 ? 255 : 0, (i + phase) % 3 == 1 ? 255 : 0, (i + phase) % 3 == 2 ? 255 : 0);
                    graphics.DrawObject(Image.Player, drawX, drawY + i * 48 + phase * 16, 32, 32, 0, 0, 0);
                }
                graphics.DisableAddBlend();
            }
        }

        public void Freeze()
        {
            freeze = true;
        }

        public bool IsDead
        {
            get
            {
                return isDead;
            }
        }

        public int HalfWidth
        {
            get
            {
                return 12;
            }
        }

        public int HalfHeight
        {
            get
            {
                return 12;
            }
        }
    }
}
