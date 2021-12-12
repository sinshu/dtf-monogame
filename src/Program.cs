using System;

namespace MiswGame2008
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            int startLevel = 1;
            try
            {
                startLevel = int.Parse(args[0]);
            }
            catch
            {
            }

            using (MiswGame2008 game = new MiswGame2008(startLevel))
            {
                game.Run();
            }
        }
    }
}
