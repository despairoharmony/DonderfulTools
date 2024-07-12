using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DonderfulUtils.Encode
{
    public class XOR
    {
        public static byte[] Decrypt(byte[] text, byte[] key)
        {
            byte[] result = new byte[text.Length];

            for (int c = 0; c < text.Length; c++)
            {
                result[c] = (byte)((uint)text[c] ^ (uint)key[c % key.Length]);
            }

            return result;
        }

        public static byte[] Encrypt(byte[] text, byte[] key)
        {
            byte[] result = new byte[text.Length + 8];

            for (int c = 0; c < text.Length; c++)
            {
                result[c] = (byte)((uint)text[c] ^ (uint)key[c % key.Length]);
            }

            byte[] encr = Encoding.ASCII.GetBytes("Encr");

            for (int d = 0; d < 4; d++)
            {
                result[result.Length - 8 + d] = key[d];
            }

            for (int e = 0; e < 4; e++)
            {
                result[result.Length - 4 + e] = encr[e];
            }

            return result;
        }

        public static bool ValidateEncr(byte[] file)
        {
            byte[] encr = Encoding.ASCII.GetBytes("Encr");
            byte[] end = file.Skip(file.Length - 4).Take(4).ToArray();

            if (end.SequenceEqual(encr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
