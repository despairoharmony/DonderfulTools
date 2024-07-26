// See https://aka.ms/new-console-template for more information
using DonderfulUtils.Model;
using DonderfulUtils.Service;
using System.Collections.Generic;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Donderful Asset Duplicator\nDeveloped by Kamui (DespairOfHarmony)\nSpecial thanks: swigz\n");


try
{
    Directory.CreateDirectory("temp");
    Directory.CreateDirectory("output");

    bool isFolder = Directory.Exists("assetbundleresources");

    if (isFolder)
    {
        Console.WriteLine("Write the type of asset to duplicate (acc/body/cos/head): ");

        string type = Console.ReadLine();

        if (type == "acc")
        {
            Console.WriteLine("Type the Base ID to clone: ");

            string id_base = Console.ReadLine();
            bool next = true;

            while (next)
            {
                Console.WriteLine("Type the New ID: (Or leave empty to stop)");

                string id_new = Console.ReadLine();

                if (string.IsNullOrEmpty(id_new))
                {
                    next = false;
                    break;
                }

                string filename_base = type + "_" + id_base;
                string filename_new = type + "_" + id_new;

                string[] paths = {
                    "assetbundleresources\\container\\" + type + "\\",
                    "assetbundleresources\\container\\ninja\\" + type + "\\",
                    "assetbundleresources\\container\\reduction\\" + type + "\\",
                    "assetbundleresources\\container\\session\\" + type + "\\",
                    "assetbundleresources\\spriteatlases\\enso_chara\\player_chara_"+ type + "\\",
                    "assetbundleresources\\textures\\dressup\\"+ type + "\\"
                };

                bool isTexture;
                foreach (string path in paths)
                {
                    isTexture = path.Contains("textures");
                    Unity3D.CreateCostumeAsset(path, filename_base, filename_new, isTexture);
                }
            }
            
        } else
        {
            Console.WriteLine("Type not supported");
        }
    }

    Directory.Delete("temp", true);
}
catch (Exception ex)
{
    Console.WriteLine("Unknown error:\n" + ex);
}
