using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonderfulUtils.Model
{
    public class AccData
    {
        public List<AccItens> items { get; set; }
    }

    public class AccItens
    {
        public int number { get; set; }
        public string Id { get; set; }
        public bool userNotSelectable { get; set; }
        public bool flipTexture { get; set; }
        public string thumbnailPath { get; set; }
        public string nameKey { get; set; }
        public string descriptionKey { get; set; }
        public string DLC { get; set; }

    }
}
