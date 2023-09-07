using System;
using System.Collections.Generic;
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
