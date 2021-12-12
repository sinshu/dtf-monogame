using System;
using System.Collections.Generic;

namespace MiswGame2008
{
    public class GameOver
    {
        private static string CHARACTERS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ$_<>";

        private bool nameEntry;
        private bool ending;
        private bool returnToTitle;

        private int ticks;

        private string name;

        private int selectedRow;
        private int selectedCol;
        private double selectorX;
        private double selectorY;

        private bool nameEntryFinished;
        private int nameEntryEndTicks;

        private List<Sound> sounds;

        public GameOver(bool nameEntry, bool ending)
        {
            this.nameEntry = nameEntry;
            this.ending = ending;
            returnToTitle = false;
            ticks = 0;
            name = "";
            selectedRow = 0;
            selectedCol = 0;
            selectorX = (Game.FieldWidth - (32 * 10 - 16)) / 2;
            selectorY = 176;
            nameEntryFinished = false;
            nameEntryEndTicks = 0;

            sounds = new List<Sound>();
        }

        public void Update(UserCommand command)
        {
            ClearSounds();

            if (nameEntry)
            {
                if (!nameEntryFinished)
                {
                    if (command.Left != command.Right)
                    {
                        if (command.Left)
                        {
                            if (selectedCol > 0)
                            {
                                selectedCol--;
                                PlaySound(Sound.CursorMove);
                            }
                        }
                        if (command.Right)
                        {
                            if (selectedCol < 9)
                            {
                                selectedCol++;
                                PlaySound(Sound.CursorMove);
                            }
                        }
                    }
                    if (command.Up != command.Down)
                    {
                        if (command.Up)
                        {
                            if (selectedRow > 0)
                            {
                                selectedRow--;
                                PlaySound(Sound.CursorMove);
                            }
                        }
                        if (command.Down)
                        {
                            if (selectedRow < 3)
                            {
                                selectedRow++;
                                PlaySound(Sound.CursorMove);
                            }
                        }
                    }
                    if (command.Button1)
                    {
                        if (selectedRow == 3)
                        {
                            if (selectedCol == 8)
                            {
                                if (name.Length > 0)
                                {
                                    name = name.Substring(0, name.Length - 1);
                                    PlaySound(Sound.Select);
                                }
                            }
                            else if (selectedCol == 9)
                            {
                                nameEntryFinished = true;
                                PlaySound(Sound.Enter);
                            }
                            else
                            {
                                if (name.Length < 8)
                                {
                                    name += CHARACTERS[selectedRow * 10 + selectedCol];
                                    PlaySound(Sound.Select);
                                }
                                else
                                {
                                    selectedRow = 3;
                                    selectedCol = 9;
                                }
                            }
                        }
                        else
                        {
                            if (name.Length < 8)
                            {
                                name += CHARACTERS[selectedRow * 10 + selectedCol];
                                PlaySound(Sound.Select);
                            }
                            else
                            {
                                selectedRow = 3;
                                selectedCol = 9;
                            }
                        }
                    }
                }
                selectorX = ((Game.FieldWidth - (32 * 10 - 16)) / 2 + selectedCol * 32) * 0.5 + selectorX * 0.5;
                selectorY = (176 + selectedRow * 32) * 0.5 + selectorY * 0.5;
            }
            else
            {
                if (command.Button1)
                {
                    returnToTitle = true;
                }
            }
            ticks++;
            if (nameEntryFinished)
            {
                if (nameEntryEndTicks < 120)
                {
                    if (nameEntryEndTicks >= 30)
                    {
                        if (command.Button1)
                        {
                            returnToTitle = true;
                        }
                    }
                    nameEntryEndTicks++;
                }
                else
                {
                    returnToTitle = true;
                }
            }
            if (!nameEntry)
            {
                if (ticks >= 90)
                {
                    returnToTitle = true;
                }
            }
        }

        public void Draw(IGraphics graphics)
        {
            graphics.SetColor(128, 0, 0, 0);
            graphics.DrawRectangle(0, 0, Game.FieldWidth, Game.FieldHeight);
            if (nameEntry)
            {
                if (nameEntryEndTicks <= 30)
                {
                    if (!nameEntryFinished)
                    {
                        int drawX = (int)Math.Round(selectorX) - 8;
                        int drawY = (int)Math.Round(selectorY) - 8 + 8;
                        graphics.SetColor(255, 0, 255, 0);
                        graphics.DrawImage(Image.Hud, drawX, drawY, 32, 32, 1, 7);
                    }
                    graphics.SetColor(255, 255, 255, 255);
                    graphics.DrawString("NAME ENTRY", (Game.FieldWidth - 16 * 10) / 2, 128 + 8);
                    for (int row = 0; row < 4; row++)
                    {
                        int drawY = 176 + row * 32;
                        for (int col = 0; col < 10; col++)
                        {
                            if (!nameEntryFinished)
                            {
                                if (row == selectedRow && col == selectedCol)
                                {
                                    graphics.SetColor(255, 0, 255, 0);
                                }
                                else
                                {
                                    graphics.SetColor(255, 255, 255, 255);
                                }
                            }
                            else
                            {
                                graphics.SetColor(255, 255, 255, 255);
                            }
                            int drawX = (Game.FieldWidth - (32 * 10 - 16)) / 2 + col * 32;
                            graphics.DrawCharacter(CHARACTERS[row * 10 + col], drawX, drawY + 8);
                        }
                    }
                    if (!nameEntryFinished)
                    {
                        graphics.SetColor(255, 0, 255, 0);
                        graphics.DrawString(name, (Game.FieldWidth - name.Length * 16) / 2 - 8, 320 + 8);
                        if (ticks % 2 == 0)
                        {
                            graphics.SetColor(255, 0, 255, 0);
                            graphics.DrawImage(Image.Hud, (Game.FieldWidth - name.Length * 16) / 2 - 8 + name.Length * 16, 320 + 8, 16, 16, 3, 2);
                        }
                    }
                    else
                    {
                        graphics.SetColor(255, 255, 255, 255);
                        graphics.DrawString(name, (Game.FieldWidth - name.Length * 16) / 2, 320 + 8);
                    }
                }
                else
                {
                    if (!ending)
                    {
                        graphics.SetColor(255, 255, 0, 0);
                        graphics.DrawString("GAME OVER", (Game.FieldWidth - 16 * 9) / 2, (Game.FieldHeight - 16) / 2);
                    }
                    else
                    {
                        graphics.SetColor(255, 255, 255, 255);
                        graphics.DrawString("THE END", (Game.FieldWidth - 16 * 7) / 2, (Game.FieldHeight - 16) / 2);
                    }
                }
            }
            else
            {
                if (!ending)
                {
                    graphics.SetColor(255, 255, 0, 0);
                    graphics.DrawString("GAME OVER", (Game.FieldWidth - 16 * 9) / 2, (Game.FieldHeight - 16) / 2);
                }
                else
                {
                    graphics.SetColor(255, 255, 255, 255);
                    graphics.DrawString("THE END", (Game.FieldWidth - 16 * 7) / 2, (Game.FieldHeight - 16) / 2);
                }
            }
        }

        public void PlaySound(Sound sound)
        {
            sounds.Add(sound);
        }

        private void ClearSounds()
        {
            sounds.Clear();
        }

        public bool ReturnToTitle
        {
            get
            {
                return returnToTitle;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public IEnumerable<Sound> CurrentSounds
        {
            get
            {
                return sounds;
            }
        }
    }
}
