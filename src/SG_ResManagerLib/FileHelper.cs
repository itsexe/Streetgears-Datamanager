using System;
using System.Text;

namespace SG_ResManagerLib
{
    /// <summary>
    /// This class contains functions copy and pasted from HexRays.
    /// Reversed by https://github.com/glandu2
    /// </summary>
    public static class FileHelper
    {
        //Decryption Key
        internal static readonly byte[] Xor = new byte[] {
            0x77, 0xE8, 0x5E, 0xEC, 0xB7, 0x4E, 0xC1, 0x87, 0x4F, 0xE6, 0xF5, 0x3C, 0x1F, 0xB3, 0x15, 0x43,
            0x6A, 0x49, 0x30, 0xA6, 0xBF, 0x53, 0xA8, 0x35, 0x5B, 0xE5, 0x9E, 0x0E, 0x41, 0xEC, 0x22, 0xB8,
            0xD4, 0x80, 0xA4, 0x8C, 0xCE, 0x65, 0x13, 0x1D, 0x4B, 0x08, 0x5A, 0x6A, 0xBB, 0x6F, 0xAD, 0x25,
            0xB8, 0xDD, 0xCC, 0x77, 0x30, 0x74, 0xAC, 0x8C, 0x5A, 0x4A, 0x9A, 0x9B, 0x36, 0xBC, 0x53, 0x0A,
            0x3C, 0xF8, 0x96, 0x0B, 0x5D, 0xAA, 0x28, 0xA9, 0xB2, 0x82, 0x13, 0x6E, 0xF1, 0xC1, 0x93, 0xA9,
            0x9E, 0x5F, 0x20, 0xCF, 0xD4, 0xCC, 0x5B, 0x2E, 0x16, 0xF5, 0xC9, 0x4C, 0xB2, 0x1C, 0x57, 0xEE,
            0x14, 0xED, 0xF9, 0x72, 0x97, 0x22, 0x1B, 0x4A, 0xA4, 0x2E, 0xB8, 0x96, 0xEF, 0x4B, 0x3F, 0x8E,
            0xAB, 0x60, 0x5D, 0x7F, 0x2C, 0xB8, 0xAD, 0x43, 0xAD, 0x76, 0x8F, 0x5F, 0x92, 0xE6, 0x4E, 0xA7,
            0xD4, 0x47, 0x19, 0x6B, 0x69, 0x34, 0xB5, 0x0E, 0x62, 0x6D, 0xA4, 0x52, 0xB9, 0xE3, 0xE0, 0x64,
            0x43, 0x3D, 0xE3, 0x70, 0xF5, 0x90, 0xB3, 0xA2, 0x06, 0x42, 0x02, 0x98, 0x29, 0x50, 0x3F, 0xFD,
            0x97, 0x58, 0x68, 0x01, 0x8C, 0x1E, 0x0F, 0xEF, 0x8B, 0xB3, 0x41, 0x44, 0x96, 0x21, 0xA8, 0xDA,
            0x5E, 0x8B, 0x4A, 0x53, 0x1B, 0xFD, 0xF5, 0x21, 0x3F, 0xF7, 0xBA, 0x68, 0x47, 0xF9, 0x65, 0xDF,
            0x52, 0xCE, 0xE0, 0xDE, 0xEC, 0xEF, 0xCD, 0x77, 0xA2, 0x0E, 0xBC, 0x38, 0x2F, 0x64, 0x12, 0x8D,
            0xF0, 0x5C, 0xE0, 0x0B, 0x59, 0xD6, 0x2D, 0x99, 0xCD, 0xE7, 0x01, 0x15, 0xE0, 0x67, 0xF4, 0x32,
            0x35, 0xD4, 0x11, 0x21, 0xC3, 0xDE, 0x98, 0x65, 0xED, 0x54, 0x9D, 0x1C, 0xB9, 0xB0, 0xAA, 0xA9,
            0x0C, 0x8A, 0xB4, 0x66, 0x60, 0xE1, 0xFF, 0x2E, 0xC8, 0x00, 0x43, 0xA9, 0x67, 0x37, 0xDB, 0x9C,
            0x77, 0xE8, 0x5E, 0xEC, 0xB7, 0x4E, 0xC1, 0x87, 0x4F, 0xE6, 0xF5, 0x3C, 0x1F, 0xB3, 0x15, 0x43,
            0x6A, 0x49, 0x30, 0xA6, 0xBF, 0x53, 0xA8, 0x35, 0x5B, 0xE5, 0x9E, 0x0E, 0x41, 0xEC, 0x22, 0xB8,
            0xD4, 0x80, 0xA4, 0x8C, 0xCE, 0x65, 0x13, 0x1D, 0x4B, 0x08, 0x5A, 0x6A, 0xBB, 0x6F, 0xAD, 0x25,
            0xB8, 0xDD, 0xCC, 0x77, 0x30, 0x74, 0xAC, 0x8C, 0x5A, 0x4A, 0x9A, 0x9B, 0x36, 0xBC, 0x53, 0x0A,
            0x3C, 0xF8, 0x96, 0x0B, 0x5D, 0xAA, 0x28, 0xA9, 0xB2, 0x82, 0x13, 0x6E, 0xF1, 0xC1, 0x93, 0xA9,
            0x9E, 0x5F, 0x20, 0xCF, 0xD4, 0xCC, 0x5B, 0x2E, 0x16, 0xF5, 0xC9, 0x4C, 0xB2, 0x1C, 0x57, 0xEE,
            0x14, 0xED, 0xF9, 0x72, 0x97, 0x22, 0x1B, 0x4A, 0xA4, 0x2E, 0xB8, 0x96, 0xEF, 0x4B, 0x3F, 0x8E,
            0xAB, 0x60, 0x5D, 0x7F, 0x2C, 0xB8, 0xAD, 0x43, 0xAD, 0x76, 0x8F, 0x5F, 0x92, 0xE6, 0x4E, 0xA7,
            0xD4, 0x47, 0x19, 0x6B, 0x69, 0x34, 0xB5, 0x0E, 0x62, 0x6D, 0xA4, 0x52, 0xB9, 0xE3, 0xE0, 0x64,
            0x43, 0x3D, 0xE3, 0x70, 0xF5, 0x90, 0xB3, 0xA2, 0x06, 0x42, 0x02, 0x98, 0x29, 0x50, 0x3F, 0xFD,
            0x97, 0x58, 0x68, 0x01, 0x8C, 0x1E, 0x0F, 0xEF, 0x8B, 0xB3, 0x41, 0x44, 0x96, 0x21, 0xA8, 0xDA,
            0x5E, 0x8B, 0x4A, 0x53, 0x1B, 0xFD, 0xF5, 0x21, 0x3F, 0xF7, 0xBA, 0x68, 0x47, 0xF9, 0x65, 0xDF,
            0x52, 0xCE, 0xE0, 0xDE, 0xEC, 0xEF, 0xCD, 0x77, 0xA2, 0x0E, 0xBC, 0x38, 0x2F, 0x64, 0x12, 0x8D,
            0xF0, 0x5C, 0xE0, 0x0B, 0x59, 0xD6, 0x2D, 0x99, 0xCD, 0xE7, 0x01, 0x15, 0xE0, 0x67, 0xF4, 0x32,
            0x35, 0xD4, 0x11, 0x21, 0xC3, 0xDE, 0x98, 0x65, 0xED, 0x54, 0x9D, 0x1C, 0xB9, 0xB0, 0xAA, 0xA9,
            0x0C, 0x8A, 0xB4, 0x66, 0x60, 0xE1, 0xFF, 0x2E, 0xC8, 0x00, 0x43, 0xA9, 0x67, 0x37, 0xDB, 0x9C
        };
        private static readonly byte[] _sCipherTable = new byte[]
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

        private static readonly byte[] _sCharTable = new byte[]
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
        /// <summary>
        /// Returns the encoded file name given by the argument
        /// </summary>
        /// <param name="encodedFileName"></param>
        /// <returns></returns>
        internal static string EncodeFileName(string encodedFileName)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(encodedFileName.ToLower());
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num = (int)(bytes[i] * 17) + num + 1;
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
                num = (int)bytes[i];
                for (int j = 0; j < num2; j++)
                {
                    num = (int)_sCipherTable[num];
                }
                num2 = (num2 + 1 + (int)(bytes[i] * 17) & 31);
                if (num2 == 0)
                {
                    num2 = 32;
                }
                bytes[i] = Convert.ToByte(num);
            }
            int num3 = (int)Math.Floor(0.33000001311302191 * (double)bytes.Length);
            int num4 = (int)Math.Floor(0.6600000262260437 * (double)bytes.Length);
            byte b = bytes[num4];
            byte b2 = bytes[num3];
            bytes[num4] = bytes[0];
            bytes[num3] = bytes[1];
            bytes[0] = b;
            bytes[1] = b2;
            num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num += (int)bytes[i];
            }
            return Convert.ToChar(_sCharTable[num % 84]) + Encoding.ASCII.GetString(bytes) + Convert.ToChar(value);
        }
        /// <summary>
        /// Returns the decoded file name given by the argument
        /// </summary>
        /// <param name="encodedFileName"></param>
        /// <returns></returns>
        internal static string DecodeFileName(string encodedFileName)
        {
            string result;
            if (encodedFileName.Length > 0)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(encodedFileName.Substring(1, encodedFileName.Length - 2));
                if (bytes.Length > 4)
                {
                    int num = (int)Math.Floor(0.33000001311302191 * (double)bytes.Length);
                    int num2 = (int)Math.Floor(0.6600000262260437 * (double)bytes.Length);
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
                    if ((char)_sCharTable[i] == encodedFileName[encodedFileName.Length - 1])
                    {
                        num3 = i;
                        break;
                    }
                }
                int num4 = num3;
                for (int i = 0; i < bytes.Length; i++)
                {
                    num3 = (int)bytes[i];
                    for (int j = 0; j < num4; j++)
                    {
                        int k;
                        for (k = 0; k < _sCipherTable.Length; k++)
                        {
                            if ((int)_sCipherTable[k] == num3)
                            {
                                break;
                            }
                        }
                        if (k < _sCipherTable.Length)
                        {
                            num3 = (int)((ushort)k);
                        }
                        else
                        {
                            num3 = 255;
                        }
                    }
                    bytes[i] = Convert.ToByte(num3);
                    num4 = (1 + num4 + 17 * num3 & 31);
                    if (num4 == 0)
                    {
                        num4 = 32;
                    }
                }
                result = Encoding.ASCII.GetString(bytes);
            }
            else
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// Gets the number of the number of res.xxx by the crypted filename.
        /// This functions is mostly copy & pasted from HexRays. 
        /// Reversed by https://github.com/glandu2
        /// </summary>
        /// <param name="encodedFileName">Encoded file name</param>
        /// <returns></returns>
        internal static int GetFileNumber(string encodedFileName)
        {
            byte[] buff = System.Text.Encoding.ASCII.GetBytes(encodedFileName.ToLower() + "0");
            int v3 = 0;
            int v30 = 0;
            int v31 = 0;
            int v29 = 0;
            if (buff[0] != 0)
            {
                while (buff.Length - 1 != v30)
                {
                    v3 = buff[v30];
                    v31 = v29 << 5;
                    v31 -= v29;

                    v31 += v3;
                    v30++;
                    v3 = buff[v30];
                    v29 = v31;
                }

                if (v31 < 0)
                    v29 = -v31;
            }
            return (v29 % 200) + 1;
        }
    }
}
