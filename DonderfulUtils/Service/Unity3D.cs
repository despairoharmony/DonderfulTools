using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using AssetsTools.NET;
using AssetsTools.NET.Extra;
using DonderfulUtils.Encode;
using DonderfulUtils.Model;
using Microsoft.Win32.SafeHandles;

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
            var afile = asset.file;
            string m_Script = "";

            foreach (var texInfo in afile.GetAssetsOfType(AssetClassID.TextAsset))
            {
                var texBase = am.GetBaseField(asset, texInfo);
                if (texBase["m_Name"].AsString == filename)
                {
                    m_Script = texBase["m_Script"].AsString;
                }
            }

            am.UnloadAll();

            return m_Script;
        }
        public static void WriteJSONData(string filename, string jsondata)
        {
            GetDecodedFile(filename);
            var am = new AssetsManager();

            var bundle = am.LoadBundleFile(@"temp\" + filename + ".unity3d");
            var bun = bundle.file;
            var asset = am.LoadAssetsFileFromBundle(bundle, 0, true);
            var afile = asset.file;

            foreach (var texInfo in afile.GetAssetsOfType(AssetClassID.TextAsset))
            {
                var texBase = am.GetBaseField(asset, texInfo);
                
                if (texBase["m_Name"].AsString == filename)
                {
                    texBase["m_Script"].AsString = jsondata;
                    texInfo.SetNewData(texBase);
                }
            }

            bun.BlockAndDirInfo.DirectoryInfos[0].SetNewData(afile);

            using (AssetsFileWriter writer = new AssetsFileWriter(@"output\" + filename + "_unc.unity3d"))
            {
                bun.Write(writer);
            }

            am.UnloadAll();

            CompressBundle(filename);
        }

        public static void CompressBundle(string filename)
        {
            var am = new AssetsManager();

            var newUncompressedBundle = new AssetBundleFile();
            newUncompressedBundle.Read(new AssetsFileReader(File.OpenRead(@"output\" + filename + "_unc.unity3d")));

            using (AssetsFileWriter writer = new AssetsFileWriter(@"output\" + filename + ".unity3d"))
            {
                newUncompressedBundle.Pack(writer, AssetBundleCompressionType.LZ4);
            }

            newUncompressedBundle.Close();

            File.Delete(@"output\" + filename + "_unc.unity3d");
        }
        public static void CompressCache(string filename)
        {
            var am = new AssetsManager();

            var newUncompressedBundle = new AssetBundleFile();
            newUncompressedBundle.Read(new AssetsFileReader(File.OpenRead(@"output\" + filename + "_unc")));

            using (AssetsFileWriter writer = new AssetsFileWriter(@"output\" + filename))
            {
                newUncompressedBundle.Pack(writer, AssetBundleCompressionType.LZ4);
            }

            newUncompressedBundle.Close();

            File.Delete(@"output\" + filename + "_unc");
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
            var afile = asset.file;

            foreach (var texInfo in afile.GetAssetsOfType(AssetClassID.TextAsset))
            {
                string outname;

                var texBase = am.GetBaseField(asset, texInfo);
                var m_Name = texBase["m_Name"].AsString;
                var m_Script = texBase["m_Script"].AsByteArray;

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

            am.UnloadAll();
        }

        public static void CreateMPData(MusicItens music, string ultamixpath)
        {
            string cachefile, cacheid;
            if (music.starUra > 0)
            {
                cachefile = "0317_swtaiko_004";
                cacheid = "vt1op";
            }
            else
            {
                cachefile = "0690_swtaiko_001";
                cacheid = "adodo";
            }

            string filename = music.uniqueId.ToString("D4") + "_swtaiko_001";

            Console.WriteLine("Generating: " + filename);

            GetDecodedMusic(cachefile);

            var am = new AssetsManager();

            var bundle = am.LoadBundleFile(@"temp\" + cachefile);
            var bunfile = bundle.file;
            var asset = am.LoadAssetsFileFromBundle(bundle, 0, true);
            var afile = asset.file;

            foreach (var texInfo in afile.GetAssetsOfType(AssetClassID.TextAsset))
            {
                var texBase = am.GetBaseField(asset, texInfo);
                string m_Name = texBase["m_Name"].AsString;
                var m_Script = texBase["m_Script"].AsByteArray;

                if (m_Name.ToLower().Contains("song_"))
                {
                    texBase["m_Name"].AsString = Regex.Replace(m_Name, cacheid.ToUpper(), music.id.ToUpper());
                    texBase["m_Script"].AsByteArray = File.ReadAllBytes(ultamixpath + "\\Data\\StreamingAssets\\Sound\\song\\" + music.songFileName + ".acb");
                }
                else if (m_Name.ToLower().Contains("_e") || m_Name.ToLower().Contains("_n") || m_Name.ToLower().Contains("_h") || m_Name.ToLower().Contains("_m") || m_Name.ToLower().Contains("_x"))
                {
                    m_Name = Regex.Replace(m_Name, cacheid, music.id);
                    texBase["m_Name"].AsString = m_Name;

                    texBase["m_Script"].AsByteArray = File.ReadAllBytes(ultamixpath + "\\Data\\StreamingAssets\\fumen\\" + music.id + "\\" + m_Name + ".bin");
                }
                else
                {
                    texBase["m_Name"].AsString = Regex.Replace(m_Name, cacheid, music.id);
                    texBase["m_Script"].AsByteArray = File.ReadAllBytes(ultamixpath + "\\Data\\StreamingAssets\\csv\\" + music.id + ".csv");
                }

                texInfo.SetNewData(texBase);
            }

            foreach (var assetInfo in afile.GetAssetsOfType(AssetClassID.AssetBundle))
            {
                var assetBase = am.GetBaseField(asset, assetInfo);
                var containerBase = assetBase["m_Container.Array"].ToArray();
                foreach (var container in containerBase)
                {
                    container["first"].AsString = Regex.Replace(container["first"].AsString, cacheid, music.id);
                    container["first"].AsString = Regex.Replace(container["first"].AsString, "fromns1/", string.Empty);

                    //container.SetNewData(containerBase);
                }

                assetBase["m_Name"].AsString = filename + ".unity3d";
                assetBase["m_AssetBundleName"].AsString = filename + ".unity3d";

                assetInfo.SetNewData(assetBase);
            }

            Guid uuid = Guid.NewGuid();
            string uuidtext = uuid.ToString();
            uuidtext = Regex.Replace(uuidtext, "-", string.Empty);

            bunfile.BlockAndDirInfo.DirectoryInfos[0].SetNewData(afile);
            bunfile.BlockAndDirInfo.DirectoryInfos[0].Name = "CAB-" + uuidtext;

            using (AssetsFileWriter writer = new AssetsFileWriter(@"output\" + filename + "_unc"))
            {
                bunfile.Write(writer);
            }

            am.UnloadAll();

            CompressCache(filename);

            Console.WriteLine("Done!");
        }
    }
}
