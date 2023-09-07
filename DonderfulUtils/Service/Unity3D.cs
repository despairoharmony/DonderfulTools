using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using AssetsTools.NET;
using AssetsTools.NET.Extra;
using DonderfulUtils.Encode;

namespace DonderfulUtils.Service
{
    public class Unity3D
    {
        public static string ExtractJSONData(string filename)
        {
            GetDecodedFile(filename);
            var am = new AssetsManager();

            var bundle = am.LoadBundleFile(@"temp\" + filename + ".unity3d");
            var asset = am.LoadAssetsFileFromBundle(bundle, 0, true);
            var tbl = asset.table;
            var info = tbl.GetAssetInfo(filename);
            var baseField = am.GetTypeInstance(asset, info).GetBaseField();
            var m_Script = baseField["m_Script"].value.AsString();

            am.UnloadAll();

            return m_Script;
        }
        public static void WriteJSONData(string filename, string jsondata)
        {
            GetDecodedFile(filename);
            var am = new AssetsManager();

            var bundle = am.LoadBundleFile(@"temp\" + filename + ".unity3d");
            var asset = am.LoadAssetsFileFromBundle(bundle, 0, true);
            var assetname = bundle.file.GetFileName(0);
            var tbl = asset.table;
            var info = tbl.GetAssetInfo(filename);
            var baseField = am.GetTypeInstance(asset, info).GetBaseField();
            baseField.Get("m_Script").GetValue().Set(jsondata);

            var newGoBytes = baseField.WriteToByteArray();
            var repl = new AssetsReplacerFromMemory(0, info.index, (int)info.curFileType, 0xffff, newGoBytes);

            //write changes to memory
            byte[] newAssetData;
            using (var stream = new MemoryStream())
            using (var writer = new AssetsFileWriter(stream))
            {
                asset.file.Write(writer, 0, new List<AssetsReplacer>() { repl }, 0);
                newAssetData = stream.ToArray();
            }

            var bunRepl = new BundleReplacerFromMemory(assetname, null, true, newAssetData, -1);
            using (var bunWriter = new AssetsFileWriter(File.OpenWrite(@"output\" + filename + "_unc.unity3d")))
                bundle.file.Write(bunWriter, new List<BundleReplacer>() { bunRepl });

            am.UnloadAll();

            CompressBundle(filename);
        }

        public static void CompressBundle(string filename)
        {
            var am = new AssetsManager();
            var bun = am.LoadBundleFile(@"output\" + filename + "_unc.unity3d");
            using (var stream = File.OpenWrite(@"output\" + filename + ".unity3d"))
            using (var writer = new AssetsFileWriter(stream))
            {
                bun.file.Pack(bun.file.reader, writer, AssetBundleCompressionType.LZ4);
            }

            am.UnloadAll();
            File.Delete(@"output\" + filename + "_unc.unity3d");
        }

        private static void GetDecodedFile(string filename)
        {
            byte[] fullfile = File.ReadAllBytes(filename + ".unity3d");
            if (XOR.ValidateEncr(fullfile))
            {
                (byte[] key, byte[] decfile) = Key.Get(fullfile);
                decfile = XOR.Decrypt(decfile, key);
                File.WriteAllBytes(@"temp\" + filename + ".unity3d", decfile);
            }
            else
            {
                File.Copy(filename + ".unity3d", @"temp\" + filename + ".unity3d", overwrite: true);
            }
        }

        private static void GetDecodedMusic(string filename)
        {
            byte[] fullfile = File.ReadAllBytes(filename);
            if (XOR.ValidateEncr(fullfile))
            {
                (byte[] key, byte[] decfile) = Key.Get(fullfile);
                decfile = XOR.Decrypt(decfile, key);
                File.WriteAllBytes(@"temp\" + filename, decfile);
            }
            else
            {
                File.Copy(filename, @"temp\" + filename);
            }
        }

        public static void ExtractMPData(string filename)
        {
            GetDecodedMusic(filename);
            var am = new AssetsManager();

            var bundle = am.LoadBundleFile(@"temp\" + filename);
            var asset = am.LoadAssetsFileFromBundle(bundle, 0, true);
            var tbl = asset.table;

            foreach (var info in tbl.assetFileInfo)
            {
                string outname;

                var baseField = am.GetTypeInstance(asset, info).GetBaseField();
                var m_Name = baseField.Get("m_Name").GetValue().AsString();
                if (!m_Name.Contains(".unity3d"))
                {
                    var m_Script = baseField["m_Script"].value.AsStringBytes();

                    if (m_Name.ToLower().Contains("psong_"))
                    {
                        m_Name = Regex.Replace(m_Name.ToUpper(), "_ACB", string.Empty);
                        Directory.CreateDirectory(@"output\presong");
                        outname = @"output\presong\" + m_Name + ".acb";
                    }
                    else if (m_Name.ToLower().Contains("song_"))
                    {
                        m_Name = Regex.Replace(m_Name.ToUpper(), "_ACB", string.Empty);
                        Directory.CreateDirectory(@"output\song");
                        outname = @"output\song\" + m_Name + ".acb";
                    }
                    else if (m_Name.ToLower().Contains("_e") || m_Name.ToLower().Contains("_n") || m_Name.ToLower().Contains("_h") || m_Name.ToLower().Contains("_m") || m_Name.ToLower().Contains("_x"))
                    {
                        string id_name = m_Name.Split("_").First();
                        Directory.CreateDirectory(@"output\fumen\" + id_name);
                        outname = @"output\fumen\" + id_name + @"\" + m_Name + ".bin";
                    }
                    else
                    {
                        Directory.CreateDirectory(@"output\csv");
                        outname = @"output\csv\" + m_Name + ".csv";
                    }
                        

                    File.WriteAllBytes(outname, m_Script);
                }
                
            }

            am.UnloadAll();
        }
    }
}
