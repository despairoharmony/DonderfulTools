using DonderfulUtils.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonderfulUltramixUpdater.Inject
{
    public static class Music
    {
        private static MusicItens Update(this MusicItens input, MusicItens refresh)
        {
            //input.order = refresh.order;
            input.genreNo = refresh.genreNo;

            input.branchEasy = refresh.branchEasy;
            input.branchNormal = refresh.branchNormal;
            input.branchHard = refresh.branchHard;
            input.branchMania = refresh.branchMania;
            input.branchUra = refresh.branchUra;

            input.starEasy = refresh.starEasy;
            input.starNormal = refresh.starNormal;
            input.starHard = refresh.starHard;
            input.starMania = refresh.starMania;
            input.starUra = refresh.starUra;

            input.shinutiEasy = refresh.shinutiEasy;
            input.shinutiNormal = refresh.shinutiNormal;
            input.shinutiHard = refresh.shinutiHard;
            input.shinutiMania = refresh.shinutiMania;
            input.shinutiUra = refresh.shinutiUra;

            input.shinutiEasyDuet = refresh.shinutiEasyDuet;
            input.shinutiNormalDuet = refresh.shinutiNormalDuet;
            input.shinutiHardDuet = refresh.shinutiHardDuet;
            input.shinutiManiaDuet = refresh.shinutiManiaDuet;
            input.shinutiUraDuet = refresh.shinutiUraDuet;

            return input;
        }
        private static MusicItens Update_Game(this MusicItens input, MusicItens refresh)
        {
            input.branchEasy = refresh.branchEasy;
            input.branchNormal = refresh.branchNormal;
            input.branchHard = refresh.branchHard;
            input.branchMania = refresh.branchMania;
            input.branchUra = refresh.branchUra;

            input.starEasy = refresh.starEasy;
            input.starNormal = refresh.starNormal;
            input.starHard = refresh.starHard;
            input.starMania = refresh.starMania;
            input.starUra = refresh.starUra;

            input.shinutiEasy = refresh.shinutiEasy;
            input.shinutiNormal = refresh.shinutiNormal;
            input.shinutiHard = refresh.shinutiHard;
            input.shinutiMania = refresh.shinutiMania;
            input.shinutiUra = refresh.shinutiUra;

            input.shinutiEasyDuet = refresh.shinutiEasyDuet;
            input.shinutiNormalDuet = refresh.shinutiNormalDuet;
            input.shinutiHardDuet = refresh.shinutiHardDuet;
            input.shinutiManiaDuet = refresh.shinutiManiaDuet;
            input.shinutiUraDuet = refresh.shinutiUraDuet;

            return input;
        }

        private static MusicItens Update_AC(this MusicItens input, MusicItens refresh)
        {
            input.shinutiEasy = refresh.shinutiEasy;
            input.shinutiNormal = refresh.shinutiNormal;
            input.shinutiHard = refresh.shinutiHard;
            input.shinutiMania = refresh.shinutiMania;

            input.shinutiEasyDuet = refresh.shinutiEasyDuet;
            input.shinutiNormalDuet = refresh.shinutiNormalDuet;
            input.shinutiHardDuet = refresh.shinutiHardDuet;
            input.shinutiManiaDuet = refresh.shinutiManiaDuet;

            return input;
        }

        private static MusicItens Update_ACUra(this MusicItens input, MusicItens refresh)
        {
            input.shinutiUra = refresh.shinutiMania;
            input.shinutiUraDuet = refresh.shinutiManiaDuet;

            return input;
        }

        public static MusicData Inject(this MusicData input, MusicData refresh)
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
                    item.order = 9999;
                    item.songFileName = item.songFileName.Replace("NS2DLC/", "").Replace("fromOther/", "").Replace("fromPS4/", "").Replace("fromV12/", "").Replace("fromNS1/", "");
                    input.items.Add(item);
                }
            }

            return input;
        }

        public static MusicData InjectShinUti(this MusicData input, MusicData refresh)
        {
            foreach (var item in refresh.items)
            {
                for (int i = 0; i < input.items.Count; i++)
                {
                    if (input.items[i].id == item.id)
                    {
                        input.items[i] = input.items[i].Update_Game(item);
                        break;
                    }
                }
            }

            return input;
        }

        public static MusicData InjectShinUti_AC(this MusicData input, MusicData refresh)
        {
            foreach (var item in refresh.items)
            {
                for (int i = 0; i < input.items.Count; i++)
                {
                    if (input.items[i].id == item.id)
                    {
                        input.items[i] = input.items[i].Update_AC(item);
                        break;
                    } else if (input.items[i].id == item.id.Replace("ex_",""))
                    {
                        input.items[i] = input.items[i].Update_ACUra(item);
                        break;
                    }
                }
            }

            return input;
        }

        private static bool CheckUniqueId(this MusicData input, int id)
        {
            foreach(var item in input.items)
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
