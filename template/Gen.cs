using System;
using System.IO;

namespace Gen
{
#if !DEBUG
    class Program
    {
        static void Main(string[] args) => new Generator(args).Run();
    }
#endif
    class Generator
    {
        const string csTemplatePath = "template/Solution.cs.template";
        const string readmeTemplatePath = "template/Readme.md.template";
        const string urlBase = "https://leetcode.com/problems/";
        bool IsForceCreate = false;
        string[] Args { get; }
        public Generator(string[] args) => Args = args;

        public int Run() // 1. Two Sum (extra)
        {
            if (0 != HandleArgs(out IsForceCreate, out string serialNum, out string[] body))
                return 1;

            string urlSuffix = string.Join("-", body).ToLowerInvariant(); // two-sum
            string folderName = serialNum?.PadLeft(4, '0') + urlSuffix; // 001.two-sum，序号不存在时不会有pading的0
            string csFilePath = Path.Combine(folderName, urlSuffix + ".cs"); // two-sum.cs
            string readmeFilePath = Path.Combine(folderName, "Readme.md");
            string ns = string.Join("", body); // TwoSum

            if (Directory.Exists(folderName))
                if (IsForceCreate)
                    Directory.Delete(folderName, true); // 如果目标不存在会抛异常，所以必须先判断
                else
                    throw new IOException(folderName + " already exists!");
            Directory.CreateDirectory(folderName);

            string csFileContent = File.ReadAllText(csTemplatePath).Replace("<NS>", ns);
            string readmeFileContent = File.ReadAllText(readmeTemplatePath)
                .Replace("<Problem>",
                    serialNum +
                    string.Join(" ", body)
                )
                .Replace("<URL>", urlBase + urlSuffix + "/");

            WriteContent(csFilePath, csFileContent);
            WriteContent(readmeFilePath, readmeFileContent);

            return 0;
        }

        void WriteContent(string path, string content)
        {
            if (File.Exists(path))
                throw new IOException(path + " already exists!");
            File.WriteAllText(path, content);
        }
        int HandleArgs(out bool isForceCreate, out string serialNumber, out string[] body)
        {
            int ShowError(ArgumentContext cont)
            {
                Console.WriteLine(cont.ErrorMessage);
                return (int)cont.ErrorCode;
            }

            serialNumber = null;
            body = null;
            isForceCreate = false;
            var context = new ArgumentContext(Args);

            bool? fc = context.ParseForceCreate();
            if (fc == null)
                return ShowError(context);
            isForceCreate = fc.Value;

            serialNumber = context.ParseSerialNumber();
            if (serialNumber == null)
                return ShowError(context);
            else if (serialNumber == string.Empty)
                serialNumber = null;

            body = context.ParseBody();
            if (body == null)
                return ShowError(context);
            return 0;
        }
    }
    enum ErrorType
    {
        Normal = 0,
        ShowUsage = 0,
        InvalidFlag,
        InvalidSerialNumber,
        MissingBody
    }
    class ArgumentContext
    {
        static System.Collections.Generic.Dictionary<ErrorType, string> ErrorMessages =
            new System.Collections.Generic.Dictionary<ErrorType, string>()
            {
                {ErrorType.ShowUsage, "Usage: ./Gen.exe [/f] 1. Two Sum"},
                {ErrorType.InvalidFlag, "Invalid Flag: "},
                {ErrorType.InvalidSerialNumber, "Invalid Serial Number: "},
                {ErrorType.MissingBody, "Missing Question Body!"}
            };
        internal ErrorType ErrorCode { get; private set; }
        internal string ErrorMessage => ErrorMessages[ErrorCode];
        string[] Args { get; }
        int _position;

        internal ArgumentContext(string[] args) => Args = args;
        internal bool? ParseForceCreate()
        {
            if (Args.Length == 0 || Args[0] == "/h")
            {
                Console.WriteLine();
                ErrorCode = ErrorType.Normal;
                return null;
            }
            else if (Args[0] == "/f")
            {
                _position++;
                return true;
            }
            else if (Args[0][0] == '/')
            {
                Console.WriteLine("Invalid Argument: " + Args[0]);
                ErrorCode = ErrorType.InvalidFlag;
                return null;
            }
            return false;
        }
        internal string ParseSerialNumber()
        {
            string SetError()
            {
                ErrorCode = ErrorType.InvalidSerialNumber;
                return null;
            }

            if (_position == Args.Length)
                return string.Empty;
            ref string number = ref Args[_position];
            if (!char.IsNumber(number[0])) // number不存在
                return string.Empty;
            for (int i = 1; i < number.Length - 1; i++)
                if (!char.IsNumber(number[i])) // 有非数字字符
                    return SetError();
            if (number[number.Length - 1] != '.')
                return SetError();
            _position++;
            return number;
        }
        internal string[] ParseBody()
        {
            if (_position == Args.Length)
            {
                ErrorCode = ErrorType.MissingBody;
                return null;
            }
            return Args.SubArray(_position).Select(x =>
            x.Replace("(", string.Empty)
            .Replace(")", string.Empty)
            .Replace(",", string.Empty)
            );
        }

        // string ParseExtra() {
        //     ref string last = ref Args[Args.Length - 1];
        //     extra = last[0] == '(' ? last.Substring(1, last.Length - 2) : string.Empty; // extra
        // }
    }
    internal static class GenExpansion
    {
        internal static T[] SubArray<T>(this T[] src, int index) => SubArray(src, index, src.Length - index);
        internal static T[] SubArray<T>(this T[] src, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(src, index, result, 0, length);
            return result;
        }
        internal static T[] Select<T>(this T[] src, Func<T, T> selector)
        {
            T[] result = new T[src.Length];
            for (int i = 0; i < src.Length; i++)
                result[i] = selector(src[i]);
            return result;
        }
    }
}
#if DEBUG
namespace Gen.Test
{
    using System.Collections.Generic;
    using Xunit;
    using System.Reflection;
    public class ArgumentContextTest
    {
        static Dictionary<string, string> BodyTestCases = new Dictionary<string, string>()
        {
            { "Sqrt(x)", "sqrtx" },
            { "Pow(x, n)", "powx-n" },
            { "Implement strStr()", "implement-strstr" },
            { "String to Integer (atoi)", "string-to-integer-atoi" },
            { "Implement Trie (Prefix Tree)", "implement-trie-prefix-tree" }
        };

        public static IEnumerable<object[]> TestData()
        {
            foreach (var item in BodyTestCases)
                yield return new object[] { item.Key.Split(' '), item.Value };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void BodyTest(string[] args, string expect)
        {
            var context = new ArgumentContext(args);
            string[] result = context.ParseBody();
            string result2 = string.Join('-', result).ToLowerInvariant();
            Assert.Equal(expect, result2);
        }
    }
    public class ErrorTest
    {
        static Dictionary<string, ErrorType> TestCases = new Dictionary<string, ErrorType>()
        {
            { "asdf", ErrorType.Normal },
            { "/f asdf", ErrorType.Normal },
            { "/f 1. asdf", ErrorType.Normal },
            { "1. asdf", ErrorType.Normal },
            { "/h", ErrorType.ShowUsage },

            { "/a", ErrorType.InvalidFlag },
            { "/", ErrorType.InvalidFlag },
            { "1asdf", ErrorType.InvalidSerialNumber },
            { "/f 1asdf", ErrorType.InvalidSerialNumber },
            { "1.", ErrorType.MissingBody },
            { "/f", ErrorType.MissingBody },
            { "/f 1.", ErrorType.MissingBody },
        };

        public static IEnumerable<object[]> TestData()
        {
            foreach (var item in TestCases)
                yield return new object[] { item.Key.Split(' '), (int)item.Value };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(string[] args, int expect)
        {
            var gen = new Generator(args);
            var fun = gen.GetType().GetMethod("HandleArgs", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(fun);
            int result = (int)fun.Invoke(gen, new object[] { null, null, null });
            Assert.Equal(expect, result);
        }
    }
}
#endif
