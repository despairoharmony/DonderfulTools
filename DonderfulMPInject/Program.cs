using DonderfulUtils.Model;
using DonderfulMPInject.Inject;
using System.Text.Json;
using DonderfulUtils.Service;
using System.Xml.Linq;

Console.WriteLine("Donderful Music Pass Inject\nDeveloped by Kamui (DespairOfHarmony)\nPlace \"MySaveData\" at the same folder of this executable.\nYou can place \"songdata.unity3d\",\"musicdata.unity3d\" and \"worddata.unity3d\" in the same folder and this program\nwill inject the data from \"MySaveData\"\n");
Console.WriteLine("Press any key to continue.\n");
Console.ReadKey();

try
{
    bool isSaveData = File.Exists("MySaveData");
    bool isSaveJson = File.Exists("savedata.json");

    if (isSaveData || isSaveJson)
    {
        SaveData musicpass = new();
        if (isSaveData)
        {
            Console.WriteLine("Loading: MySaveData\n");
            byte[] save = File.ReadAllBytes("MySaveData");
            musicpass = MySaveData.GetSongData(save);
        } else if (isSaveJson)
        {
            Console.WriteLine("Loading: savedata.json");
            string savedatajson = File.ReadAllText("savedata.json");
            musicpass = JsonSerializer.Deserialize<SaveData>(savedatajson);
        }

        Directory.CreateDirectory("output");
        Directory.CreateDirectory("temp");

        var opt = new JsonSerializerOptions();
        opt.WriteIndented = true;
        opt.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

        bool isSong = File.Exists("songdata.unity3d");
        bool isMusic = File.Exists("musicdata.unity3d");
        bool IsWord = File.Exists("worddata.unity3d");

        if (isSong)
        {
            Console.WriteLine("Loading: songdata.unity3d");
            string songdatajson = Unity3D.ExtractJSONData("songdata");
            SongData songs = JsonSerializer.Deserialize<SongData>(songdatajson);

            Console.WriteLine("Converting: songdata.unity3d");
            songs = Song.Inject(songs, musicpass);
            songs.items = songs.items.OrderBy(x => x.uniqueId).ToList();

            songdatajson = JsonSerializer.Serialize<SongData>(songs, opt);
            Unity3D.WriteJSONData("songdata", songdatajson);
            Console.WriteLine(@"Injection done, file saved at: output\songdata.unity3d" + "\n");
        }
        else
        {
            Console.WriteLine("File not found: songdata.unity3d\n");
        }

        if (isMusic)
        {
            Console.WriteLine("Loading: musicdata.unity3d");
            string musicdatajson = Unity3D.ExtractJSONData("musicdata");
            MusicData music = JsonSerializer.Deserialize<MusicData>(musicdatajson);

            Console.WriteLine("Converting: musicdata.unity3d");
            music = Music.Inject(music, musicpass);
            music.items = music.items.OrderBy(x => x.uniqueId).ToList();

            musicdatajson = JsonSerializer.Serialize<MusicData>(music, opt);
            Unity3D.WriteJSONData("musicdata", musicdatajson);
            Console.WriteLine(@"Injection done, file saved at: output\musicdata.unity3d" + "\n");
        }
        else
        {
            Console.WriteLine("File not found: musicdata.unity3d\n");
        }

        if (IsWord)
        {
            Console.WriteLine("Loading: worddata.unity3d");
            string worddatajson = Unity3D.ExtractJSONData("worddata");
            WordData dicionary = JsonSerializer.Deserialize<WordData>(worddatajson);

            Console.WriteLine("Converting: worddata.unity3d");
            dicionary = Word.Inject(dicionary, musicpass);

            worddatajson = JsonSerializer.Serialize<WordData>(dicionary, opt);
            Unity3D.WriteJSONData("worddata", worddatajson);
            Console.WriteLine(@"Injection done, file saved at: output\worddata.unity3d" + "\n");
        }
        else
        {
            Console.WriteLine("File not found: worddata.unity3d\n");
        }
        if (isSaveData)
        {

            string passjson = JsonSerializer.Serialize<SaveData>(musicpass, opt);
            File.WriteAllText(@"output\savedata.json", passjson);
        }

        Directory.Delete("temp", true);
    }
    else
    {
        Console.WriteLine("File not found: MySaveData\nThis program cannot progress.");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Unknown error:\n" + ex);
}

// Keep the console window open in debug mode.
Console.WriteLine("Press any key to exit.");
Console.ReadKey();
