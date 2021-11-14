using System;
using System.IO;

#if NDEBUG
using Leetcode.Tools.Gen;
class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
            Console.WriteLine("Generate basic code files.");
        else if (args[0][0] == '/')
            Console.WriteLine("There is no flags.");
        else
        {
            var ctx = new NameContext(args);
            var fg = new FileGenerator(ctx);
            fg.CreateFolder();
            fg.CreateFiles();
        }
    }
}
#endif

namespace Leetcode.Tools.Gen
{
    public class NameContext
    {
        public int SerialNumber { get; }
        public string[] Body { get; }
        public NameContext(string[] args) // 例如 1. Two Sum (extra)
        {
            if (args.Length < 2 || args[0][args[0].Length - 1] != '.')
                throw new ArgumentException("Missing Serial Number or body or both.", nameof(args));
            SerialNumber = int.Parse(args[0].TrimEnd('.')); // 1
            if (SerialNumber <= 0)
                throw new ArgumentException("Invalid Serial Number.", nameof(args));
            Body = args.SubArray(1).SelectInPlace(x => x.Replace("(", "").Replace(")", "").Replace(",", "")); // Two Sum extra
        }
        public NameContext(int serialNum, string[] body) => (SerialNumber, Body) = (serialNum, body);
        public string SerialNumberString => SerialNumber.ToString("000");
        public string URLSuffix => string.Join("-", Body).ToLower(); // two-sum-extra
        public string FolderName => SerialNumberString + "." + URLSuffix; // 001.two-sum-extra
        string CsFileName => "Code.cs"; // URLSuffix + ".cs" -> two-sum-extra.cs；现在决定就用Code.cs
        public string CsFilePath => FolderName + "/" + CsFileName;
        public string ReadmeFilePath => FolderName + "/" + "Readme.md";
        public string NameSpace => string.Join("", Body);
        const string URLBase = "https://leetcode.com/problems/";
        public string URL => URLBase + URLSuffix + "/";
        public string ProblemTitle => SerialNumber + " " + string.Join(" ", Body);
    }

    public class FileGenerator
    {
        const string CsTemplatePath = "template/Code.cs.t";
        const string ReadmeTemplatePath = "template/Readme.md.t";

        NameContext NC { get; }
        public FileGenerator(NameContext ctx) => NC = ctx;

        public void CreateFolder()
        {
            if (Directory.Exists(NC.FolderName))
                throw new IOException(NC.FolderName + " already exists!");
            Directory.CreateDirectory(NC.FolderName);
        }
        public void CreateFiles()
        {
            string csFileContent = File.ReadAllText(CsTemplatePath)
                .Replace("<NS>", NC.NameSpace).Replace("<SN>", NC.SerialNumberString);
            string readmeFileContent = File.ReadAllText(ReadmeTemplatePath)
                .Replace("<Problem>", NC.ProblemTitle).Replace("<URL>", NC.URL);
            File.WriteAllText(NC.CsFilePath, csFileContent);
            File.WriteAllText(NC.ReadmeFilePath, readmeFileContent);
        }
    }

    internal static class DNFCompatibleExpansion
    {
        internal static T[] SubArray<T>(this T[] src, int index) => SubArray(src, index, src.Length - index);
        internal static T[] SubArray<T>(this T[] src, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(src, index, result, 0, length);
            return result;
        }
        internal static T[] SelectInPlace<T>(this T[] src, Func<T, T> selector)
        {
            for (int i = 0; i < src.Length; i++)
                src[i] = selector(src[i]);
            return src;
        }
    }
}

#if !NDEBUG
namespace Leetcode.Tools.Gen.Test
{
    using System.Collections.Generic;
    using Xunit;
    using System.Linq;
    public class NameContextTest
    {
        static readonly IReadOnlyList<(string, string)> URLSuffixTestCases = new[] {
            ("Sqrt(x)", "sqrtx"),
            ("Pow(x, n)", "powx-n"),
            ("Implement strStr()", "implement-strstr"),
            ("String to Integer (atoi)", "string-to-integer-atoi"),
            ("Implement Trie (Prefix Tree)", "implement-trie-prefix-tree")
        };

        public static IEnumerable<object[]> URLSuffixTestData()
        {
            foreach (var item in URLSuffixTestCases)
                yield return new object[] { item.Item1.Split(' ').Prepend("1."), item.Item2 };
        }

        [Theory]
        [MemberData(nameof(URLSuffixTestData))]
        public void URLSuffixTest(string[] args, string expect)
        {
            var ctx = new NameContext(args);
            Assert.Equal(expect, ctx.URLSuffix);
        }

        [Theory]
        [InlineData(new[] { "1.", "" }, "001")]
        [InlineData(new[] { "11.", "" }, "011")]
        [InlineData(new[] { "111.", "" }, "111")]
        [InlineData(new[] { "1111.", "" }, "1111")]
        public void SerialNumberStringTest(string[] args, string expect)
        {
            var ctx = new NameContext(args);
            Assert.Equal(expect, ctx.SerialNumberString);
        }

        [Fact]
        public void ErrorTest()
        {
            Assert.Throws<ArgumentException>(() => new NameContext(new string[] { }));
            Assert.Throws<ArgumentException>(() => new NameContext(new[] { "1." }));
            Assert.Throws<ArgumentException>(() => new NameContext(new[] { "asdf", "" }));
            Assert.Throws<FormatException>(() => new NameContext(new[] { "1asdf.", "" }));
        }
    }
}
#endif
