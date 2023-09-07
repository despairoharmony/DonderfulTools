using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using DonderfulUtils.Model;

namespace DonderfulMPInject.Inject
{
    public class Music
    {
        private static MusicItens Add(MusicItens input, SaveItens save)
        {
            InternalData extra = JsonSerializer.Deserialize<InternalData>(save.game_mode);

            input.uniqueId = save.song_uid;
            input.id = save.song_id;
            input.songFileName = extra.SongFileName;
            input.order = save.sort_order;
            input.genreNo = save.genre_id;
            input.debug = false;
            input.branchEasy = Convert.ToBoolean(save.is_branch_1);
            input.branchNormal = Convert.ToBoolean(save.is_branch_2);
            input.branchHard = Convert.ToBoolean(save.is_branch_3);
            input.branchMania = Convert.ToBoolean(save.is_branch_4);
            input.branchUra = Convert.ToBoolean(save.is_branch_5);
            input.starEasy = save.star_1;
            input.starNormal = save.star_2;
            input.starHard = save.star_3;
            input.starMania = save.star_4;
            input.starUra = save.star_5;
            input.shinutiEasy = save.shin_score_1;
            input.shinutiNormal = save.shin_score_2;
            input.shinutiHard = save.shin_score_3;
            input.shinutiMania = save.shin_score_4;
            input.shinutiUra = save.shin_score_5;
            input.shinutiEasyDuet = save.shin_score_multi_1;
            input.shinutiNormalDuet = save.shin_score_multi_2;
            input.shinutiHardDuet = save.shin_score_multi_3;
            input.shinutiManiaDuet = save.shin_score_multi_4;
            input.shinutiUraDuet = save.shin_score_multi_5;

            return input;
        }

        public static MusicData Inject(MusicData input, SaveData save)
        {
            bool isNew;
            foreach (SaveItens data in save.items)
            {
                isNew = true;
                for (int i = 0; i < input.items.Count; i++)
                {
                    if (input.items[i].uniqueId == data.song_uid)
                    {
                        isNew = false;
                        input.items[i] = Add(input.items[i], data);
                    }
                }

                if (isNew)
                {
                    MusicItens shin = Add(new MusicItens(), data);
                    input.items.Add(shin);
                }
            }

            return input;
        }
    }
}
