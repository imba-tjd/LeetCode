using System;
using System.IO;

namespace Gen
{
    class Program
    {
        static void Main(string[] args) => new Generator(args).Run();
    }
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

            serialNumber = default;
            body = default;
            isForceCreate = default;
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

