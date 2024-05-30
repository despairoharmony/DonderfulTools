using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using DonderfulUtils.Model;

namespace DonderfulMPInject.Inject
{
    public class Song
    {
        private static SongItens Add(SongItens input, SaveItens save)
        {
            InternalData extra = JsonSerializer.Deserialize<InternalData>(save.game_mode);

            input.uniqueId = save.song_uid;
            input.id = save.song_id;
            input.isLock = false;
            input.exclusionSongSelect = false;
            input.session = false;
            input.isOnlineUsed = true;
            input.DLC = "HkX8sA53LnJi"; // Activate with "World is Mine" DLC
            input.isRecording = true;
            input.isNew = true;
            input.HasInPackage = "2";
            input.HasPreviewInPackage = false;
            input.Reserve1 = "";
            input.Reserve2 = extra.Reserve2;
            input.Reserve3 = ""; // Update 1.3.0 set Reserve 3 as DLC key
            input.previewPos = 0;
            input.fumenOffsetPos = 0;
            input.playable_region_list = "1,2,3";
            input.subscription_region_list = "1,2,3";
            input.dlc_region_list = "1,2,3";
            // Update 3.0.1 new fields -> fitness mode
            input.calorie_e = decimal.Parse(extra.calorie_e, CultureInfo.InvariantCulture);
            input.calorie_n = decimal.Parse(extra.calorie_n, CultureInfo.InvariantCulture);
            input.calorie_e_1furi = decimal.Parse(extra.calorie_e_1furi, CultureInfo.InvariantCulture);
            input.calorie_n_1furi = decimal.Parse(extra.calorie_n_1furi, CultureInfo.InvariantCulture);
            //Update 3.2.0 new field - > dancer set
            input.dancerSet = extra.DancerSet;

            return input;
        }
        public static SongData Inject(SongData input, SaveData save)
        {
            bool isNew;

            //Disable non released songs
            for (int i = 0; i < input.items.Count; i++)
            {
                if (input.items[i].HasInPackage != "1")
                {
                    input.items[i].DLC = "NoNReLEASED";
                }
            }

            foreach (SaveItens data in save.items)
            {
                isNew = true;
                for (int j = 0; j < input.items.Count; j++)
                {
                    if (input.items[j].uniqueId == data.song_uid)
                    {
                        isNew = false;
                        input.items[j] = Add(input.items[j], data);
                    }
                }

                if (isNew)
                {
                    SongItens shin = Add(new SongItens(), data);
                    input.items.Add(shin);
                }
            }

            return input;
        }
    }
}