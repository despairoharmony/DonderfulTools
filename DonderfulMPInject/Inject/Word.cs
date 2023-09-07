using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using DonderfulUtils.Model;

namespace DonderfulMPInject.Inject
{
    public class Word
    {
        private static WordItens AddTitle(WordItens input, SaveItens save)
        {
            input.key = String.Format("song_{0}", save.song_id);
            input.japaneseText = Regex.Replace(save.song_name_ja, "<[^>]+>", string.Empty);
            input.englishUsText = Regex.Replace(save.song_name_en, "<[^>]+>", string.Empty);
            input.englishUsFontType = 3;
            input.frenchText = Regex.Replace(save.song_name_fr, "<[^>]+>", string.Empty);
            input.frenchFontType = 3;
            input.italianText = Regex.Replace(save.song_name_it, "<[^>]+>", string.Empty);
            input.italianFontType = 3;
            input.germanText = Regex.Replace(save.song_name_de, "<[^>]+>", string.Empty);
            input.germanFontType = 3;
            input.spanishText = Regex.Replace(save.song_name_es, "<[^>]+>", string.Empty);
            input.spanishFontType = 3;
            input.chineseTText = Regex.Replace(save.song_name_zh_tw, "<[^>]+>", string.Empty);
            input.chineseTFontType = 1;
            input.chineseSText = Regex.Replace(save.song_name_zh_cn, "<[^>]+>", string.Empty);
            input.chineseSFontType = 4;
            input.koreanText = Regex.Replace(save.song_name_ko, "<[^>]+>", string.Empty);
            input.koreanFontType = 2;

            return input;
        }

        private static WordItens AddSub(WordItens input, SaveItens save)
        {
            input.key = String.Format("song_sub_{0}", save.song_id);
            input.japaneseText = Regex.Replace(save.song_name_sub_ja, "<[^>]+>", string.Empty);
            input.englishUsText = Regex.Replace(save.song_name_sub_en, "<[^>]+>", string.Empty);
            input.englishUsFontType = 3;
            input.frenchText = Regex.Replace(save.song_name_sub_fr, "<[^>]+>", string.Empty);
            input.frenchFontType = 3;
            input.italianText = Regex.Replace(save.song_name_sub_it, "<[^>]+>", string.Empty);
            input.italianFontType = 3;
            input.germanText = Regex.Replace(save.song_name_sub_de, "<[^>]+>", string.Empty);
            input.germanFontType = 3;
            input.spanishText = Regex.Replace(save.song_name_sub_es, "<[^>]+>", string.Empty);
            input.spanishFontType = 3;
            input.chineseTText = Regex.Replace(save.song_name_sub_zh_tw, "<[^>]+>", string.Empty);
            input.chineseTFontType = 1;
            input.chineseSText = Regex.Replace(save.song_name_sub_zh_cn, "<[^>]+>", string.Empty);
            input.chineseSFontType = 4;
            input.koreanText = Regex.Replace(save.song_name_sub_ko, "<[^>]+>", string.Empty);
            input.koreanFontType = 2;

            return input;
        }

        private static WordItens AddDetail(WordItens input, SaveItens save)
        {
            input.key = String.Format("song_detail_{0}", save.song_id);

            if (Regex.Replace(save.song_ruby_ja, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_ja, "<[^>]+>", string.Empty))
                input.japaneseText = Regex.Replace(save.song_ruby_ja, "<[^>]+>", string.Empty);
            else
                input.japaneseText = "";

            if (Regex.Replace(save.song_ruby_en, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_en, "<[^>]+>", string.Empty))
                input.englishUsText = Regex.Replace(save.song_ruby_en, "<[^>]+>", string.Empty);
            else
                input.englishUsText = "";

            input.englishUsFontType = 0;

            if (Regex.Replace(save.song_ruby_fr, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_fr, "<[^>]+>", string.Empty))
                input.frenchText = Regex.Replace(save.song_ruby_fr, "<[^>]+>", string.Empty);
            else
                input.frenchText = "";

            input.frenchFontType = 0;

            if (Regex.Replace(save.song_ruby_it, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_it, "<[^>]+>", string.Empty))
                input.italianText = Regex.Replace(save.song_ruby_it, "<[^>]+>", string.Empty);
            else
                input.italianText = "";

            input.italianFontType = 0;

            if (Regex.Replace(save.song_ruby_de, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_de, "<[^>]+>", string.Empty))
                input.germanText = Regex.Replace(save.song_ruby_de, "<[^>]+>", string.Empty);
            else
                input.germanText = "";

            input.germanFontType = 0;

            if (Regex.Replace(save.song_ruby_es, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_es, "<[^>]+>", string.Empty))
                input.spanishText = Regex.Replace(save.song_ruby_es, "<[^>]+>", string.Empty);
            else
                input.spanishText = "";

            input.spanishFontType = 0;

            if (Regex.Replace(save.song_ruby_zh_tw, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_zh_tw, "<[^>]+>", string.Empty))
                input.chineseTText = Regex.Replace(save.song_ruby_zh_tw, "<[^>]+>", string.Empty);
            else
                input.chineseTText = "";

            input.chineseTFontType = 0;

            if (Regex.Replace(save.song_ruby_zh_cn, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_zh_cn, "<[^>]+>", string.Empty))
                input.chineseSText = Regex.Replace(save.song_ruby_zh_cn, "<[^>]+>", string.Empty);
            else
                input.chineseSText = "";

            input.chineseSFontType = 0;

            if (Regex.Replace(save.song_ruby_ko, "<[^>]+>", string.Empty) != Regex.Replace(save.song_name_ko, "<[^>]+>", string.Empty))
                input.koreanText = Regex.Replace(save.song_ruby_ko, "<[^>]+>", string.Empty);
            else
                input.koreanText = "";

            input.koreanFontType = 0;

            return input;
        }

        public static WordData Inject(WordData input, SaveData save)
        {
            bool isNew_title;
            bool isNew_sub;
            bool isNew_detail;

            string key_title;
            string key_sub;
            string key_detail;

            foreach (SaveItens data in save.items)
            {
                isNew_title = true;
                isNew_sub = true;
                isNew_detail = true;

                key_title = String.Format("song_{0}", data.song_id);
                key_sub = String.Format("song_sub_{0}", data.song_id);
                key_detail = String.Format("song_detail_{0}", data.song_id);

                for (int i = 0; i < input.items.Count; i++)
                {
                    if (input.items[i].key == key_title)
                    {
                        isNew_title = false;
                        input.items[i] = AddTitle(input.items[i], data);
                    }
                    if (input.items[i].key == key_sub)
                    {
                        isNew_sub = false;
                        input.items[i] = AddSub(input.items[i], data);
                    }
                    if (input.items[i].key == key_detail)
                    {
                        isNew_detail = false;
                        input.items[i] = AddDetail(input.items[i], data);
                    }
                }

                if (isNew_title)
                {
                    WordItens shin_1 = AddTitle(new WordItens(), data);
                    input.items.Add(shin_1);
                }
                if (isNew_sub)
                {
                    WordItens shin_2 = AddSub(new WordItens(), data);
                    input.items.Add(shin_2);
                }
                if (isNew_detail)
                {
                    WordItens shin_3 = AddDetail(new WordItens(), data);
                    input.items.Add(shin_3);
                }
            }

            return input;
        }
    }
}
