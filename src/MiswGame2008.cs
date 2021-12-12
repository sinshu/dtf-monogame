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
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

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
            manager.Update(input);
        }

        protected override void Draw(GameTime gameTime)
        {
            video.Begin();
            manager.Draw(video);
            video.End();
        }
    }
}
