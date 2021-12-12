using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiswGame2008
{
    public class SdlGraphics : IGraphics, IDisposable
    {
        private MiswGame2008 app;

        private Texture2D[] textures;
        private Texture2D singlePixelWhite;
        private RenderTarget2D renderTarget;
        private SpriteBatch sprite;

        private Color color;

        public SdlGraphics(MiswGame2008 app)
        {
            this.app = app;

            Initialize();
        }

        private void Initialize()
        {
            textures = new Texture2D[Utility.GetEnumCount(typeof(Image))];
            for (int i = 0; i < textures.Length; i++)
            {
                string path = Path.Combine("gamedata", "images", Enum.GetName(typeof(Image), i) + ".png");
                Console.Write("Load image '" + path + "' ... ");
                textures[i] = LoadTextureByPath(path);
                Console.WriteLine("OK");
            }

            singlePixelWhite = new Texture2D(app.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            singlePixelWhite.SetData(new uint[] { 0xFFFFFFFF });

            renderTarget = new RenderTarget2D(app.GraphicsDevice, 640, 480);

            sprite = new SpriteBatch(app.GraphicsDevice);

            color = Color.White;
        }

        private Texture2D LoadTextureByPath(string path)
        {
            return Texture2D.FromFile(app.GraphicsDevice, path);
        }

        public void Begin()
        {
            app.GraphicsDevice.SetRenderTarget(renderTarget);

            sprite.Begin(SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                null);
        }

        public void End()
        {
            sprite.End();

            app.GraphicsDevice.SetRenderTarget(null);

            sprite.Begin(SpriteSortMode.Immediate,
                BlendState.Opaque,
                SamplerState.PointClamp,
                null,
                null,
                null,
                null);

            var windowWidth = app.GraphicsDevice.PresentationParameters.BackBufferWidth;
            var windowHeight = app.GraphicsDevice.PresentationParameters.BackBufferHeight;

            sprite.Draw(
                renderTarget,
                new Rectangle(0, 0, windowWidth, windowHeight),
                Color.White);

            sprite.End();
        }

        public void EnableAddBlend()
        {
            sprite.End();
            sprite.Begin(SpriteSortMode.Immediate,
                BlendState.Additive,
                SamplerState.PointClamp,
                null,
                null,
                null,
                null);
        }

        public void DisableAddBlend()
        {
            sprite.End();
            sprite.Begin(SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                null);
        }

        public void SetColor(int a, int r, int g, int b)
        {
            color = new Color(r, g, b) * (a / 255F);
        }

        public void Fill()
        {
            sprite.Draw(
                singlePixelWhite,
                new Rectangle(0, 0, 640, 480),
                color);
        }

        public void DrawRectangle(int x, int y, int width, int height)
        {
            sprite.Draw(
                singlePixelWhite,
                new Rectangle(x, y, width, height),
                new Rectangle(0, 0, 1, 1),
                color);
        }

        public void DrawImage(Image image, int x, int y)
        {
            sprite.Draw(textures[(int)image], new Vector2(x, y), color);
        }

        public void DrawImage(Image image, int x, int y, int width, int height, int row, int col)
        {
            sprite.Draw(
                textures[(int)image],
                new Rectangle(x, y, width, height),
                new Rectangle(width * col, height * row, width, height),
                color);
        }

        public void DrawImage(Image image, int x, int y, int width, int height, int row, int col, int deg)
        {
            /*
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            int yaneRad = -(int)Math.Round((double)(deg * 512) / 360);
            screen.BltRotate(textures[(int)image], x, y, srcRect, yaneRad, 1.0f, width / 2, height / 2);
            */

            sprite.Draw(
                textures[(int)image],
                new Rectangle(x + width / 2, y + height / 2, width, height),
                new Rectangle(width * col, height * row, width, height),
                color,
                -MathF.PI * deg / 180F,
                new Vector2(width / 2, height / 2),
                SpriteEffects.None,
                0);
        }

        public void DrawImage(Image image, int x, int y, int width, int height, int row, int col, int deg, double stretch)
        {
            /*
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            int yaneRad = -(int)Math.Round((double)(deg * 512) / 360);
            screen.BltRotate(textures[(int)image], x, y, srcRect, yaneRad, (float)stretch, 0, 0);
            */

            sprite.Draw(
                textures[(int)image],
                new Rectangle(x + width / 2, y + height / 2, width, height),
                new Rectangle(width * col, height * row, width, height),
                color,
                -MathF.PI * deg / 180F,
                new Vector2(width / 2, height / 2),
                SpriteEffects.None,
                0);
        }

        public void DrawObject(Image image, int x, int y, int width, int height, int row, int col, int deg)
        {
            /*
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            int yaneRad = -(int)Math.Round((double)(deg * 512) / 360);
            screen.BltRotate(textures[(int)image], x - width / 2, y - height / 2, srcRect, yaneRad, 1.0f, width / 2, height / 2);
            */

            sprite.Draw(
                textures[(int)image],
                new Rectangle(x, y, width, height),
                new Rectangle(width * col, height * row, width, height),
                color,
                -MathF.PI * deg / 180F,
                new Vector2(width / 2, height / 2),
                SpriteEffects.None,
                0);
        }

        public void DrawBullet(Image image, int x, int y, int width, int height, int row, int col, int deg)
        {
            /*
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            int yaneRad = -(int)Math.Round((double)(deg * 512) / 360);
            screen.BltRotate(textures[(int)image], x - width, y - height / 2, srcRect, yaneRad, 1.0f, width, height / 2);
            */

            sprite.Draw(
                textures[(int)image],
                new Rectangle(x, y, width, height),
                new Rectangle(width * col, height * row, width, height),
                color,
                -MathF.PI * deg / 180F,
                new Vector2(width, height / 2),
                SpriteEffects.None,
                0);
        }

        public void DrawBackground(Image image, int x, int y, int width, int height, int row, int col, double stretch)
        {
            /*
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            Point[] points = new Point[4];
            points[0] = new Point((float)x, (float)(y * stretch));
            points[1] = new Point((float)(x + width), (float)(y * stretch));
            points[2] = new Point((float)(x + width), (float)((y + height) * stretch));
            points[3] = new Point((float)x, (float)((y + height) * stretch));
            screen.Blt(textures[(int)image], srcRect, points);
            */

            sprite.Draw(
                textures[(int)image],
                new Rectangle(x, (int)(y * stretch), width, (int)(height * stretch)),
                new Rectangle(width * col, height * row, width, height),
                color);
        }

        public void DrawCharacter(char c, int x, int y)
        {
            /*
            ITexture texture = textures[(int)Image.Hud];
            int u, v;
            if ('0' <= c && c <= '9')
            {
                u = (c - '0') * 16;
                v = 0;
            }
            else if ('A' <= c && c <= 'P')
            {
                u = (c - 'A') * 16;
                v = 16;
            }
            else if ('Q' <= c && c <= 'Z')
            {
                u = (c - 'Q') * 16;
                v = 32;
            }
            else
            {
                switch (c)
                {
                    case '#':
                        u = 160;
                        v = 0;
                        break;
                    case '$':
                        u = 160;
                        v = 32;
                        break;
                    case '_':
                        u = 176;
                        v = 32;
                        break;
                    case '<':
                        u = 0;
                        v = 48;
                        break;
                    case '>':
                        u = 16;
                        v = 48;
                        break;
                    default:
                        return;
                }
            }
            Rect srcRect = new Rect((float)u, (float)v, (float)(u + 16), (float)(v + 16));
            screen.Blt(texture, x, y, srcRect);
            */
        }

        public void DrawString(string s, int x, int y)
        {
            for (int i = 0; i < s.Length; i++)
            {
                DrawCharacter(s[i], x + i * 16, y);
            }
        }

        public void Dispose()
        {
            if (sprite != null)
            {
                sprite.Dispose();
                sprite = null;
            }

            if (renderTarget != null)
            {
                renderTarget.Dispose();
                renderTarget = null;
            }

            if (singlePixelWhite != null)
            {
                singlePixelWhite.Dispose();
                singlePixelWhite = null;
            }

            if (textures != null)
            {
                for (var i = 0; i < textures.Length; i++)
                {
                    if (textures[i] != null)
                    {
                        textures[i].Dispose();
                        textures[i] = null;
                    }
                }
            }
        }
    }
}
