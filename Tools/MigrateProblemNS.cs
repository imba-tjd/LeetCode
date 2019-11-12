// 作用看第20行
using System.IO;

namespace Tools
{
    class MigrateProblemNS
    {
        static void Main()
        {
            var files = Directory.GetFiles(".", "*.cs", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                string folderName = Path.GetFileName(Path.GetDirectoryName(f));
                int i = folderName.IndexOf('.');
                if (i == -1) // 比如templete文件夹下的.cs文件
                    continue;
                var sn = folderName.Substring(0, folderName.IndexOf('.')); // 001

                var content = File.ReadAllText(f);
                content = content.Replace("namespace Problems", "namespace Problems.Problem" + sn);
                File.WriteAllText(f, content);
            }
        }
    }
}
