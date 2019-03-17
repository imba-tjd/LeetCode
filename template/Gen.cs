using System;
using System.IO;

namespace Gen
{
    class Gen
    {
        const string csTempleteFileName = "templete/Solution.cs.templete";
        const string readmeTempleteFileName = "templete/Readme.md.templete";
        static bool ForceCreate = false;

        static int Main(string[] args) // 0. Two Sum
        {
            if (args.Length == 0 || args[0] == "/h")
            {
                Console.WriteLine("Usage: `./Gen.exe 0. Two Sum`");
                return 1;
            }
            else if (args[0] == "/f")
                ForceCreate = true;

            // string[] body = new string[args.Length - 1 - (ForceCreate ? 1 : 0)]; // Two Sum
            // CopyStringArray(args, 1 + (ForceCreate ? 1 : 0), body);
            string[] body = SubArray(args, ForceCreate ? 2 : 1);

            string csFileName = string.Join("-", body).ToLowerInvariant(); // two-sum
            string folderName = args[ForceCreate ? 1 : 0] + csFileName; // 0.two-sum
            string ns = string.Join("", body); // TwoSum

            if (Directory.Exists(folderName))
                if (ForceCreate)
                    Directory.Delete(folderName, true); // 如果目标不存在会抛异常
                else
                    throw new IOException(folderName + " already exists!");
            Directory.CreateDirectory(folderName);
            string csFileRealPath = Path.Combine(folderName, csFileName + ".cs"); // two-sum.cs
            string readmeFileRealPath = Path.Combine(folderName, "Readme.md");

            string csFileContent = File.ReadAllText(csTempleteFileName);
            csFileContent = csFileContent.Replace("<NS>", ns);
            string readmeFileContent = File.ReadAllText(readmeTempleteFileName);
            readmeFileContent = readmeFileContent.Replace("<Problem>", string.Join(" ", args));

            if (File.Exists(csFileRealPath))
                throw new IOException(csFileRealPath + " already exists!");
            File.WriteAllText(csFileRealPath, csFileContent);
            if (File.Exists(readmeFileRealPath))
                throw new IOException(readmeFileRealPath + " already exists!");
            File.WriteAllText(readmeFileRealPath, readmeFileContent);

            return 0;
        }
        // static void CopyStringArray(string[] src, int from, string[] dest)
        // {
        //     for (int i = 0; i < src.Length - from; i++)
        //         dest[i] = src[i + from];
        // }
        static T[] SubArray<T>(T[] src, int index) => SubArray(src, index, src.Length - index);
        static T[] SubArray<T>(T[] src, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(src, index, result, 0, length);
            return result;
        }
    }
}
