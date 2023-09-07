using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonderfulUtils.Model
{
    public class WordData
    {
        public List<WordItens> items { get; set; }
    }

    public class WordItens
    {
        public string key { get; set; }
        public string japaneseText { get; set; }
        public string englishUsText { get; set; }
        public int englishUsFontType { get; set; }
        public string frenchText { get; set; }
        public int frenchFontType { get; set; }
        public string italianText { get; set; }
        public int italianFontType { get; set; }
        public string germanText { get; set; }
        public int germanFontType { get; set; }
        public string spanishText { get; set; }
        public int spanishFontType { get; set; }
        public string chineseTText { get; set; }
        public int chineseTFontType { get; set; }
        public string chineseSText { get; set; }
        public int chineseSFontType { get; set; }
        public string koreanText { get; set; }
        public int koreanFontType { get; set; }

    }
}
