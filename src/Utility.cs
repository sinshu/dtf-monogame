using System;

namespace MiswGame2008
{
    public class Utility
    {
        public static int GetEnumCount(Type enumType)
        {
            int count = 0;
            foreach (int i in Enum.GetValues(enumType))
            {
                count++;
            }
            return count;
        }

        public static double Sin(int deg)
        {
            return Math.Sin(deg * Math.PI / 180);
        }

        public static double Cos(int deg)
        {
            return Math.Cos(deg * Math.PI / 180);
        }

        public static int Atan2(double y, double x)
        {
            return (int)Math.Round(Math.Atan2(y, x) * 180 / Math.PI);
        }

        public static int NormalizeDeg(int deg)
        {
            return ((deg + 180) % 360 + 360) % 360 - 180;
        }
    }
}
