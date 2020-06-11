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
        static string path = $"C:\\Users\\{Environment.UserName}\\";
        static void Main(string[] args)
        {
            Console.Title = "CmdBrowser";
            ConsoleCommands.ClearConsole(path);
            while (true)
            {
                string userInput = Console.ReadLine();
                string command = userInput.Split(' ')[0];
                string argsString = userInput.Replace(command + " ", "");
                List<string> argsList = argsString.Split(' ').ToList();

                argsList.RemoveAll(x => x == "");

                switch (command)
                {
                    case "ls":
                        ConsoleCommands.ListDirectories(path);
                        break;

                    case "cd":
                        if (argsList.Count == 0)
                        {
                            path = $"C:\\Users\\{Environment.UserName}\\";
                        }
                        else
                        {
                            if (argsList[0] == "..")
                            {
                                List<string> parsedPath = path.Split('\\').ToList();

                                parsedPath.RemoveAt(parsedPath.Count - 1);

                                if (parsedPath.Count == 1)
                                {
                                    ConsoleCommands.WriteHeader(path);
                                    break;
                                }
                                path = "";

                                for (int i = 0; i < parsedPath.Count - 1; i++)
                                {
                                    path += parsedPath[i] + "\\";
                                }
                            }
                            else
                            {
                                string tempPath = path + argsString + "\\";
                                if (Directory.Exists(tempPath))
                                    path = tempPath;
                                else
                                    Console.WriteLine("Path doesn´t exist: " + tempPath);
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
