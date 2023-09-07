using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonderfulUtils.Model
{
    public class SaveData
    {
        public List<SaveItens> items { get; set; }
    }

    public class SaveItens
    {
        public int song_uid { get; set; }
        public string playable_region_list { get; set; }
        public string subscription_region_list { get; set; }
        public string individual_region_list { get; set; }
        public int sort_order { get; set; }
        public string song_id { get; set; }
        public string song_name_ja { get; set; }
        public string song_name_en { get; set; }
        public string song_name_fr { get; set; }
        public string song_name_it { get; set; }
        public string song_name_de { get; set; }
        public string song_name_es { get; set; }
        public string song_name_zh_tw { get; set; }
        public string song_name_zh_cn { get; set; }
        public string song_name_ko { get; set; }
        public string song_ruby_ja { get; set; }
        public string song_ruby_en { get; set; }
        public string song_ruby_fr { get; set; }
        public string song_ruby_it { get; set; }
        public string song_ruby_de { get; set; }
        public string song_ruby_es { get; set; }
        public string song_ruby_zh_tw { get; set; }
        public string song_ruby_zh_cn { get; set; }
        public string song_ruby_ko { get; set; }
        public string song_name_sub_ja { get; set; }
        public string song_name_sub_en { get; set; }
        public string song_name_sub_fr { get; set; }
        public string song_name_sub_it { get; set; }
        public string song_name_sub_de { get; set; }
        public string song_name_sub_es { get; set; }
        public string song_name_sub_zh_tw { get; set; }
        public string song_name_sub_zh_cn { get; set; }
        public string song_name_sub_ko { get; set; }
        public int genre_id { get; set; }
        public int is_branch_1 { get; set; }
        public int is_branch_2 { get; set; }
        public int is_branch_3 { get; set; }
        public int is_branch_4 { get; set; }
        public int is_branch_5 { get; set; }
        public int star_1 { get; set; }
        public int star_2 { get; set; }
        public int star_3 { get; set; }
        public int star_4 { get; set; }
        public int star_5 { get; set; }
        public int shin_score_1 { get; set; }
        public int shin_score_2 { get; set; }
        public int shin_score_3 { get; set; }
        public int shin_score_4 { get; set; }
        public int shin_score_5 { get; set; }
        public int shin_score_multi_1 { get; set; }
        public int shin_score_multi_2 { get; set; }
        public int shin_score_multi_3 { get; set; }
        public int shin_score_multi_4 { get; set; }
        public int shin_score_multi_5 { get; set; }
        public int is_mybattle_ok { get; set; }
        public int is_recording_ok { get; set; }
        public string game_mode { get; set; }
    }
}
