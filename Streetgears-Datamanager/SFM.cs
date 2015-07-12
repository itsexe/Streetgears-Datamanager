using System.Text;

namespace SGunpacker
{
    static class Sfm
    {
        public static string BytesToString(byte[] b)
        {
            int num = 0;
            for (int i = 0; i < (int)b.Length && b[i] > 0; i++)
            {
                num++;
            }
            byte[] numArray = new byte[num];
            for (int i = 0; i < num && b[i] > 0; i++)
            {
                numArray[i] = b[i];
            }

            return Encoding.ASCII.GetString(numArray);
        }
        public static byte[] StringToBytes(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}