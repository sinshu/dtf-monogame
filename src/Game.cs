using System;
using System.Collections.Generic;

namespace MiswGame2008
{
    public class Game
    {
        private static int FIELD_WIDTH = 480;
        private static int FIELD_HEIGHT = 480;

        // •`‰æ—p
        private static Random drawRandom = new Random();

        private Random random;

        private Player player;
        private List<Bullet> playerBulletList;
        private List<Enemy> enemyList;
        private List<Enemy> enemyAddList;
        private List<Bullet> enemyBulletList;
        private List<Effect> effectList;

        private int ticks;

        private int playerDeadTicks;
        private bool warping;
        private int warpTicks;

        private bool gameOver;
        private bool goingToNextStage;

        private int highScore;
        private int score;
        private int left;
        private int tension;

        private bool returnToTitle;

        private List<Sound> sounds;

        public Game(Random random, int highScore, int score, int left)
        {
            this.random = random;
            playerBulletList = new List<Bullet>();
            enemyList = new List<Enemy>();
            enemyAddList = new List<Enemy>();
            enemyBulletList = new List<Bullet>();
            effectList = new List<Effect>();
            this.highScore = highScore;
            this.score = score;
            this.left = left;
            tension = 0;

            sounds = new List<Sound>();

            Restart();
        }

        public virtual void Restart()
        {
            player = new Player(this, FIELD_WIDTH / 2, FIELD_HEIGHT - 48);
            playerBulletList.Clear();
            enemyList.Clear();
            enemyAddList.Clear();
            enemyBulletList.Clear();
            effectList.Clear();
            ticks = 0;
            playerDeadTicks = 0;
            warping = false;
            warpTicks = 0;
            gameOver = false;
            goingToNextStage = false;
            returnToTitle = false;
            InitEnemies();
        }

        public virtual void InitEnemies()
        {
            for (int i = -3; i <= 3; i++)
            {
                if (-1 <= i && i <= 1)
                {
                    AddEnemy(new MissileEnemy(this, 240 + 64 * i, 64));
                }
                else
                {
                    AddEnemy(new BlueEnemy(this, 240 + 64 * i, 64));
                }
            }
            for (int i = -2; i < 2; i++)
            {
                AddEnemy(new RedEnemy(this, 240 + 64 * i + 32, 96));
            }
            for (int i = -3; i <= 3; i++)
            {
                AddEnemy(new GreenEnemy(this, 240 + 64 * i, 128));
            }
        }

        public virtual void Update(GameCommand command)
        {
            ClearSounds();

            foreach (Effect effect in effectList)
            {
                effect.Update();
            }
            foreach (Bullet bullet in playerBulletList)
            {
                bullet.Update();
            }
            player.Update(command);
            foreach (Bullet bullet in enemyBulletList)
            {
                bullet.Update();
            }
            foreach (Enemy enemy in enemyList)
            {
                enemy.Update();
            }
            foreach (Enemy enemy in enemyAddList)
            {
                enemyList.Add(enemy);
            }
            enemyAddList.Clear();

            foreach (Bullet bullet in playerBulletList)
            {
                foreach (Enemy enemy in enemyList)
                {
                    if (bullet.IsRemoved || enemy.IsRemoved)
                    {
                        continue;
                    }
                    if (Math.Abs(enemy.X - bullet.X) < enemy.HalfWidth && Math.Abs(enemy.Y - bullet.Y) < enemy.HalfHeight)
                    {
                        bullet.Hit();
                        if (enemy.Hit())
                        {
                            tension += 100;
                            score += tension;
                        }
                    }
                    foreach (Enemy child in enemy.Children)
                    {
                        if (bullet.IsRemoved || child.IsRemoved)
                        {
                            continue;
                        }
                        if (Math.Abs(child.X - bullet.X) < child.HalfWidth && Math.Abs(child.Y - bullet.Y) < child.HalfHeight)
                        {
                            bullet.Hit();
                            if (child.Hit())
                            {
                                tension += 100;
                                score += tension;
                            }
                        }
                    }
                }
            }
            if (!player.IsDead && !warping)
            {
                foreach (Enemy enemy in enemyList)
                {
                    if (enemy.IsRemoved || !enemy.IsObstacle)
                    {
                        continue;
                    }
                    if (Math.Abs(player.X - enemy.X) < player.HalfWidth + enemy.HalfWidth && Math.Abs(player.Y - enemy.Y) < player.HalfHeight + enemy.HalfHeight)
                    {
                        enemy.Hit();
                        player.Hit();
                    }
                    foreach (Enemy child in enemy.Children)
                    {
                        if (child.IsRemoved || !child.IsObstacle)
                        {
                            continue;
                        }
                        if (Math.Abs(player.X - child.X) < player.HalfWidth + child.HalfWidth && Math.Abs(player.Y - child.Y) < player.HalfHeight + child.HalfHeight)
                        {
                            child.Hit();
                            player.Hit();
                        }
                    }
                }
                foreach (Bullet bullet in enemyBulletList)
                {
                    if (bullet.IsRemoved)
                    {
                        continue;
                    }
                    if (Math.Abs(player.X - bullet.X) < player.HalfWidth && Math.Abs(player.Y - bullet.Y) < player.HalfHeight)
                    {
                        bullet.Hit();
                        player.Hit();
                    }
                }
            }

            playerBulletList.RemoveAll(Bullet.ShouldBeRemoved);
            foreach (Enemy enemy in enemyList)
            {
                if (Enemy.ShouldBeRemoved(enemy))
                {
                    enemy.OnRemove();
                }
            }
            enemyList.RemoveAll(Enemy.ShouldBeRemoved);
            enemyBulletList.RemoveAll(Bullet.ShouldBeRemoved);
            effectList.RemoveAll(Effect.ShouldBeRemoved);
            foreach (Enemy enemy in enemyAddList)
            {
                enemyList.Add(enemy);
            }
            enemyAddList.Clear();

            if (player.IsDead)
            {
                if (playerDeadTicks < 30)
                {
                    playerDeadTicks++;
                }
                else
                {
                    gameOver = true;
                }
                // ˆê‰ž
                warping = false;
                warpTicks = 0;
            }
            else if (enemyList.Count == 0 && enemyBulletList.Count == 0)
            {
                warping = true;
            }
            if (warping)
            {
                if (warpTicks < 60)
                {
                    if (warpTicks == 15)
                    {
                        player.Freeze();
                    }
                    else if (warpTicks > 15)
                    {
                        if (warpTicks == 30)
                        {
                            PlaySound(Sound.Warp);
                        }
                        player.Warp(warpTicks >= 30);
                    }
                    warpTicks++;
                }
                else
                {
                    score += tension * 50;
                    goingToNextStage = true;
                }
            }

            if (command.Exit)
            {
                returnToTitle = true;
            }

            if (CoutAliveEnemies() > 0)
            {
                tension -= 10;
                if (tension < 0)
                {
                    tension = 0;
                }
            }

            ticks++;
        }

        public virtual void Draw(IGraphics graphics)
        {
            DrawBackground(graphics);
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(graphics);
            }
            foreach (Bullet bullet in playerBulletList)
            {
                bullet.Draw(graphics);
            }
            foreach (Effect effect in effectList)
            {
                effect.Draw(graphics);
            }
            foreach (Bullet bullet in enemyBulletList)
            {
                bullet.Draw(graphics);
            }
            player.Draw(graphics);
            if (ticks < 8)
            {
                int alpha = (8 - ticks) * 32;
                if (alpha > 255)
                {
                    alpha = 255;
                }
                graphics.SetColor(alpha, 255, 255, 255);
                graphics.DrawRectangle(0, 0, FIELD_WIDTH, FIELD_HEIGHT);
            }
            if (left > 0)
            {
                if (playerDeadTicks > 30 - 8)
                {
                    int alpha = (playerDeadTicks - (30 - 8)) * 32;
                    if (alpha > 255)
                    {
                        alpha = 255;
                    }
                    graphics.SetColor(alpha, 255, 255, 255);
                    graphics.DrawRectangle(0, 0, FIELD_WIDTH, FIELD_HEIGHT);
                }
            }
            if (!player.IsDead)
            {
                if (warpTicks > 60 - 32)
                {
                    graphics.EnableAddBlend();
                    for (int i = 0; i < (warpTicks - (60 - 32)) * 8; i++)
                    {
                        int drawLen = (warpTicks - 32) * 16;
                        int drawX = drawRandom.Next(FIELD_WIDTH);
                        int drawY = drawRandom.Next(FIELD_HEIGHT + drawLen * 2) - drawLen;
                        switch (i % 3)
                        {
                            case 0:
                                graphics.SetColor(255, 255, 0, 0);
                                break;
                            case 1:
                                graphics.SetColor(255, 0, 255, 0);
                                break;
                            case 2:
                                graphics.SetColor(255, 0, 0, 255);
                                break;
                        }
                        graphics.DrawRectangle(drawX, drawY, 1, drawLen);
                    }
                    graphics.DisableAddBlend();

                    int alpha = (warpTicks - (60 - 32)) * 8;
                    if (alpha > 255)
                    {
                        alpha = 255;
                    }
                    graphics.SetColor(alpha, 255, 255, 255);
                    graphics.DrawRectangle(0, 0, FIELD_WIDTH, FIELD_HEIGHT);
                }
            }
            if (player.IsDead && !gameOver)
            {
                graphics.SetColor(255, 255, 0, 0);
                DrawStringCenter(graphics, "MISS");
            }
            else if (warping)
            {
                DrawClear(graphics);
            }
            else if (ticks < 60)
            {
                DrawLevel(graphics);
            }
            DrawStatus(graphics);
        }

        public virtual void DrawBackground(IGraphics graphics)
        {
            graphics.SetColor(255, 0, 0, 0);
            graphics.DrawRectangle(0, 0, FIELD_WIDTH, FIELD_HEIGHT);
        }

        private void DrawStringCenter(IGraphics graphics, string s)
        {
            graphics.DrawString(s, (FIELD_WIDTH - s.Length * 16) / 2, (FIELD_HEIGHT - 16) / 2);
        }

        private void DrawLevel(IGraphics graphics)
        {
            if (Level < 10)
            {
                graphics.SetColor(255, 255, 255, 255);
                graphics.DrawString("LEVEL", (FIELD_WIDTH - 7 * 16) / 2, (FIELD_HEIGHT - 16) / 2);
                graphics.SetColor(255, 0, 255, 0);
                graphics.DrawString(Level.ToString(), (FIELD_WIDTH - 7 * 16) / 2 + 6 * 16, (FIELD_HEIGHT - 16) / 2);
            }
            else
            {
                graphics.SetColor(255, 255, 255, 255);
                graphics.DrawString("LEVEL", (FIELD_WIDTH - 8 * 16) / 2, (FIELD_HEIGHT - 16) / 2);
                graphics.SetColor(255, 0, 255, 0);
                graphics.DrawString(Level.ToString(), (FIELD_WIDTH - 8 * 16) / 2 + 6 * 16, (FIELD_HEIGHT - 16) / 2);
            }
        }

        private void DrawClear(IGraphics graphics)
        {
            graphics.SetColor(255, 0, 0, 255);
            {
                string s = "CLEAR";
                graphics.DrawString(s, (FIELD_WIDTH - s.Length * 16) / 2, (FIELD_HEIGHT - 16) / 2 - 16);
            }
            graphics.SetColor(255, 255, 255, 255);
            {
                string s = (tension * 50) + " BONUS POINTS";
                graphics.DrawString(s, (FIELD_WIDTH - s.Length * 16) / 2, (FIELD_HEIGHT - 16) / 2 + 16);
            }
        }

        public void DrawStatus(IGraphics graphics)
        {
            graphics.SetColor(255, 128, 192, 255);
            graphics.DrawRectangle(480, 0, 160, 480);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawImage(Image.Hud, FIELD_WIDTH + 16 + 2, 32 + 8 + 4, 128, 64, 2, 0);
            graphics.DrawImage(Image.Hud, FIELD_WIDTH + 16 - 2, 32 + 8 - 4, 128, 64, 1, 0);
            graphics.SetColor(255, 255, 255, 255);
            graphics.DrawString("TOP SCORE", (640 - FIELD_WIDTH - 9 * 16) / 2 + FIELD_WIDTH, 128 + 8);
            {
                string s = highScore.ToString();
                graphics.DrawString(s, (640 - FIELD_WIDTH - s.Length * 16) / 2 + FIELD_WIDTH, 160 + 8);
            }
            graphics.DrawString("SCORE", (640 - FIELD_WIDTH - 5 * 16) / 2 + FIELD_WIDTH, 208 + 8);
            {
                string s = score.ToString();
                graphics.DrawString(s, (640 - FIELD_WIDTH - s.Length * 16) / 2 + FIELD_WIDTH, 240 + 8);
            }
            graphics.DrawString("LEVEL", (640 - FIELD_WIDTH - 5 * 16) / 2 + FIELD_WIDTH, 288 + 8);
            {
                string s = Level.ToString();
                graphics.DrawString(s, (640 - FIELD_WIDTH - s.Length * 16) / 2 + FIELD_WIDTH, 320 + 8);
            }
            graphics.DrawString("LEFT", (640 - FIELD_WIDTH - 4 * 16) / 2 + FIELD_WIDTH, 368 + 8);
            {
                int drawX = (640 - FIELD_WIDTH - (left * 40 - 8)) / 2 + FIELD_WIDTH;
                for (int i = 0; i < left; i++)
                {
                    graphics.DrawImage(Image.Player, drawX + i * 40, 400 + 8, 32, 32, 0, 0);
                }
            }
        }

        public void AddPlayerBullet(Bullet bullet)
        {
            playerBulletList.Add(bullet);
        }

        public void AddEnemy(Enemy enemy)
        {
            enemyList.Add(enemy);
        }

        public void AddEnemyInGame(Enemy enemy)
        {
            enemyAddList.Add(enemy);
        }

        public void AddEnemyBullet(Bullet bullet)
        {
            enemyBulletList.Add(bullet);
        }

        public void AddEffect(Effect effect)
        {
            effectList.Add(effect);
        }

        public int CoutAliveEnemies()
        {
            int count = 0;
            foreach (Enemy enemy in enemyList)
            {
                if (!enemy.IsDead)
                {
                    count++;
                }
            }
            return count;
        }

        public void PlaySound(Sound sound)
        {
            sounds.Add(sound);
        }

        private void ClearSounds()
        {
            sounds.Clear();
        }

        public virtual int Level
        {
            get
            {
                return 0;
            }
        }

        public static int FieldWidth
        {
            get
            {
                return FIELD_WIDTH;
            }
        }

        public static int FieldHeight
        {
            get
            {
                return FIELD_HEIGHT;
            }
        }

        public Player Player
        {
            get
            {
                return player;
            }
        }

        public List<Bullet> PlayerBulletList
        {
            get
            {
                return playerBulletList;
            }
        }

        public List<Enemy> EnemyList
        {
            get
            {
                return enemyList;
            }
        }

        public List<Bullet> EnemyBulletList
        {
            get
            {
                return enemyBulletList;
            }
        }

        public Random Random
        {
            get
            {
                return random;
            }
        }

        public int Ticks
        {
            get
            {
                return ticks;
            }
        }

        public virtual bool GameOver
        {
            get
            {
                return gameOver;
            }
        }

        public virtual bool EndingGameOver
        {
            get
            {
                return false;
            }
        }

        public virtual bool GoingToNextStage
        {
            get
            {
                return goingToNextStage;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public double BackgroundStretch
        {
            get
            {
                if (warpTicks <= 36)
                {
                    return 1;
                }
                else
                {
                    return warpTicks - 36;
                }
            }
        }

        public bool ReturnToTitle
        {
            get
            {
                return returnToTitle;
            }
        }

        public IEnumerable<Sound> CurrentSounds
        {
            get
            {
                return sounds;
            }
        }

        public virtual bool IsBossStage
        {
            get
            {
                return false;
            }
        }

        public static Game CreateGame(Random random, int level, int highScore, int score, int left)
        {
            switch (level)
            {
                case 1:
                    return new Level1(random, highScore, score, left);
                case 2:
                    return new Level2(random, highScore, score, left);
                case 3:
                    return new Level3(random, highScore, score, left);
                case 4:
                    return new Level4(random, highScore, score, left);
                case 5:
                    return new Level5(random, highScore, score, left);
                case 6:
                    return new Level6(random, highScore, score, left);
                case 7:
                    return new Level7(random, highScore, score, left);
                case 8:
                    return new Level8(random, highScore, score, left);
                case 9:
                    return new Level9(random, highScore, score, left);
                case 10:
                    return new Level10(random, highScore, score, left);
                case 11:
                    return new Level11(random, highScore, score, left);
                case 12:
                    return new Level12(random, highScore, score, left);
                case 13:
                    return new Level13(random, highScore, score, left);
                case 14:
                    return new Level14(random, highScore, score, left);
                case 15:
                    return new Level15(random, highScore, score, left);
                case 16:
                    return new Level16(random, highScore, score, left);
                case 17:
                    return new Level17(random, highScore, score, left);
                case 18:
                    return new Level18(random, highScore, score, left);
                case 19:
                    return new Level19(random, highScore, score, left);
                case 20:
                    return new Level20(random, highScore, score, left);
                case 21:
                    return new Level21(random, highScore, score, left);
                case 22:
                    return new Level22(random, highScore, score, left);
                case 23:
                    return new Level23(random, highScore, score, left);
                case 24:
                    return new Level24(random, highScore, score, left);
                case 25:
                    return new Level25(random, highScore, score, left);
                default:
                    return new Game(random, highScore, score, left);
            }
        }
    }
}
