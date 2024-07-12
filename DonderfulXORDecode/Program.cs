// See https://aka.ms/new-console-template for more information
using System.Buffers.Text;
using DonderfulUtils.Encode;
using DonderfulUtils.Service;

Console.WriteLine("Donderful XOR Decoder and Encoder.\nDeveloped by Kamui (DespairOfHarmony)\nSpecial thanks: swigz\n");

if (args.Length > 0)
{
    foreach (string arg in args)
    {
        string filename = arg;

        Console.WriteLine("Decoding: " + filename);

        byte[] fullfile = File.ReadAllBytes(filename);

        if (XOR.ValidateEncr(fullfile))
        {
            (byte[] key, byte[] decfile) = Key.Get(fullfile);

            decfile = XOR.Decrypt(decfile, key);

            if (Path.GetExtension(filename) == ".bin")
            {
                decfile = B64.Decode(decfile);
            }

            Directory.CreateDirectory("output");

            string nfilename = String.Concat(@"output\" + Path.GetFileName(filename));

            File.WriteAllBytes(nfilename, decfile);

            Console.WriteLine("All done! Decoded file saved at: " + nfilename);
        } else
        {
            Random rnd = new Random();
            byte[] newkey = new byte[4];
            rnd.NextBytes(newkey);

            byte[] encfile = XOR.Encrypt(fullfile, newkey);

            Directory.CreateDirectory("output");

            string nfilename = String.Concat(@"output\" + Path.GetFileName(filename));

            File.WriteAllBytes(nfilename, encfile);

            Console.WriteLine("All done! Encoded file saved at: " + nfilename);
        }
        
    }

}
else
{
    Console.WriteLine("This will XOR'd (and decode Base64 from .bin) from any file of Taiko no Tatsujin: Donderful Festival.\nJust drag and drop the file into the exe.");
    Console.ReadLine();
}

//Console.ReadLine();
