using System;
using System.Text;

namespace SGunpacker
{
    public class FileNameCipher
    {
        private readonly byte[] _sCipherTable = new byte[]
        {
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            103,
            32,
            0,
            38,
            119,
            44,
            108,
            78,
            88,
            79,
            0,
            55,
            46,
            37,
            101,
            0,
            56,
            95,
            93,
            35,
            80,
            49,
            45,
            36,
            86,
            91,
            0,
            89,
            0,
            94,
            0,
            0,
            75,
            125,
            106,
            48,
            64,
            71,
            83,
            41,
            65,
            120,
            121,
            54,
            57,
            69,
            70,
            123,
            87,
            98,
            61,
            82,
            118,
            116,
            104,
            50,
            52,
            77,
            40,
            107,
            0,
            109,
            97,
            43,
            126,
            68,
            39,
            67,
            33,
            74,
            73,
            100,
            66,
            85,
            96,
            113,
            102,
            112,
            72,
            81,
            51,
            76,
            110,
            111,
            90,
            105,
            114,
            115,
            117,
            59,
            122,
            99,
            0,
            84,
            53,
            0
        };

        private readonly byte[] _sCharTable = new byte[]
        {
            94,
            38,
            84,
            95,
            78,
            115,
            100,
            123,
            120,
            111,
            53,
            118,
            96,
            114,
            79,
            89,
            86,
            43,
            44,
            105,
            73,
            85,
            35,
            107,
            67,
            74,
            113,
            56,
            36,
            39,
            126,
            76,
            48,
            80,
            93,
            70,
            101,
            66,
            110,
            45,
            65,
            117,
            40,
            112,
            88,
            72,
            90,
            104,
            119,
            68,
            121,
            50,
            125,
            97,
            103,
            87,
            71,
            55,
            75,
            61,
            98,
            81,
            59,
            83,
            82,
            116,
            41,
            52,
            54,
            108,
            64,
            106,
            69,
            37,
            57,
            33,
            99,
            49,
            91,
            51,
            102,
            109,
            77,
            122,
            0
        };

        public string EncodeFileName(string szFileName)
        {
            byte[] bytes = Encoding.Default.GetBytes(szFileName.ToLower());
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num = (int) (bytes[i]*17) + num + 1;
            }
            num = (bytes.Length + num & 31);
            if (num == 0)
            {
                num = 32;
            }
            byte value = _sCharTable[num];
            int num2 = num;
            for (int i = 0; i < bytes.Length; i++)
            {
                num = (int) bytes[i];
                for (int j = 0; j < num2; j++)
                {
                    num = (int) _sCipherTable[num];
                }
                num2 = (num2 + 1 + (int) (bytes[i]*17) & 31);
                if (num2 == 0)
                {
                    num2 = 32;
                }
                bytes[i] = Convert.ToByte(num);
            }
            int num3 = (int) Math.Floor(0.33000001311302191*(double) bytes.Length);
            int num4 = (int) Math.Floor(0.6600000262260437*(double) bytes.Length);
            byte b = bytes[num4];
            byte b2 = bytes[num3];
            bytes[num4] = bytes[0];
            bytes[num3] = bytes[1];
            bytes[0] = b;
            bytes[1] = b2;
            num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num += (int) bytes[i];
            }
            return Convert.ToChar(_sCharTable[num%84]) + Encoding.Default.GetString(bytes) + Convert.ToChar(value);
        }

        public string DecodeFileName(string szFileHash)
        {
            string result;
            if (szFileHash.Length > 0)
            {
                byte[] bytes = Encoding.Default.GetBytes(szFileHash.Substring(1, szFileHash.Length - 2));
                if (bytes.Length > 4)
                {
                    int num = (int) Math.Floor(0.33000001311302191*(double) bytes.Length);
                    int num2 = (int) Math.Floor(0.6600000262260437*(double) bytes.Length);
                    byte b = bytes[num2];
                    byte b2 = bytes[num];
                    bytes[num2] = bytes[0];
                    bytes[num] = bytes[1];
                    bytes[0] = b;
                    bytes[1] = b2;
                }
                int num3 = 0;
                for (int i = 0; i < _sCharTable.Length; i++)
                {
                    if ((char) _sCharTable[i] == szFileHash[szFileHash.Length - 1])
                    {
                        num3 = i;
                        break;
                    }
                }
                int num4 = num3;
                for (int i = 0; i < bytes.Length; i++)
                {
                    num3 = (int) bytes[i];
                    for (int j = 0; j < num4; j++)
                    {
                        int k;
                        for (k = 0; k < _sCipherTable.Length; k++)
                        {
                            if ((int) _sCipherTable[k] == num3)
                            {
                                break;
                            }
                        }
                        if (k < _sCipherTable.Length)
                        {
                            num3 = (int) ((ushort) k);
                        }
                        else
                        {
                            num3 = 255;
                        }
                    }
                    bytes[i] = Convert.ToByte(num3);
                    num4 = (1 + num4 + 17*num3 & 31);
                    if (num4 == 0)
                    {
                        num4 = 32;
                    }
                }
                result = Encoding.Default.GetString(bytes);
            }
            else
            {
                result = "";
            }
            return result;
        }
    }
}
