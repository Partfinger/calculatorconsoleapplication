using Calculator;
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
                WorkWithFile(args[0]);
            else
                WorkWithConsole();
        }

        static void WorkWithConsole()
        {
            InputOutputHandler handler = new InputOutputHandler(new ConsoleAplication());
            Console.WriteLine(Properties.Localization.Welcome);
            Console.WriteLine(Properties.Localization.InputYoutExpression);
            while (true)
            {
                handler.HandleInput(Console.ReadLine());
            }
        }

        static void WorkWithFile(string path)
        {
            if (File.Exists(path))
            {
                FileManager manager = new FileManager(path);
                InputOutputHandler handler = new InputOutputHandler(manager);
                string[] inputFile = manager.ReadFile();
                foreach (string expression in inputFile)
                {
                    handler.HandleInput(expression);
                }
                manager.WriteInFile();
            }
            else
            {
                Console.WriteLine(Properties.Localization.FileNotExist);
            }
        }
    }
}
