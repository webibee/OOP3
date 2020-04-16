using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace GitDif
{
    class Program
    {
        static void Main()
        {
            bool Leave_Loop = false;
            string[] User_Input = new string[0];
            List<char> First_Contents = new List<char>();
            List<char> Second_Contents = new List<char>();
            while (!Leave_Loop) //Error checking User's command and parameter inputs - only allowing 'dif' command and valid text file names beginning with "GitRepositories_" and being of type ".txt"
            {
                try
                {
                    Console.Write(">: [Input] ");
                    User_Input = Console.ReadLine().Split();
                    if ((InputCheck(User_Input[0], User_Input[1]) == true) & (InputCheck(User_Input[0], User_Input[2]) == true))
                    {
                        TextFiles User_Class_Instantiation = new Contents(User_Input);/////////////////////////     SCOTT
                        First_Contents = User_Class_Instantiation.FirstIndividualCharacters();
                        Second_Contents = User_Class_Instantiation.SecondIndividualCharacters();
                        Leave_Loop = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command or file(s) entered...");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
            bool Text_Files_Different = false;
            try
            {
                for (int Index = 0; Index < Second_Contents.Count(); Index++)// For loop cycling through each individual characters in both text files and comparing them
                {
                    if (First_Contents[Index] != Second_Contents[Index])
                    {
                        Console.Write($">: [Output] ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{User_Input[1]} and {User_Input[2]} are different");
                        Console.ForegroundColor = ConsoleColor.White;
                        Text_Files_Different = true;
                        break;
                    }
                }
                if (!Text_Files_Different)
                {
                    Console.Write($">: [Output] ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{User_Input[1]} and {User_Input[2]} are not different");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        public static bool InputCheck(string Command, string File)
        {
            if (Command.Contains("diff"))
            {
                if ((File.Contains("GitRepositories_")) & (File.Substring(File.Length - 4) == ".txt")) return true; // Only returns true if text files are in format "GitRepositories_*.txt"
                else return false;
            }
            else return false;
        }
    }
}
