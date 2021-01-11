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

        public void WriteLine(string text)
        {
            output += $"{text}\n";
        }

        public void WriteInFile()
        {
            FileInfo fileInfo = new FileInfo(path);
            string newFileName = 
                $"{fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length)}_results{fileInfo.Extension}";
            string pathToNewFile = Path.Combine(fileInfo.DirectoryName, newFileName);
            using (FileStream stream = new FileStream(pathToNewFile, FileMode.Create))
            {
                stream.Write(Encoding.Default.GetBytes(output));
            }
        }

        public void Write(string text)
        {
            output += text;
        }
    }
}
