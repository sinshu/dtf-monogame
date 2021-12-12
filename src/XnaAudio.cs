using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Audio;
using NAudio.Vorbis;

namespace MiswGame2008
{
    public class XnaAudio : IAudio, IDisposable
    {
        private MiswGame2008 app;

        private SoundEffect[] sounds;
        private byte[][] musics;

        private DynamicSoundEffectInstance dynamicSound;
        private VorbisWaveReader oggStream;
        private byte[] floatBuffer;
        private byte[] shortBuffer;
        private Music currentMusic;

        public XnaAudio(MiswGame2008 app)
        {
            this.app = app;

            Initialize();
        }

        private void Initialize()
        {
            sounds = new SoundEffect[Utility.GetEnumCount(typeof(Sound))];
            for (int i = 0; i < sounds.Length; i++)
            {
                string path = Path.Combine("gamedata", "sounds", Enum.GetName(typeof(Sound), i) + ".wav");
                Console.Write("Load sound '" + path + "' ... ");
                sounds[i] = LoadSoundByPath(path);
                Console.WriteLine("OK");
            }

            musics = new byte[Utility.GetEnumCount(typeof(Music))][];
            for (int i = 0; i < musics.Length; i++)
            {
                string path = Path.Combine("gamedata", "musics", Enum.GetName(typeof(Music), i) + ".ogg");
                Console.Write("Load music '" + path + "' ... ");
                musics[i] = File.ReadAllBytes(path);
                Console.WriteLine("OK");
            }

            dynamicSound = new DynamicSoundEffectInstance(44100, AudioChannels.Mono);
            floatBuffer = new byte[4 * 4410];
            shortBuffer = new byte[2 * 4410];
            oggStream = null;
            currentMusic = (Music)(-1);

            dynamicSound.BufferNeeded += (sender, e) => SubmitBuffer();
        }

        private SoundEffect LoadSoundByPath(string path)
        {
            return SoundEffect.FromFile(path);
        }

        public void PlaySound(Sound sound)
        {
            sounds[(int)sound].Play();
        }

        public void PlayMusic(Music music)
        {
            if (currentMusic == music)
            {
                return;
            }

            StopMusic();

            oggStream = new VorbisWaveReader(new MemoryStream(musics[(int)music]));
            currentMusic = music;

            SubmitBuffer();
            dynamicSound.Play();
        }

        public void StopMusic()
        {
            if ((int)currentMusic == -1)
            {
                return;
            }
            else
            {
                dynamicSound.Stop();
                oggStream = null;
                currentMusic = (Music)(-1);
            }
        }

        private void SubmitBuffer()
        {
            if (oggStream != null)
            {
                Array.Clear(floatBuffer, 0, floatBuffer.Length);

                var read = oggStream.Read(floatBuffer);

                var fp = MemoryMarshal.Cast<byte, float>(floatBuffer);
                var sp = MemoryMarshal.Cast<byte, short>(shortBuffer);
                for (var i = 0; i < sp.Length; i++)
                {
                    var value = (int)(32768 * fp[i]);
                    if (value < short.MinValue) value = short.MinValue;
                    if (value > short.MaxValue) value = short.MaxValue;
                    sp[i] = (short)value;
                }

                dynamicSound.SubmitBuffer(shortBuffer);

                if (read < floatBuffer.Length)
                {
                    oggStream.Seek(0, SeekOrigin.Begin);
                }
            }
        }

        public void Dispose()
        {
            if (dynamicSound != null)
            {
                dynamicSound.Dispose();
                dynamicSound = null;
            }

            if (sounds != null)
            {
                for (var i = 0; i < sounds.Length; i++)
                {
                    if (sounds[i] != null)
                    {
                        sounds[i].Dispose();
                        sounds[i] = null;
                    }
                }
            }
        }
    }
}
