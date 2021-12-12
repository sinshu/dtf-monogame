using System;

namespace MiswGame2008
{
    public interface IGraphics
    {
        void Begin();
        void End();
        void SetColor(int a, int r, int g, int b);
        void EnableAddBlend();
        void DisableAddBlend();
        void Fill();
        void DrawRectangle(int x, int y, int width, int height);
        void DrawImage(Image image, int x, int y);
        void DrawImage(Image image, int x, int y, int width, int height, int row, int col);
        void DrawImage(Image image, int x, int y, int width, int height, int row, int col, int deg);
        void DrawImage(Image image, int x, int y, int width, int height, int row, int col, int deg, double stretch);
        void DrawObject(Image image, int x, int y, int width, int height, int row, int col, int deg);
        void DrawBullet(Image image, int x, int y, int width, int height, int row, int col, int deg);
        void DrawBackground(Image image, int x, int y, int width, int height, int row, int col, double stretch);
        void DrawCharacter(char c, int x, int y);
        void DrawString(string s, int x, int y);
    }
}
