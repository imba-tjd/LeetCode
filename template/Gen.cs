using System;
using System.IO;

namespace Gen
{
    class Gen
    {
        const string csFileTemplatePath = "template/Solution.cs.template";
        const string readmeFileTemplatePath = "template/Readme.md.template";
        const string urlBase = "https://leetcode.com/problems/";
        static bool ForceCreate = false;

        static int Main(string[] args) // 1. Two Sum
        {
            if (args.Length == 0 || args[0] == "/h")
            {
                Console.WriteLine("Usage: `./Gen.exe [/f] 1. Two Sum`");
                return 1;
            }
            else if (args[0] == "/f")
                ForceCreate = true;

            string index = args[ForceCreate ? 1 : 0]; // 1.
            string[] body = SubArray(args, ForceCreate ? 2 : 1); // Two Sum

            string csFileName = string.Join("-", body).ToLowerInvariant(); // two-sum
            string folderName = index.PadLeft(4, '0') + csFileName; // 001.two-sum

            if (Directory.Exists(folderName))
                if (ForceCreate)
                    Directory.Delete(folderName, true); // 如果目标不存在会抛异常
                else
                    throw new IOException(folderName + " already exists!");
            Directory.CreateDirectory(folderName);
            string csFileRealPath = Path.Combine(folderName, csFileName + ".cs"); // two-sum.cs
            string readmeFileRealPath = Path.Combine(folderName, "Readme.md");

            string ns = string.Join("", body); // TwoSum
            string csFileContent = File.ReadAllText(csFileTemplatePath).Replace("<NS>", ns);
            string readmeFileContent = File.ReadAllText(readmeFileTemplatePath)
                .Replace("<Problem>", index + " " + string.Join(" ", body))
                .Replace("<URL>", urlBase + csFileName);

            WriteContent(csFileRealPath, csFileContent);
            WriteContent(readmeFileRealPath, readmeFileContent);

            return 0;
        }

        static T[] SubArray<T>(T[] src, int index) => SubArray(src, index, src.Length - index);
        static T[] SubArray<T>(T[] src, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(src, index, result, 0, length);
            return result;
        }
        static void WriteContent(string path, string content)
        {
            if (File.Exists(path))
                throw new IOException(path + " already exists!");
            File.WriteAllText(path, content);
        }
    }
}
