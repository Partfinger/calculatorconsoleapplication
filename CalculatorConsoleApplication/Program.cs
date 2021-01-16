using Calculator;
using CalculatorConsoleApplication.Output;
using System;
using System.IO;
using System.Text;

namespace CalculatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            if (args.Length > 0)
                WorkWithFile(args);
            else
                WorkWithConsole();
        }

        static void WorkWithConsole()
        {
            InputOutputHandler handler = new InputOutputHandler(new ConsoleAplication());
            Console.WriteLine(Properties.Localization.Welcome);
            Console.WriteLine(Properties.Localization.InputYourExpression);
            while (true)
            {
                string newLine = Console.ReadLine();
                if (newLine.Length != 0)
                    handler.HandleInput(newLine);
                else
                    break;
            }
        }

        static void WorkWithFile(string[] paths)
        {
            foreach(string path in paths)
            {
                if (File.Exists(path))
                {
                    FileManager manager = new FileManager(path);
                    InputOutputHandler handler = new InputOutputHandler(manager);
                    if (handler.HandleFile(path))
                    {
                        manager.WriteInFile();
                    }
                }
                else
                {
                    Console.WriteLine(
                        string.Format(Properties.Localization.FileNotExist, path));
                }
            }
        }
    }
}
