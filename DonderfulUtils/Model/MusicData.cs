using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonderfulUtils.Model
{
    public class MusicData
    {
        public List<MusicItens> items { get; set; }
    }

    public class MusicItens
    {
        public int uniqueId { get; set; }
        public string id { get; set; }
        public string songFileName { get; set; }
        public float order { get; set; }
        public int genreNo { get; set; }
        public bool debug { get; set; }
        public bool branchEasy { get; set; }
        public bool branchNormal { get; set; }
        public bool branchHard { get; set; }
        public bool branchMania { get; set; }
        public bool branchUra { get; set; }
        public int starEasy { get; set; }
        public int starNormal { get; set; }
        public int starHard { get; set; }
        public int starMania { get; set; }
        public int starUra { get; set; }
        public int shinutiEasy { get; set; }
        public int shinutiNormal { get; set; }
        public int shinutiHard { get; set; }
        public int shinutiMania { get; set; }
        public int shinutiUra { get; set; }
        public int shinutiEasyDuet { get; set; }
        public int shinutiNormalDuet { get; set; }
        public int shinutiHardDuet { get; set; }
        public int shinutiManiaDuet { get; set; }
        public int shinutiUraDuet { get; set; }
    }
}
