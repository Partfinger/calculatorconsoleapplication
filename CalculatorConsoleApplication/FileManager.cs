using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalculatorConsoleApplication
{
    public class FileManager : IOutput
    {
        string path;
        string output = "";

        public FileManager(string path)
        {
            this.path = path;
        }

        public string[] ReadFile()
        {
            return File.ReadAllLines(path);
        }

        public void WriteLine(string text)
        {
            output += $"{text}\n";
        }

        public void WriteInFile()
        {
            FileInfo fileInf = new FileInfo(path);
            string newFileName = fileInf.Name.Substring(0, fileInf.Name.Length - fileInf.Extension.Length);
            newFileName += "_result.txt";
            string pathToNewFile = Path.Combine(fileInf.DirectoryName, newFileName);
            FileStream stream = new FileStream(pathToNewFile, FileMode.Create);
            stream.Write(Encoding.Default.GetBytes(output));
            stream.Close();
        }

        public void Write(string text)
        {
            output += text;
        }
    }
}
