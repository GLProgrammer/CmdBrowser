using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdBrowser
{
    class ConsoleCommands
    {
        public static void ClearConsole(string path)
        {
            string username = Environment.UserName;
            Console.Clear();
            Console.WriteLine($"Welcome {username}! ");
            WriteHeaderWithoutLine(path);
        }

        static void WriteHeaderWithoutLine(string path)
        {
            string username = Environment.UserName;
            string computerName = Environment.UserDomainName;
            if (path == $"C:\\Users\\{Environment.UserName}\\")
                Console.Write($"{username}@{computerName}-> ~$ ");
            else
                Console.Write($"{username}@{computerName}-> {path}$ ");
        }

        public static void WriteHeader(string path)
        {
            Console.WriteLine();
            string username = Environment.UserName;
            string computerName = Environment.UserDomainName;
            if (path == $"C:\\Users\\{Environment.UserName}\\")
                Console.Write($"{username}@{computerName}-> ~$ ");
            else
                Console.Write($"{username}@{computerName}-> {path}$ ");
        }

        public static void ListDirectories(string path)
        {
            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            foreach (string directoryName in directories)
            {
                string directoryNameToWrite = directoryName.Replace(path, "");
                if (directoryNameToWrite.StartsWith("."))
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                else
                    Console.ForegroundColor = ConsoleColor.Blue;

                Console.Write($" {directoryNameToWrite}\\");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (string fileName in files)
            {
                string fileNameToWrite = fileName.Replace(path, "");
                Console.Write($" {fileNameToWrite}");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            WriteHeader(path);
        }
    }
}
