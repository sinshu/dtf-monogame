using System;

namespace MiswGame2008
{
    public class LastBoss : Enemy
    {
        private int targetX;
        private int targetY;
        private double x2;
        private double y2;
        private int moveCount;

        private int spawnEnemyType;

        private int hitPoints;
        private bool damaged;
        private bool isDying;
        private int dyingCount;

        private int animation;

        public LastBoss(Game game, double x, double y)
            : base(game, x, y)
        {
            targetX = Game.FieldWidth / 2;
            targetY = Game.FieldHeight / 4;
            x2 = X;
            y2 = y;
            moveCount = game.Random.Next(30, 60);
            spawnEnemyType = 0;
            hitPoints = 192;
            damaged = false;
            isDying = false;
            dyingCount = 0;
            animation = 0;
        }

        public override void Update()
        {
            damaged = false;

            if (isDying)
            {
                if (dyingCount < 60)
                {
                    if (dyingCount % 2 == 0)
                    {
                        Effect effect = new SmallExplosionEffect(Game, X + Game.Random.Next(-32, 32 + 1), Y + Game.Random.Next(-32, 32 + -+1), Game.Random.Next(0, 360));
                        Game.AddEffect(effect);
                        if (dyingCount % 4 == 0)
                        {
                            Game.PlaySound(Sound.Explosion);
                        }
                    }
                    dyingCount++;
                    if (dyingCount == 60)
                    {
                        Effect effect = new BigExplosionEffect(Game, X, Y, Game.Random.Next(0, 360), 255, 255, 255);
                        Game.AddEffect(effect);
                        Game.PlaySound(Sound.Explosion);
                    }
                }
                return;
            }

            if (moveCount > 0)
            {
                moveCount--;
            }
            else
            {
                targetX = Game.FieldWidth / 2 + Game.Random.Next(-64, 64 + 1);
                targetY = Game.FieldHeight / 4 - Game.Random.Next(0, 64 + 1);
                moveCount = Game.Random.Next(30, 60);
            }

            spawnEnemyType = (192 - hitPoints) / 32;
            if (!Game.Player.IsDead && !isDying && Game.Ticks >= 60)
            {
                switch (spawnEnemyType)
                {
                    case 0:
                        if (Game.Ticks % 8 == 0)
                        {
                            Effect effect = new GreenEnemySpawner(Game, X + Game.Random.Next(-64, 65), Y + Game.Random.Next(0, 64));
                            Game.AddEffect(effect);
                        }
                        break;
                    case 1:
                        if (Game.Ticks % 8 == 0)
                        {
                            Effect effect = new BlueEnemySpawner(Game, X + Game.Random.Next(-64, 65), Y + Game.Random.Next(0, 64));
                            Game.AddEffect(effect);
                        }
                        break;
                    case 2:
                        if (Game.Ticks % 8 == 0)
                        {
                            Effect effect = new RedEnemySpawner(Game, X + Game.Random.Next(-64, 65), Y + Game.Random.Next(0, 64));
                            Game.AddEffect(effect);
                        }
                        break;
                    case 3:
                        if (Game.Ticks % 16 == 0)
                        {
                            Effect effect = new OrangeEnemySpawner(Game, X + Game.Random.Next(-64, 65), Y + Game.Random.Next(0, 64));
                            Game.AddEffect(effect);
                        }
                        break;
                    case 4:
                        if (Game.Ticks % 16 == 0)
                        {
                            Effect effect = new MissileEnemySpawner(Game, X + Game.Random.Next(-64, 65), Y + Game.Random.Next(0, 64));
                            Game.AddEffect(effect);
                        }
                        break;
                    case 5:
                        if (Game.Ticks % 16 == 0)
                        {
                            Effect effect = new KurageSpawner(Game, X + Game.Random.Next(-64, 65), Y + Game.Random.Next(0, 64));
                            Game.AddEffect(effect);
                        }
                        break;
                }
            }
            else
            {
                hitPoints = 192;
            }

            x2 = targetX * 0.125 + x2 * 0.875;
            y2 = targetY * 0.125 + y2 * 0.875;
            X = x2 * 0.125 + X * 0.875;
            Y = y2 * 0.125 + Y * 0.875;

            animation = (animation + 1) % 8;
        }

        public override void Draw(IGraphics graphics)
        {
            int drawX = (int)Math.Round(X);
            int drawY = (int)Math.Round(Y);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawObject(Image.LastBoss, drawX, drawY, 64, 64, 0, animation / 2, 0);
            if (damaged)
            {
                graphics.EnableAddBlend();
                graphics.DrawObject(Image.LastBoss, drawX, drawY, 64, 64, 0, animation / 2, 0);
                graphics.DisableAddBlend();
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

        public override bool IsRemoved
        {
            get
            {
                return dyingCount == 60;
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
    }
}
