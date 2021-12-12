using System;
using Microsoft.Xna.Framework;

namespace MiswGame2008
{
    public class MiswGame2008 : Microsoft.Xna.Framework.Game
    {
        private int startLevel;

        private GraphicsDeviceManager graphics;

        private SdlGraphics video;
        private SdlInput input;
        private SdlAudio audio;

        private GameManager manager;

        public MiswGame2008(int startLevel)
        {
            this.startLevel = startLevel;

            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            var display = graphics.GraphicsDevice.DisplayMode;

            graphics.PreferredBackBufferWidth = display.Width;
            graphics.PreferredBackBufferHeight = display.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 30);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            video = new SdlGraphics(this);
            input = new SdlInput(this);
            audio = new SdlAudio(this);

            manager = new GameManager(audio, startLevel);
            manager.LoadScoreDataFromFile("score.dat");

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            manager.SaveScoreDataToFile("score.dat");

            if (input != null)
            {
                input.Dispose();
                input = null;
            }

            if (audio != null)
            {
                audio.Dispose();
                audio = null;
            }

            if (video != null)
            {
                video.Dispose();
                video = null;
            }

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update();
            manager.Update(input);

            if (manager.Exiting)
            {
                Exit();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            video.Begin();
            manager.Draw(video);
            video.End();
        }
    }
}
