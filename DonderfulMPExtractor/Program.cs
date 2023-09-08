// See https://aka.ms/new-console-template for more information
using System.Buffers.Text;
using System.Xml.Linq;
using DonderfulUtils.Service;

Console.WriteLine("Donderful Music Pass Extractor.\nDeveloped by Kamui (DespairOfHarmony)\n");

if (args.Length > 0)
{
    Directory.CreateDirectory("temp");
    Directory.CreateDirectory("output");

    foreach (string arg in args)
    {
        string filename = Path.GetFileName(arg);

        Console.WriteLine("Decoding: " + filename);
        Unity3D.ExtractMPData(filename);

        Console.WriteLine("All done! Decoded files saved at output folder.\n");

    }
    Directory.Delete("temp", true);
}
else
{
    Console.WriteLine("This will extract Music Pass Cache Data from Taiko no Tatsujin: Donderful Festival into a LayeredFS format.\nJust drag and drop the file into the exe.");
    Console.ReadLine();
}

//Console.ReadLine();
