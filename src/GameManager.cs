using System;
using System.IO;

namespace MiswGame2008
{
    public class GameManager
    {
        public static int INIT_PLAYER_LEFT = 4;

        private GameManagerStatus status;
        private Random random;
        private Title title;
        private Game game;
        private GameOver gameOver;
        private Ranking ranking;
        private int level;
        private int currentScore;
        private int playerLeft;
        private int bossEndCount;

        private TopPlayerInfo[] top10Players;

        private int startLevel;

        private bool exiting;

        private IAudio audio;

        public GameManager(IAudio audio, int startLevel)
        {
            status = GameManagerStatus.None;
            random = new Random();
            title = null;
            game = null;
            gameOver = null;
            ranking = null;
            level = 0;
            currentScore = 0;
            playerLeft = 0;
            bossEndCount = 0;
            top10Players = new TopPlayerInfo[10];

            this.startLevel = startLevel;

            for (int i = 0; i < 10; i++)
            {
                top10Players[i] = new TopPlayerInfo((10 - i) * 1000, 10 - i, "_NONAME_");
            }
            exiting = false;

            this.audio = audio;
        }

        public void Update(IInput input)
        {
            switch (status)
            {
                case GameManagerStatus.None:
                    title = new Title(random);
                    status = GameManagerStatus.Title;
                    audio.PlayMusic(Music.Title);
                    break;
                case GameManagerStatus.Title:
                    title.Update(input.UserCommand);
                    if (title.ExitGame)
                    {
                        exiting = true;
                    }
                    else if (title.StartGame)
                    {
                        title = null;
                        level = startLevel;
                        currentScore = 0;
                        playerLeft = INIT_PLAYER_LEFT;
                        game = Game.CreateGame(random, level, top10Players[0].Score, currentScore, playerLeft);
                        status = GameManagerStatus.Game;
                        if (!game.IsBossStage)
                        {
                            audio.PlayMusic(Music.Game);
                        }
                        else
                        {
                            audio.PlayMusic(Music.Boss);
                        }
                    }
                    else if (title.Ranking)
                    {
                        title = null;
                        ranking = new Ranking(top10Players, random);
                        status = GameManagerStatus.Ranking;
                    }
                    break;
                case GameManagerStatus.Game:
                    game.Update(input.GameCommand);
                    foreach (Sound sound in game.CurrentSounds)
                    {
                        audio.PlaySound(sound);
                    }
                    if (game.ReturnToTitle)
                    {
                        game = null;
                        title = new Title(random);
                        status = GameManagerStatus.Title;
                        audio.PlayMusic(Music.Title);
                    }
                    else if (game.GameOver)
                    {
                        if (playerLeft > 0)
                        {
                            currentScore = game.Score;
                            playerLeft--;
                            game = Game.CreateGame(random, level, top10Players[0].Score, currentScore, playerLeft);
                        }
                        else
                        {
                            currentScore = game.Score;
                            gameOver = new GameOver(currentScore > top10Players[9].Score, false);
                            status = GameManagerStatus.GameOver;
                            if (currentScore > top10Players[9].Score)
                            {
                                audio.PlayMusic(Music.NameEntry);
                            }
                            else
                            {
                                audio.StopMusic();
                            }
                        }
                    }
                    else if (game.GoingToNextStage)
                    {
                        if (!game.IsBossStage)
                        {
                            currentScore = game.Score;
                            level++;
                            game = Game.CreateGame(random, level, top10Players[0].Score, currentScore, playerLeft);
                            if (game.IsBossStage)
                            {
                                audio.PlayMusic(Music.Boss);
                            }
                        }
                        else
                        {
                            bossEndCount = 0;
                            status = GameManagerStatus.BossEnd;
                            audio.StopMusic();
                        }
                    }
                    else if (game.EndingGameOver)
                    {
                        currentScore = game.Score;
                        gameOver = new GameOver(currentScore > top10Players[9].Score, true);
                        status = GameManagerStatus.GameOver;
                        if (currentScore > top10Players[9].Score)
                        {
                            audio.PlayMusic(Music.NameEntry);
                        }
                        else
                        {
                            audio.StopMusic();
                        }
                    }
                    break;
                case GameManagerStatus.BossEnd:
                    if (bossEndCount < 30)
                    {
                        bossEndCount++;
                    }
                    if (bossEndCount == 30)
                    {
                        currentScore = game.Score;
                        level++;
                        game = Game.CreateGame(random, level, top10Players[0].Score, currentScore, playerLeft);
                        status = GameManagerStatus.Game;
                        audio.PlayMusic(Music.Game);
                    }
                    break;
                case GameManagerStatus.GameOver:
                    game.Update(GameCommand.Empty);
                    gameOver.Update(input.UserCommand);
                    foreach (Sound sound in gameOver.CurrentSounds)
                    {
                        audio.PlaySound(sound);
                    }
                    if (gameOver.ReturnToTitle)
                    {
                        if (currentScore > top10Players[9].Score)
                        {
                            AddNewTopPlayer(new TopPlayerInfo(currentScore, level, gameOver.Name));
                        }
                        game = null;
                        gameOver = null;
                        ranking = new Ranking(top10Players, random);
                        status = GameManagerStatus.Ranking;
                        audio.StopMusic();
                    }
                    break;
                case GameManagerStatus.Ranking:
                    ranking.Update(input.UserCommand);
                    if (ranking.ReturnToTitle)
                    {
                        ranking = null;
                        title = new Title(random);
                        status = GameManagerStatus.Title;
                        audio.PlayMusic(Music.Title);
                    }
                    break;
            }
        }

        public void Draw(IGraphics graphics)
        {
            switch (status)
            {
                case GameManagerStatus.Title:
                    title.Draw(graphics);
                    break;
                case GameManagerStatus.Game:
                    game.Draw(graphics);
                    break;
                case GameManagerStatus.BossEnd:
                    graphics.SetColor(255, 255, 255, 255);
                    graphics.DrawRectangle(0, 0, Game.FieldWidth, Game.FieldHeight);
                    break;
                case GameManagerStatus.GameOver:
                    game.Draw(graphics);
                    gameOver.Draw(graphics);
                    break;
                case GameManagerStatus.Ranking:
                    ranking.Draw(graphics);
                    break;
            }
        }

        private void AddNewTopPlayer(TopPlayerInfo player)
        {
            for (int i = 0; i < 10; i++)
            {
                if (player.Score > top10Players[i].Score)
                {
                    for (int j = 8; j >= i; j--)
                    {
                        top10Players[j + 1] = top10Players[j];
                    }
                    top10Players[i] = player;
                    return;
                }
            }
        }

        public void LoadScoreDataFromFile(string path)
        {
            StreamReader reader;
            try
            {
                reader = new StreamReader(path);
            }
            catch (IOException)
            {
                Console.WriteLine("Faild to read high scores!");
                return;
            }
            TopPlayerInfo[] data;
            try
            {
                data = new TopPlayerInfo[10];
                for (int i = 0; i < 10; i++)
                {
                    string[] rows = reader.ReadLine().Split(',');
                    data[i] = new TopPlayerInfo(int.Parse(rows[0]), int.Parse(rows[1]), rows[2]);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid high score data!");
                reader.Close();
                return;
            }
            reader.Close();
            top10Players = data;
            Console.WriteLine("Loaded the high scores.");
        }

        public void SaveScoreDataToFile(string path)
        {
            StreamWriter writer;
            try
            {
                writer = new StreamWriter(path);
            }
            catch (IOException)
            {
                Console.WriteLine("Faild to write the high score data!");
                return;
            }
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    TopPlayerInfo info = top10Players[i];
                    writer.WriteLine(info.Score + "," + info.Level + "," + info.Name);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Faild to write the high score data!");
                writer.Close();
                return;
            }
            writer.Close();
            Console.WriteLine("Saved the high scores.");
        }

        public bool Exiting
        {
            get
            {
                return exiting;
            }
        }
    }
}
