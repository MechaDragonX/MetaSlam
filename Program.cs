using System;
using System.IO;
using System.Linq;

namespace MetaSlam
{
    class Program
    {
        private static readonly string[] commands = { /*"help"*/, "audio", /*"video",*/ "a", /*"v",*/ "exit" };

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- MetaSlam ---\n");
            Console.WriteLine("Please type \"help\" for help!\n");
            Console.ResetColor();
            string[] input;

            while(true)
            {
                input = CheckCommand();
                switch(input[0])
                {
                    //case "help":
                    //{
                    //    if(input.Length == 2)
                    //        HelpCommand(input[1]);
                    //    else
                    //        HelpCommand();
                    //    break;
                    //}
                    case "audio":
                    case "a":
                    {
                        if(input.Length == 2)
                            AudioCommand(input[1]);
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
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine().ToLower().Split(' ');
                if((input.Length > 0 && input.Length <= 2) && commands.Contains(input[0]))
                {
                    Console.ResetColor();
                    Console.WriteLine();
                    return input;
                }
                else if(input.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Please type a command!");
                }
                else if (input.Length > 2)
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
        private static string ValidatePath()
        {
            string path;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                path = Console.ReadLine();
                if(Directory.Exists(path))
                {
                    Console.ResetColor();
                    break;
                }
                else if(Directory.GetFiles(path).Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: That directory has no files!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: The directory doesn't exist! Did you misspell the path?");
                    Console.ResetColor();
                }
            }
            return Path.GetFullPath(path);
        }
        //private static void HelpCommand(string args = "")
        //{

        //}
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
