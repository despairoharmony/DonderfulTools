using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonderfulUtils.Service
{
    public class Key
    {
        public static (byte[], byte[]) Get(byte[] fullfile)
        {
            byte[] key = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                key[i] = fullfile[fullfile.Length - 8 + i];
            }

            byte[] file = new byte[fullfile.Length - 8];

            for (int j = 0; j < file.Length; j++)
            {
                file[j] = fullfile[j];
            }

            return (key, file);
        }
    }
}
