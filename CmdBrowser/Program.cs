using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
                string argsString;

                if (userInput.Contains(" "))
                    argsString = userInput.Replace(command + " ", "");
                else
                    argsString = userInput.Replace(command, "");

                List<string> argsList = argsString.Split(' ').ToList();
                argsList.RemoveAll(x => x == "");

                switch (command)
                {
                    case "ls":
                        ConsoleCommands.ListDirectories(path);
                        break;

                    case "cd":
                        #region CD Logic
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

                        #endregion
                        ConsoleCommands.WriteHeader(path);
                        break;

                    case "open":
                        if (argsList.Count == 0)
                        {
                            Console.WriteLine("Opening directory...");
                            Process.Start("explorer.exe", path);
                        }
                        else
                        {
                            if (File.Exists(path + argsString))
                            {
                                Console.WriteLine("Opening file...");
                                Process process = new Process();
                                process.StartInfo.FileName = path + argsString;
                                process.StartInfo.RedirectStandardOutput = false;
                                process.Start();
                                Thread.Sleep(500);
                            }
                            else
                                Console.WriteLine("File doesn´t exist!");
                        }

                        ConsoleCommands.WriteHeader(path);
                        break;

                    case "clear":
                        ConsoleCommands.ClearConsole(path);
                        break;

                    case "stop":
                            return;

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
