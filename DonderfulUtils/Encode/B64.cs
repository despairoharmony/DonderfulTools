using System;
using System.Collections.Generic;
using System.Text;

namespace DonderfulUtils.Encode
{
    public class B64 //Base64
    {
        public static byte[] Decode(byte[] b64data)
        {
            string b64str = Encoding.UTF8.GetString(b64data);
            byte[] b64dec = Convert.FromBase64String(b64str);
            return b64dec;
        }
    }
}
