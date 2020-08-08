using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HZDPackTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string packedDir = @"E:\SteamLibrary\SteamApps\common\Horizon Zero Dawn\Packed_DX12";
            Console.WriteLine(packedDir);
            string[] idxFiles = Directory.GetFiles(packedDir, "*.idx");

            if (File.Exists("hzd_fileList.csv")) File.Delete("hzd_fileList.csv");
            File.WriteAllText("hzd_fileList.csv", "container,filePath\n");

            foreach (string idxFile in idxFiles)
            {
                Console.WriteLine(idxFile);
                string[] files = GetAllFilesFromIdx(idxFile);
                File.AppendAllLines("hzd_fileList.csv", files);
            }
            Console.Read();
        }

        static string[] GetAllFilesFromIdx(string idxFile)
        {
            string idxName = Path.GetFileNameWithoutExtension(idxFile);
            List<string> files = new List<string>();

            using (BinaryReader idxReader = new BinaryReader(File.OpenRead(idxFile)))
            {
                idxReader.BaseStream.Seek(4, SeekOrigin.Begin); // Skip header
                int fileCount = idxReader.ReadInt32();

                for (int i = 0; i < fileCount; i++)
                {
                    int fileNameLength = idxReader.ReadInt32();
                    string fileName = Helper.ByteArrayToString(idxReader.ReadBytes(fileNameLength));
                    idxReader.BaseStream.Seek(32, SeekOrigin.Current); // Skip other data to next entry
                    files.Add($"{idxName},{fileName}");
                }
            }

            return files.ToArray();
        }
    }
}
