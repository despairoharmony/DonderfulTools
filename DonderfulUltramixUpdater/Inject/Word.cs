using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DonderfulUtils.Model;

namespace DonderfulUltramixUpdater.Inject
{
    public static class Word
    {
        private static WordItens Update(this WordItens input, WordItens refresh)
        {
            if (refresh.japaneseText != null)
                input.japaneseText = refresh.japaneseText;

            if (refresh.englishUsText != null)
                input.englishUsText = refresh.englishUsText;
            input.englishUsFontType = 3;

            if (refresh.frenchText != null)
                input.frenchText = refresh.frenchText;
            input.frenchFontType = 3;

            if (refresh.italianText != null)
                input.italianText = refresh.italianText;
            input.italianFontType = 3;

            if (refresh.germanText != null)
                input.germanText = refresh.germanText;
            input.germanFontType = 3;

            if (refresh.spanishText != null)
                input.spanishText = refresh.spanishText;
            input.spanishFontType = 3;

            if (refresh.chineseTText != null)
                input.chineseTText = refresh.chineseTText;
            input.chineseTFontType = 1;

            if (refresh.chineseSText != null)
                input.chineseSText = refresh.chineseSText;
            input.chineseSFontType = 4;

            if (refresh.koreanText != null)
                input.koreanText = refresh.koreanText;
            input.koreanFontType = 2;

            return input;
        }

        public static WordData Inject(this WordData input, WordData refresh)
        {
            foreach (var item in refresh.items)
            {
                bool isNew = true;
                if(item.key.Contains("song_") && !item.key.Contains("song_small") && !item.key.Contains("song_sort") && !item.key.Contains("song_japan") && !item.englishUsText.ToLower().Contains("now printing"))
                {
                    for (int i = 0; i < input.items.Count; i++)
                    {
                        if (input.items[i].key == item.key)
                        {
                            isNew = false;
                            input.items[i] = input.items[i].Update(item);
                            break;
                        }
                    }
                    if (isNew)
                    {
                        input.items.Add(item);
                    }
                }
            }

            return input;
        }

        public static WordData RemoveDuplicates(this WordData input)
        {
            for (int i = 0; i < input.items.Count; i++)
            {
                if (input.items[i].key.Contains("song_"))
                {
                    for (int j = i + 1; j < input.items.Count; j++)
                    {
                        if (input.items[i].key == input.items[j].key)
                        {
                            input.items.Remove(input.items[j]);
                        }
                    }
                }
            }

            return input;
        }
    }
}
