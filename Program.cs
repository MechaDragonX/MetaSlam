using System;
using System.IO;
using System.Linq;

namespace MetaSlam
{
    class Program
    {
        private static readonly string[] commands = { "help", "audio", /*"video",*/ "exit" };
        private static readonly string[] shortCommands = { "h", "a", /*"v"*/ "x" };
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
                        if (input.Length == 2)
                            HelpCommand(input[1]);
                        else
                            HelpCommand();
                        break;
                    }
                    case "audio":
                    {
                        if(input.Length == 2)
                        {
                            if(ValidatePath(input[1]))
                                AudioCommand(input[1]);
                        }
                        else
                            AudioCommand();
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
                if((input.Length > 0 && input.Length <= 2) && (commands.Contains(input[0]) || shortCommands.Contains(input[0])))
                {
                    Console.ResetColor();
                    Console.WriteLine();
                    if(shortCommands.Contains(input[0]))
                    {
                        if(input.Length == 1)
                            return new string[] { commands[Array.IndexOf(shortCommands, input[0])] };
                        else
                            return new string[] { commands[Array.IndexOf(shortCommands, input[0])], input[1] };
                    }
                }
                else if(input.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Please type a command!");
                }
                else if(input.Length > 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Too many arguments! Only one is allowed!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Invalid Command! Type \"help\" for a list of commands!");
                }
            }
        }
        private static bool ValidatePath(string path)
        {
            if(Directory.GetFiles(path).Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: That directory has no files!");
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
                Console.WriteLine($"ERROR: The directory doesn't exist! Did you misspell the path?");
                Console.ResetColor();
                return false;
            }
        }
        private static void HelpCommand(string args = "")
        {
            if(args != "" && !(commands.Contains(args) || shortCommands.Contains(args)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: The provided argument is not a valid command!");
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
                Console.WriteLine($"ERROR: The audio command needs one argument. Please type \"help audio\" for more information.");
                Console.ResetColor();
                return;
            }

            Audio.NameDirectory(args);
            Console.WriteLine("All files written!\n");
        }
    }
}
