using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DonderfulUtils.Model;

namespace DonderfulUltramixUpdater.Inject
{
    public static class Song
    {
        private static SongItens Update(this SongItens input, SongItens refresh)
        {
            input.uniqueId = refresh.uniqueId;

            input.isOnlineUsed = true;
            input.DLC = refresh.DLC;
            input.isRecording = true;
            input.isNew = refresh.isNew;
            input.HasInPackage = refresh.HasInPackage;
            input.HasPreviewInPackage = true;
            input.Reserve1 = "";
            input.Reserve2 = "";
            input.Reserve3 = ""; // Update 1.3.0 set Reserve 3 as DLC key
            input.previewPos = refresh.previewPos;
            input.fumenOffsetPos = refresh.fumenOffsetPos;
            input.playable_region_list = refresh.playable_region_list;
            input.subscription_region_list = refresh.subscription_region_list;
            input.dlc_region_list = refresh.dlc_region_list;

            input.calorie_e = refresh.calorie_e;
            input.calorie_n = refresh.calorie_n;
            input.calorie_e_1furi = refresh.calorie_e_1furi;
            input.calorie_n_1furi = refresh.calorie_n_1furi;
            input.dancerSet = refresh.dancerSet;

            return input;
        }

        private static SongItens Add(SongItens refresh)
        {
            SongItens input = new()
            {
                uniqueId = refresh.uniqueId,
                id = refresh.id,
                isLock = refresh.isLock,
                exclusionSongSelect = refresh.exclusionSongSelect,
                session = refresh.session,
                isOnlineUsed = true,
                DLC = refresh.DLC,
                isRecording = true,
                isNew = refresh.isNew,
                HasInPackage = refresh.HasInPackage,
                HasPreviewInPackage = true,
                Reserve1 = "",
                Reserve2 = "",
                Reserve3 = "",
                previewPos = refresh.previewPos,
                fumenOffsetPos = refresh.fumenOffsetPos,
                playable_region_list = refresh.playable_region_list,
                subscription_region_list = refresh.subscription_region_list,
                dlc_region_list = refresh.dlc_region_list,
                // Update 3.0.1 new fields -> fitness mode
                calorie_e = refresh.calorie_e,
                calorie_n = refresh.calorie_n,
                calorie_e_1furi = refresh.calorie_e_1furi,
                calorie_n_1furi = refresh.calorie_n_1furi,
                dancerSet = refresh.dancerSet,
            };

            return input;
        }

        public static SongData Inject(this SongData input, SongData refresh)
        {
            foreach (var item in refresh.items)
            {
                bool isNew = true;
                for (int i = 0; i < input.items.Count; i++)
                {
                    if (input.items[i].id == item.id)
                    {
                        isNew = false;
                        input.items[i] = input.items[i].Update(item);
                        break;
                    }
                }
                if (isNew)
                {
                    SongItens shin = Add(item);
                    input.items.Add(shin);
                }
            }

            return input;
        }
    }
}
