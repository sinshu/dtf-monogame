using System;

namespace MiswGame2008
{
    public class TopPlayerInfo
    {
        private int score;
        private int level;
        private string name;

        public TopPlayerInfo(int score, int level, string name)
        {
            this.score = score;
            this.level = level;
            this.name = name;
        }

        public int Score
        {
            get
            {
                return score;
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
