using DonderfulUltramixUpdater.Inject;
using DonderfulUtils.Model;
using DonderfulUtils.Service;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Donderful Ultramix Updater\nDeveloped by Kamui (DespairOfHarmony)\nSpecial thanks: swigz\n");


try
{
    Directory.CreateDirectory("output");
    var opt = new JsonSerializerOptions();
    opt.WriteIndented = true;
    opt.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

    Console.WriteLine("Options Avaiable: ");
    Console.WriteLine("1 - Extract worddata from Ultramix");
    Console.WriteLine("2 - Inject musicdata to Ultramix");
    Console.WriteLine("3 - Inject songdata to Ultramix");
    Console.WriteLine("4 - Inject worddata to Ultramix");

    Console.Write("Choose a option: ");
    string type = Console.ReadLine();

    if (type == "1")
    {
        Console.WriteLine("Write the worddata.json path: ");
        string wordpath = Console.ReadLine();
        wordpath = wordpath.Replace("\"", "");
        string wordjson = File.ReadAllText(wordpath);
        WordData word = JsonSerializer.Deserialize<WordData>(wordjson);

        WordData newword = new();
        newword.items = new();

        WordItens versionkey = word.items.FirstOrDefault(k => k.key == "title_version_number");
        newword.items.Add(versionkey);

        Console.WriteLine("Write the musicdata.json path: ");
        string musicpath = Console.ReadLine();
        if (!string.IsNullOrEmpty(musicpath))
        {
            musicpath = musicpath.Replace("\"", "");
            string musicjson = File.ReadAllText(musicpath);
            MusicData music = JsonSerializer.Deserialize<MusicData>(musicjson);

            foreach (var item in music.items)
            {
                string[] keywords = {
                    "song_" + item.id,
                    "song_sub_" + item.id,
                    "song_detail_" + item.id,
                };

                foreach (string key in keywords)
                {
                    WordItens newitem = word.items.FirstOrDefault(k => k.key == key);
                    if (newitem != null)
                        newword.items.Add(newitem);
                }
            }
        }

        Console.WriteLine("Write the acc.json path: ");
        string accpath = Console.ReadLine();
        if (!string.IsNullOrEmpty(accpath))
        {
            accpath = accpath.Replace("\"", "");
            string accjson = File.ReadAllText(accpath);
            AccData acc = JsonSerializer.Deserialize<AccData>(accjson);

            foreach (var item in acc.items)
            {
                string[] keywords = {
                    item.nameKey,
                    item.descriptionKey
                };

                foreach (string key in keywords)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        WordItens newitem = word.items.FirstOrDefault(k => k.key == key);
                        if (newitem != null)
                            newword.items.Add(newitem);
                    }
                }
            }
        }

        string newwordjson = JsonSerializer.Serialize<WordData>(newword, opt);
        File.WriteAllText(@"output\worddata_extract.json", newwordjson);
    }
    else if (type == "2")
    {
        Console.WriteLine("Write the input (OG) musicdata.json path: ");
        string musicpath = Console.ReadLine();
        musicpath = musicpath.Replace("\"", "");
        string musicjson = File.ReadAllText(musicpath);
        MusicData music = JsonSerializer.Deserialize<MusicData>(musicjson);

        Console.WriteLine("Write the refresh (MP) musicdata.json path: ");
        string musicpath_new = Console.ReadLine();
        musicpath_new = musicpath_new.Replace("\"", "");
        string musicjson_new = File.ReadAllText(musicpath_new);
        MusicData music_new = JsonSerializer.Deserialize<MusicData>(musicjson_new);

        music = music.Inject(music_new);
        music.items = music.items.OrderBy(x => x.uniqueId).ToList();

        string newmusicjson = JsonSerializer.Serialize<MusicData>(music, opt);
        File.WriteAllText(@"output\musicdata.json", newmusicjson);
    }
    else if (type == "3")
    {
        Console.WriteLine("Write the input (OG) songdata.json path: ");
        string songpath = Console.ReadLine();
        songpath = songpath.Replace("\"", "");
        string songjson = File.ReadAllText(songpath);
        SongData song = JsonSerializer.Deserialize<SongData>(songjson);

        Console.WriteLine("Write the refresh (MP) songdata.json path: ");
        string songpath_new = Console.ReadLine();
        songpath_new = songpath_new.Replace("\"", "");
        string songjson_new = File.ReadAllText(songpath_new);
        SongData song_new = JsonSerializer.Deserialize<SongData>(songjson_new);

        song = song.Inject(song_new);
        song.items = song.items.OrderBy(x => x.uniqueId).ToList();

        string newsongjson = JsonSerializer.Serialize<SongData>(song, opt);
        File.WriteAllText(@"output\songdata.json", newsongjson);
    }
    else if (type == "4")
    {
        Console.WriteLine("Write the input (OG) worddata.json path: ");
        string wordpath = Console.ReadLine();
        wordpath = wordpath.Replace("\"", "");
        string wordjson = File.ReadAllText(wordpath);
        WordData word = JsonSerializer.Deserialize<WordData>(wordjson);

        Console.WriteLine("Write the refresh (Extract) worddata.json path: ");
        string wordpath_new = Console.ReadLine();
        wordpath_new = wordpath_new.Replace("\"", "");
        string wordjson_new = File.ReadAllText(wordpath_new);
        WordData word_new = JsonSerializer.Deserialize<WordData>(wordjson_new);

        word = word.Inject(word_new);

        string newwordjson = JsonSerializer.Serialize<WordData>(word, opt);
        File.WriteAllText(@"output\worddata.json", newwordjson);
    }
    else
    {
        Console.WriteLine("Option typed wrong: " + type);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Unknown error:\n" + ex);
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();
