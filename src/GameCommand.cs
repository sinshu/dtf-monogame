using System;

namespace MiswGame2008
{
    public struct GameCommand
    {
        private bool left;
        private bool up;
        private bool right;
        private bool down;
        private bool button1;
        private bool button2;
        private bool exit;

        public GameCommand(bool left, bool up, bool right, bool down, bool button1, bool button2, bool exit)
        {
            this.left = left;
            this.right = right;
            this.up = up;
            this.down = down;
            this.button1 = button1;
            this.button2 = button2;
            this.exit = exit;
        }

        public static GameCommand Empty
        {
            get
            {
                return new GameCommand(false, false, false, false, false, false, false);
            }
        }

        public bool Left
        {
            get
            {
                return left;
            }
        }

        public bool Up
        {
            get
            {
                return up;
            }
        }

        public bool Right
        {
            get
            {
                return right;
            }
        }

        public bool Down
        {
            get
            {
                return down;
            }
        }

        public bool Button1
        {
            get
            {
                return button1;
            }
        }

        public bool Button2
        {
            get
            {
                return button2;
            }
        }

        public bool Exit
        {
            get
            {
                return exit;
            }
        }
    }
}
