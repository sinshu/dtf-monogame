using System;

namespace MiswGame2008
{
    public interface IInput
    {
        bool IsPressLeft { get; }
        bool IsPressUp { get; }
        bool IsPressRight { get; }
        bool IsPressDown { get; }
        bool IsPressButton1 { get; }
        bool IsPressButton2 { get; }
        bool IsPushLeft { get; }
        bool IsPushUp { get; }
        bool IsPushRight { get; }
        bool IsPushDown { get; }
        bool IsPushButton1 { get; }
        bool IsPushButton2 { get; }
        UserCommand UserCommand { get; }
        GameCommand GameCommand { get; }
    }
}
