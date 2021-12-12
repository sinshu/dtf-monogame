using System;
using Microsoft.Xna.Framework.Input;

namespace MiswGame2008
{
    public class SdlInput : IInput, IDisposable
    {
        private MiswGame2008 app;

        private KeyboardState curr;
        private KeyboardState prev;

        public SdlInput(MiswGame2008 app)
        {
            this.app = app;
        }

        public void Update()
        {
            prev = curr;
            curr = Keyboard.GetState();
        }

        public void Dispose()
        {
        }

        private bool IsPress(Keys key)
        {
            return curr[key] == KeyState.Down;
        }

        private bool IsPush(Keys key)
        {
            return curr[key] == KeyState.Down && prev[key] == KeyState.Up;
        }

        public bool IsPressLeft
        {
            get
            {
                return IsPress(Keys.Left);
            }
        }

        public bool IsPressUp
        {
            get
            {
                return IsPress(Keys.Up);
            }
        }

        public bool IsPressRight
        {
            get
            {
                return IsPress(Keys.Right);
            }
        }

        public bool IsPressDown
        {
            get
            {
                return IsPress(Keys.Down);
            }
        }

        public bool IsPressButton1
        {
            get
            {
                return IsPress(Keys.A)
                    || IsPress(Keys.Z)
                    || IsPress(Keys.Space)
                    || IsPress(Keys.LeftControl)
                    || IsPress(Keys.RightControl)
                    || IsJoyStickButtonPress();
            }
        }

        public bool IsPressButton2
        {
            get
            {
                return IsPress(Keys.S)
                    || IsPress(Keys.X)
                    || IsPress(Keys.Enter)
                    || IsPress(Keys.LeftShift)
                    || IsPress(Keys.RightShift);
            }
        }

        public bool IsPushLeft
        {
            get
            {
                return IsPush(Keys.Left);
            }
        }

        public bool IsPushUp
        {
            get
            {
                return IsPush(Keys.Up);
            }
        }

        public bool IsPushRight
        {
            get
            {
                return IsPush(Keys.Right);
            }
        }

        public bool IsPushDown
        {
            get
            {
                return IsPush(Keys.Down);
            }
        }

        public bool IsPushButton1
        {
            get
            {
                return IsPush(Keys.A)
                    || IsPush(Keys.Z)
                    || IsPush(Keys.Space)
                    || IsPush(Keys.LeftControl)
                    || IsPush(Keys.RightControl)
                    || IsJoyStickButtonPush();
            }
        }

        public bool IsPushButton2
        {
            get
            {
                return IsPush(Keys.S)
                    || IsPush(Keys.X)
                    || IsPush(Keys.Enter)
                    || IsPush(Keys.LeftShift)
                    || IsPush(Keys.RightShift);
            }
        }

        public UserCommand UserCommand
        {
            get
            {
                return new UserCommand(
                    IsPushLeft,
                    IsPushUp,
                    IsPushRight,
                    IsPushDown,
                    IsPushButton1,
                    IsPushButton2,
                    IsPush(Keys.Escape));
            }
        }

        public GameCommand GameCommand
        {
            get
            {
                return new GameCommand(
                    IsPressLeft,
                    IsPressUp,
                    IsPressRight,
                    IsPressDown,
                    IsPressButton1,
                    IsPressButton2,
                    IsPush(Keys.Escape));
            }
        }

        private bool IsJoyStickButtonPress()
        {
            /*
            for (int i = 4; i < joyStick.ButtonNum; i++)
            {
                if (joyStick.IsPress(i))
                {
                    return true;
                }
            }
            */

            return false;
        }

        private bool IsJoyStickButtonPush()
        {
            /*
            for (int i = 4; i < joyStick.ButtonNum; i++)
            {
                if (joyStick.IsPush(i))
                {
                    return true;
                }
            }
            */

            return false;
        }
    }
}
