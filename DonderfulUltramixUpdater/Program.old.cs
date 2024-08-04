/*
using DonderfulUltramixUpdater.Inject;
using DonderfulUtils.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");

try
{
    Directory.CreateDirectory("output");
    var opt = new JsonSerializerOptions();
    opt.WriteIndented = true;
    opt.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

    //Music
    bool isMusic_OG = File.Exists("musicdata_og.json");
    bool isMusic_AC15 = File.Exists("musicdata_AC15.json");
    bool isMusic_PS4 = File.Exists("musicinfo_ps4.json");
    bool isMusic_PTB = File.Exists("musicinfo_ptb.json");
    bool isMusic_NS1 = File.Exists("musicinfo_ns1.json");
    bool isMusic_MP = File.Exists("musicdata_mp.json");

    if (isMusic_OG)
    {
        MusicData music = new();
        string musicjson;

        string music_ogjson = File.ReadAllText("musicdata_og.json");
        var music_og = JsonSerializer.Deserialize<MusicData>(music_ogjson);
        music = music_og;

        if (isMusic_AC15)
        {
            string music_ac15son = File.ReadAllText("musicdata_AC15.json");
            var music_ac15 = JsonSerializer.Deserialize<MusicData>(music_ac15son);

            music = music.InjectShinUti_AC(music_ac15);
        }
        if (isMusic_PS4)
        {
            string music_ps4json = File.ReadAllText("musicinfo_ps4.json");
            var music_ps4 = JsonSerializer.Deserialize<MusicData>(music_ps4json);

            music = music.InjectShinUti(music_ps4);
        }
        if (isMusic_PTB)
        {
            string music_ptbjson = File.ReadAllText("musicinfo_ptb.json");
            var music_ptb = JsonSerializer.Deserialize<MusicData>(music_ptbjson);

            music = music.InjectShinUti(music_ptb);
        }
        if (isMusic_NS1)
        {
            string music_ns1json = File.ReadAllText("musicinfo_ns1.json");
            var music_ns1 = JsonSerializer.Deserialize<MusicData>(music_ns1json);

            music = music.InjectShinUti(music_ns1);
        }

        if (isMusic_MP)
        {
            string music_mpjson = File.ReadAllText("musicdata_mp.json");
            var music_mp = JsonSerializer.Deserialize<MusicData>(music_mpjson);

            music = music.Inject(music_mp);
        }

        music.items = music.items.OrderBy(x => x.uniqueId).ToList();
        musicjson = JsonSerializer.Serialize<MusicData>(music, opt);
        File.WriteAllText(@"output\musicdata.json", musicjson);
    }
    //Song
    bool isSong_OG = File.Exists("songdata_og.json");
    bool isSong_MP = File.Exists("songdata_mp.json");

    if (isMusic_OG)
    {
        SongData song = new();
        string songjson;

        string song_ogjson = File.ReadAllText("songdata_og.json");
        var song_og = JsonSerializer.Deserialize<SongData>(song_ogjson);
        song = song_og;

        if (isMusic_MP)
        {
            string song_mpjson = File.ReadAllText("songdata_mp.json");
            var song_mp = JsonSerializer.Deserialize<SongData>(song_mpjson);

            song = song.Inject(song_mp);
        }

        song.items = song.items.OrderBy(x => x.uniqueId).ToList();
        songjson = JsonSerializer.Serialize<SongData>(song, opt);
        File.WriteAllText(@"output\songdata.json", songjson);
    }

    //WordData
    bool isWord_OG = File.Exists("worddata_og.json");
    bool isWord_PS4AS = File.Exists("wordlist_ps4AS.json");
    bool isWord_PS4US = File.Exists("wordlist_ps4US.json");
    bool isWord_PTB = File.Exists("wordlist_ptb.json");
    bool isWord_NS1 = File.Exists("wordlist_ns1.json");
    bool isWord_MP = File.Exists("worddata_mp.json");

    if (isWord_OG)
    {
        WordData word = new();
        string wordjson;

        string word_ogjson = File.ReadAllText("worddata_og.json");
        var word_og = JsonSerializer.Deserialize<WordData>(word_ogjson);
        word = word_og.RemoveDuplicates();

        if (isWord_PS4AS)
        {
            string word_ps4json = File.ReadAllText("wordlist_ps4AS.json");
            var word_ps4 = JsonSerializer.Deserialize<WordData>(word_ps4json);
        
            word = word.Inject(word_ps4);
        }

        if (isWord_PS4US)
        {
            string word_ps4json = File.ReadAllText("wordlist_ps4US.json");
            var word_ps4 = JsonSerializer.Deserialize<WordData>(word_ps4json);

            word = word.Inject(word_ps4);
        }

        if (isWord_PTB)
        {
            string word_ptbjson = File.ReadAllText("wordlist_ptb.json");
            var word_ptb = JsonSerializer.Deserialize<WordData>(word_ptbjson);

            word = word.Inject(word_ptb);
        }

        if (isWord_NS1)
        {
            string word_ns1json = File.ReadAllText("wordlist_ns1.json");
            var word_ns1 = JsonSerializer.Deserialize<WordData>(word_ns1json);

            word = word.Inject(word_ns1);
        }

        if (isWord_MP)
        {
            string word_mpjson = File.ReadAllText("worddata_mp.json");
            var word_mp = JsonSerializer.Deserialize<WordData>(word_mpjson);

            word = word.Inject(word_mp);
        }

        wordjson = JsonSerializer.Serialize<WordData>(word, opt);
        File.WriteAllText(@"output\worddata.json", wordjson);
    }
} catch (Exception ex)
{
    Console.WriteLine("Unknown error:\n" + ex);
}
*/