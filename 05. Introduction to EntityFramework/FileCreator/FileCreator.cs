namespace P02_DatabaseFirst
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public static class FileCreator
    {
        public static void CreateFile(string text)
        {
            string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\..\..\")) + @"\Output.txt";

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                byte[] data = Encoding.Default.GetBytes(text.Trim());
                fs.Write(data, 0, data.Length);
            }

            Process.Start("notepad.exe", path);
        }
    }
}
