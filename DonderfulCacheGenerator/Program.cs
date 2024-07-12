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

    bool isMusic = File.Exists("musicdata.json");
    bool isSong = File.Exists("songdata.json");
    bool isFolder = Directory.Exists(path + "\\Data\\StreamingAssets");

    if (isMusic && isSong && isFolder)
    {
        Console.WriteLine("Loading: songdata.json");
        string songdatajson = File.ReadAllText("songdata.json");
        SongData song = JsonSerializer.Deserialize<SongData>(songdatajson);

        Console.WriteLine("Loading: musicdata.json");
        string musicjson = File.ReadAllText("musicdata.json");
        MusicData music = JsonSerializer.Deserialize<MusicData>(musicjson);

        foreach (var item in music.items)
        {
            bool checkCSV = File.Exists(path + "\\Data\\StreamingAssets\\csv\\" + item.id + ".csv");
            bool checkACB = File.Exists(path + "\\Data\\StreamingAssets\\Sound\\song\\" + item.songFileName + ".acb");
            bool checkFumen = Directory.Exists(path + "\\Data\\StreamingAssets\\fumen\\" + item.id);

            if (checkCSV && checkACB && checkFumen)
            {
                Unity3D.CreateMPData(item, path);

                for (int i = 0; i < song.items.Count; i++)
                {
                    if (song.items[i].uniqueId == item.uniqueId)
                    {
                        song.items[i].DLC = "HkX8sA53LnJi"; // Activate with "World is Mine" DLC
                        song.items[i].HasInPackage = "2";
                        song.items[i].Reserve3 = ""; // Update 1.3.0 set Reserve 3 as DLC key
                        song.items[i].playable_region_list = "1,2,3";
                        song.items[i].subscription_region_list = "1,2,3";
                        song.items[i].dlc_region_list = "1,2,3";
                    }
                }
            }
        }

        Console.WriteLine(@"Saving: output\songdata.json");
        songdatajson = JsonSerializer.Serialize<SongData>(song, opt);
        File.WriteAllText(@"output\songdata.json", songdatajson);
    }

    Directory.Delete("temp", true);
} catch (Exception ex)
{
    Console.WriteLine("Unknown error:\n" + ex);
}
