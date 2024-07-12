using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonderfulUtils.Model;
using DonderfulUtils.Encode;
using System.Text.Json;

namespace DonderfulUtils.Service
{
    public class MySaveData
    {
        public static SaveData GetSongData(byte[] savefile)
        {
            byte[] firstencr = ExtractFirstEncr(savefile);
            (byte[] key, byte[] decfile) = Key.Get(firstencr);
            decfile = XOR.Decrypt(decfile, key);

            byte[] jsonstart = Encoding.UTF8.GetBytes(@"{""song_uid"":");
            byte[] jsonend = Encoding.UTF8.GetBytes(@"""}""}");

            string jsondata;

            SaveData result = new();
            result.items = new();

            for (int i = 0; i < decfile.Length; i++)
            {
                if (decfile.Skip(i).Take(jsonstart.Length).SequenceEqual(jsonstart))
                {
                    for (int j = i; j < decfile.Length; j++)
                    {
                        if (decfile.Skip(j).Take(jsonend.Length).SequenceEqual(jsonend))
                        {
                            byte[] json = decfile.Skip(i).Take((j + jsonend.Length) - i).ToArray();
                            jsondata = Encoding.UTF8.GetString(json);
                            result.items.Add(JsonSerializer.Deserialize<SaveItens>(jsondata));
                            break;
                        }
                    }
                }
            }

            return result;
        }
        public static byte[] ExtractFirstEncr(byte[] save)
        {
            byte[] encr = Encoding.ASCII.GetBytes("Encr");
            int finalpos = 0;
            for (int i = 0; i < save.Length; i++)
            {
                if (save.Skip(i).Take(encr.Length).SequenceEqual(encr))
                {
                    finalpos = i + 4;
                    break;
                }
            }

            byte[] first = save.Take(finalpos).ToArray();

            return first;
        }

    }
}
