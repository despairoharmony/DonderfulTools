using System;
using System.Collections.Generic;
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
            input.previewPos = refresh.previewPos;
            input.fumenOffsetPos = refresh.fumenOffsetPos;

            return input;
        }
        private static SongItens Add(SongItens refresh)
        {
            SongItens input = new()
            {
                uniqueId = refresh.uniqueId,
                id = refresh.id,
                isLock = false,
                exclusionSongSelect = false,
                session = false,
                isOnlineUsed = true,
                DLC = "",
                isRecording = true,
                isNew = false,
                HasInPackage = "1",
                HasPreviewInPackage = true,
                Reserve1 = "",
                Reserve2 = "",
                Reserve3 = "",
                previewPos = 0,
                fumenOffsetPos = 0,
                playable_region_list = "1,2,3",
                subscription_region_list = "1,2,3",
                dlc_region_list = "1,2,3"
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
                    while (input.CheckUniqueId(item.uniqueId))
                        item.uniqueId = item.uniqueId + 3000;
                    SongItens shin = Add(item);
                    input.items.Add(shin);
                }
            }

            return input;
        }

        private static bool CheckUniqueId(this SongData input, int id)
        {
            foreach (var item in input.items)
            {
                if (item.uniqueId == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
