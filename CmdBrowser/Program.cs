using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdBrowser
{
    class Program
    {
        static string path = $"C:\\Users\\{Environment.UserName}";
        static void Main(string[] args)
        {
            Console.Title = "CmdBrowser";
            ConsoleCommands.ClearConsole(path);
            while (true)
            {
                string[] command = Console.ReadLine().Split(' ');

                switch (command[0])
                {
                    case "ls":
                        ConsoleCommands.ListDirectories(path);
                        break;

                    case "cd":
                        if(command.Length == 1)
                        {
                            path = $"C:\\Users\\{Environment.UserName}";
                        }
                        else
                        {
                            if (command[1] == "..")
                            {
                                string[] parsedPath = path.Split('\\');
                                path = path.Replace("\\" + parsedPath[parsedPath.Length - 1], "");
                            }
                            else
                            {
                                if (Directory.Exists(path + "\\" + command[1]))
                                    path += $"\\{command[1]}";
                                else
                                    Console.WriteLine("Path doesn´t exist!");
                            }
                        }
                        

                        ConsoleCommands.WriteHeader(path);
                        break;

                    default:
                        Console.WriteLine("Command not found!");
                        ConsoleCommands.WriteHeader(path);
                        break;
                }
            }
        }

        public static string GetPath()
        {
            return path;
        }
    }
}
