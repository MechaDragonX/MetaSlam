using System;
using System.IO;
using System.Linq;

namespace MetaSlam
{
    class Program
    {
        private static readonly string[] commands = { "help", "audio", "video", "exit" };
        private static readonly string[] shortCommands = { "h", "a", "v", "x" };
        private static readonly string[] help =
        {
            "Provides information on all the commands. You can use this command to just get information on a single command.\nSyntax: \"help <command>\" (\"command\" = Name of the Command)\nShortform: 'h'",
            "Name all audio files in the provided directory with the format, \"<number> - <title> - <artist if different>\", using the metadata.\nSyntax: \"audio <path>\" (\"path\" = Path to Directory)\nShortform: 'a'",
            "Exits the program.\nSyntax: \"exit\" (No arguments)\nShortform: 'x'"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("--- MetaSlam ---\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please type \"help\" for help!\n");
            Console.ResetColor();

            string[] input;
            while(true)
            {
                input = CheckCommand();
                switch(input[0])
                {
                    case "help":
                    {
                        if(input.Length == 2)
                            HelpCommand(input[1]);
                        else if(input.Length == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Too many arguments! Only a maximum of two is allowed!");
                            Console.ResetColor();
                        }
                        else
                            HelpCommand();
                        break;
                    }
                    case "audio":
                    {
                        if(input.Length == 2)
                        {
                            if(ValidateDirPath(input[1]))
                                AudioCommand(input[1]);
                        }
                        else if(input.Length == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Too many arguments! Only a maximum of two is allowed!");
                            Console.ResetColor();
                        }
                        else
                            AudioCommand();
                        break;
                    }
                    case "video":
                    {
                        if(input.Length == 2)
                        {
                            if(ValidateDirPath(input[1]))
                                VideoCommand(input[1]);
                        }
                        else if(input.Length == 3)
                        {
                            if(ValidateDirPath(input[1]) && ValidateFilePath(input[2]))
                                VideoCommand(input[1], input[2]);
                        }   
                        else
                            VideoCommand();
                        break;
                    }
                    case "exit":
                    {
                        Console.WriteLine("Thanks for using MetaSlam!\nPress any key to exit...");
                        Console.ReadKey();
                        return;
                    }
                }
            }
        }

        private static string[] CheckCommand()
        {
            string[] input;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine().ToLower().Split(' ');
                if((input.Length > 0 && input.Length <= 3) && (commands.Contains(input[0]) || shortCommands.Contains(input[0])))
                {
                    Console.ResetColor();
                    if(shortCommands.Contains(input[0]))
                        input[0] = commands[Array.IndexOf(shortCommands, input[0])];
                    return input;
                }
                else if(input.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Please type a command!");
                }
                else if(input.Length > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Too many arguments! Only a maximum of two is allowed!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Invalid Command! Type \"help\" for a list of commands!");
                }
            }
        }
        private static bool ValidateDirPath(string path)
        {
            if(Directory.GetFiles(path).Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: That directory has no files!");
                Console.ResetColor();
                return false;
            }
            else if(Directory.GetFiles(path).Length > 0)
            {
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: The directory doesn't exist! Did you misspell the path?");
                Console.ResetColor();
                return false;
            }
        }
        private static bool ValidateFilePath(string path)
        {
            if(!File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: The file doesn't exist! Did you misspell the path?");
                Console.ResetColor();
                return false;
            }
            return true;
        }
        private static void HelpCommand(string args = "")
        {
            if(args != "" && !(commands.Contains(args) || shortCommands.Contains(args)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: The provided argument is not a valid command!");
                Console.ResetColor();
                return;
            }
            if(args != "")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(commands[Array.IndexOf(commands, args)]);
                Console.ResetColor();
                Console.WriteLine($"{help[Array.IndexOf(commands, args)]}\n");
                return;
            }

            for(int i = 0; i < help.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(commands[i]);
                Console.ResetColor();
                Console.WriteLine($"{help[i]}");
            }
            Console.ResetColor();
            Console.WriteLine();
            return;
        }
        private static void AudioCommand(string args = "")
        {
            if(args == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: The audio command needs one argument. Please type \"help audio\" for more information.");
                Console.ResetColor();
                return;
            }

            Audio.NameDirectory(args);
            Console.WriteLine("All files written!\n");
        }
        private static void VideoCommand(string arg1 = "", string arg2 = "")
        {
            if((arg1 == "" && arg2 == "") || (arg1 == "" || arg2 == ""))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: The video command needs TWO arguments! Please type \"help video\" for more information.");
                Console.ResetColor();
                return;
            }

            if(Path.GetExtension(arg2) != ".txt")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: The metadata file needs to a plain text file! Please type \"help video\" for more information.");
                Console.ResetColor();
                return;
            }

            try
            {
                Video.NameDirectory(arg1, arg2);
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: {e.Message} Please type \"help video\" for more information.");
                Console.ResetColor();
                return;
            }
            finally
            {
                Console.WriteLine("All files written!\n");
            }
        }
    }
}
