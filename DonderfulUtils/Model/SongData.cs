using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonderfulUtils.Model
{
    public class SongData
    {
        public List<SongItens> items { get; set; }
    }

    public class SongItens
    {
        public int uniqueId { get; set; }
        public string id { get; set; }
        public bool isLock { get; set; }
        public bool exclusionSongSelect { get; set; }
        public bool session { get; set; }
        public bool isOnlineUsed { get; set; }
        public string DLC { get; set; }
        public bool isRecording { get; set; }
        public bool isNew { get; set; }
        public string HasInPackage { get; set; }
        public bool HasPreviewInPackage { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public int previewPos { get; set; }
        public int fumenOffsetPos { get; set; }
        public string playable_region_list { get; set; }
        public string subscription_region_list { get; set; }
        public string dlc_region_list { get; set; }
        public decimal calorie_e {  get; set; }
        public decimal calorie_n { get; set; }
        public decimal calorie_e_1furi { get; set; }
        public decimal calorie_n_1furi { get; set; }
        public string dancerSet { get; set; }
    }
}
