using System;

namespace MiswGame2008
{
    public interface IAudio
    {
        void PlaySound(Sound sound);
        void PlayMusic(Music music);
        void StopMusic();
    }
}
