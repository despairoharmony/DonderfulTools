// See https://aka.ms/new-console-template for more information
using System.Buffers.Text;
using DonderfulUtils.Service;

Console.WriteLine("Donderful JSON Extractor.\nDeveloped by Kamui (DespairOfHarmony)\n");

if (args.Length > 0)
{
    Directory.CreateDirectory("temp");

    foreach (string arg in args)
    {
        string filename = arg;
        string name = Path.GetFileNameWithoutExtension(filename);
        string ext = Path.GetExtension(filename);

        if (ext == ".unity3d")
        {
            Console.WriteLine("Decoding: " + name + ".unity3d");
            string jsondata = Unity3D.ExtractJSONData(name);
            File.WriteAllText(name + ".json", jsondata);
            Console.WriteLine("All done! Decoded file saved at: " + name + ".json\n");
        } else if (ext == ".json")
        {
            Console.WriteLine("Checking if file " + name + ".unity3d exists.");
            bool isBundle = File.Exists("songdata.unity3d");
            if (isBundle)
            {
                Directory.CreateDirectory("output");
                string jsondata = File.ReadAllText(filename);
                Console.WriteLine("Encoding: " + name + ".json");
                Unity3D.WriteJSONData(name, jsondata);
                Console.WriteLine(@"All done! Encoded file saved at: output\" + name + ".unity3d\n");
            } else
            {
                Console.WriteLine(name + ".unity3d is missing, skipping file.");
            }
        } else
        {
            Console.WriteLine("File extension: " + ext + " not supported.");
        }
    }
    Directory.Delete("temp", true);
}
else
{
    Console.WriteLine("This will decode the .unity3d file from 'assetbundlestreamingassets' into a .json,\nAnd also inject the modfied .json into the .unity3d of origin.\nThis tool is meant for Taiko no Tatsujin: Donderful Festival.\nJust drag and drop the file into the exe.");
    Console.ReadLine();
}

//Console.ReadLine();
