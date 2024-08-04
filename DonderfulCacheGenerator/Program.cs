// See https://aka.ms/new-console-template for more information
using DonderfulUtils.Model;
using DonderfulUtils.Service;
using System.Collections.Generic;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Donderful Cache Generator\nDeveloped by Kamui (DespairOfHarmony)\nSpecial thanks: swigz\n");


try
{
    Directory.CreateDirectory("temp");
    Directory.CreateDirectory("output");

    var opt = new JsonSerializerOptions();
    opt.WriteIndented = true;
    opt.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

    Console.WriteLine("Write the romfs location: ");

    string path = Console.ReadLine();

    bool isMusic = File.Exists(path + "\\Data\\StreamingAssets\\assetbundlestreamingassets\\readassets\\musicdata.unity3d");
    bool isSong = File.Exists(path + "\\Data\\StreamingAssets\\assetbundlestreamingassets\\readassets\\songdata.unity3d");
    bool isFolder = Directory.Exists(path + "\\Data\\StreamingAssets");

    if (isMusic && isSong && isFolder)
    {
        Console.WriteLine("Loading: songdata.unity3d");
        File.Copy(path + "\\Data\\StreamingAssets\\assetbundlestreamingassets\\readassets\\songdata.unity3d", "songdata.unity3d");
        string songdatajson = Unity3D.ExtractJSONData("songdata");
        SongData songs = JsonSerializer.Deserialize<SongData>(songdatajson);

        Console.WriteLine("Loading: musicdata.unity3d");
        File.Copy(path + "\\Data\\StreamingAssets\\assetbundlestreamingassets\\readassets\\musicdata.unity3d", "musicdata.unity3d");
        string musicdatajson = Unity3D.ExtractJSONData("musicdata");
        MusicData music = JsonSerializer.Deserialize<MusicData>(musicdatajson);

        foreach (var item in music.items)
        {
            bool checkCSV = File.Exists(path + "\\Data\\StreamingAssets\\csv\\" + item.id + ".csv");
            bool checkACB = File.Exists(path + "\\Data\\StreamingAssets\\Sound\\song\\" + item.songFileName + ".acb");
            bool checkFumen = Directory.Exists(path + "\\Data\\StreamingAssets\\fumen\\" + item.id);

            if (checkCSV && checkACB && checkFumen)
            {
                Unity3D.CreateMPData(item, path);

                for (int i = 0; i < songs.items.Count; i++)
                {
                    if (songs.items[i].uniqueId == item.uniqueId)
                    {
                        songs.items[i].DLC = "HkX8sA53LnJi"; // Activate with "World is Mine" DLC
                        songs.items[i].HasInPackage = "2";
                        songs.items[i].Reserve3 = ""; // Update 1.3.0 set Reserve 3 as DLC key
                        songs.items[i].playable_region_list = "1,2,3";
                        songs.items[i].subscription_region_list = "1,2,3";
                        songs.items[i].dlc_region_list = "1,2,3";
                    }
                }
            }
        }

        songdatajson = JsonSerializer.Serialize<SongData>(songs, opt);
        Unity3D.WriteJSONData("songdata", songdatajson);
        Console.WriteLine(@"DLC Injection done, file saved at: output\songdata.unity3d" + "\n");

        File.Delete("songdata.unity3d");
        File.Delete("musicdata.unity3d");
    }

    Directory.Delete("temp", true);
} catch (Exception ex)
{
    Console.WriteLine("Unknown error:\n" + ex);
}
