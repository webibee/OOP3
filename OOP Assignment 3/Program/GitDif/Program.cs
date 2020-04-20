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
            List<string> First_Contents = new List<string>();
            List<string> Second_Contents = new List<string>();
            Dictionary<int, string> First_Dictionary = new Dictionary<int, string>();
            Dictionary<int, string> Second_Dictionary = new Dictionary<int, string>();
            WriteToLog("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", false);
            DateTime LocalDate = DateTime.Now;
            WriteToLog($"Program Executed:: {LocalDate}", false);
            while (!Leave_Loop) //Error checking User's command and parameter inputs - only allowing 'dif' command and valid text file names beginning with "GitRepositories_" and being of type ".txt"
            {
                try
                {
                    Console.Write(">: [Input] ");
                    string User_Input_Temp = Console.ReadLine();
                    WriteToLog($"\n>: [Input] {User_Input_Temp}", false);
                    User_Input = User_Input_Temp.Split();
                    if ((InputCheck(User_Input[0], User_Input[1]) == true) & (InputCheck(User_Input[0], User_Input[2]) == true))
                    {
                        WriteToLog("Accepted User Input:: ", true);
                        foreach (string PartOfUserInput in User_Input)
                        {
                            WriteToLog(PartOfUserInput + " ", true);
                        }
                        WriteToLog("\n", true);
                        TextFiles User_Class_Instantiation = new Contents(User_Input);
                        First_Contents = User_Class_Instantiation.FirstContents();
                        First_Dictionary = User_Class_Instantiation.FirstFileDictionary();
                        Second_Contents = User_Class_Instantiation.SecondContents();
                        Second_Dictionary = User_Class_Instantiation.SecondFileDictionary();
                        Leave_Loop = true;
                    }
                    else
                    {
                        string temp = "Invalid command or file(s) entered...";
                        Console.WriteLine(temp);
                        WriteToLog(temp, false);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    WriteToLog($"Error: {e.Message}", false);
                }
            }
            try
            {
                if (First_Contents != Second_Contents)
                {
                    foreach (KeyValuePair<int, string> line in First_Dictionary)
                    {
                        if (!(Second_Dictionary[line.Key]).Contains(line.Value)) //If the Second files' line x is not the same as the first file's line x
                        {
                            string temp = $"\n>: [Output] Line: {line.Key}\n";
                            Console.Write(temp);
                            WriteToLog(temp, true);
                            OutputDifference(First_Dictionary[line.Key].Split(), Second_Dictionary[line.Key].Split());            
                        }
                    }
                }
                else
                {
                    string temp = ">: [Output] ";
                    Console.Write(temp);
                    WriteToLog(temp, true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    temp = $"{User_Input[1]} and {User_Input[2]} are not different";
                    Console.Write(temp);
                    WriteToLog(temp, true);
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                string temp = $"Error: {e.Message}";
                Console.WriteLine(temp);
                WriteToLog(temp, false);
            }
        }
        private static bool InputCheck(string Command, string File)
        {
            if (Command.Contains("diff"))
            {
                if ((File.Contains("GitRepositories_")) & (File.Substring(File.Length - 4) == ".txt")) return true; // Only returns true if text files are in format "GitRepositories_*.txt"
                else return false;
            }
            else return false;
        }
        private static void OutputDifference(string[] FirstString, string[] SecondString)
        {
            if (FirstString == SecondString)
            {
                foreach (string word in FirstString)
                {
                    string temp = word + " ";
                    Console.Write(temp);
                    WriteToLog(temp, true);
                }
            }
            else if ((FirstString.Length == 0) & (SecondString.Length != 0))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (string word in SecondString)
                {
                    string temp = word + " ";
                    Console.Write(temp);
                    WriteToLog(temp + "(GREEN)", true);
                }
                Console.ResetColor();
            }
            else if ((FirstString.Length != 0) & (SecondString.Length == 0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (string word in FirstString)
                {
                    string temp = word + " ";
                    Console.Write(temp);
                    WriteToLog(temp + "(RED)", true);
                }
                Console.ResetColor();
            }
            else
            {
                string FirstWordFirstString = FirstString[0];
                //Console.WriteLine($"WORD1: {FirstWordFirstString}");
                string[] FirstRemaining = new ArraySegment<string>(FirstString, 1, FirstString.Length - 1).ToArray();
                string FirstWordSecondString = SecondString[0];
                //Console.WriteLine($"WORD2: {FirstWordSecondString}");
                string[] SecondRemaining = new ArraySegment<string>(SecondString, 1, SecondString.Length - 1).ToArray();
                bool FirstChecker = PresenceChecker(FirstRemaining, SecondString);
                //Console.WriteLine($"FirstChecker: {FirstChecker}");
                bool SecondChecker = PresenceChecker(SecondRemaining, FirstString);
                //Console.WriteLine($"SecondChecker: {SecondChecker}");
                if (FirstWordFirstString == FirstWordSecondString)
                {
                    string temp = FirstWordFirstString + " ";
                    Console.Write(temp);
                    WriteToLog(temp, true);
                    OutputDifference(FirstRemaining, SecondRemaining);
                }
                else if ((FirstWordFirstString != FirstWordSecondString) & (!(SecondRemaining.Contains(FirstWordFirstString))))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    string temp = FirstWordFirstString + " ";
                    Console.Write(temp);
                    WriteToLog(temp + "(RED)", true);
                    Console.ResetColor();
                    OutputDifference(FirstRemaining, SecondString);
                }
                else if ((FirstWordFirstString != FirstWordSecondString) & (!(FirstRemaining.Contains(FirstWordSecondString))))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string temp = FirstWordSecondString + " ";
                    Console.Write(temp);
                    WriteToLog(temp + "(GREEN)", true);
                    Console.ResetColor();
                    OutputDifference(FirstString, SecondRemaining);
                }
                else if ((FirstWordFirstString != FirstWordSecondString) & (FirstChecker == true))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string temp = FirstWordSecondString + " ";
                    Console.Write(temp);
                    WriteToLog(temp + "(GREEN)", true); // true = write; false = writeLINE
                    Console.ResetColor();
                    OutputDifference(FirstString, SecondRemaining);
                }
                else if ((FirstWordFirstString != FirstWordSecondString) & (SecondChecker == true))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string temp = FirstWordSecondString + " ";
                    Console.Write(temp);
                    WriteToLog(temp + "(GREEN)", true); // true = write; false = writeLINE
                    Console.ResetColor();
                    OutputDifference(FirstString, SecondRemaining);
                }
                //else if (FirstWordFirstString != FirstWordSecondString) //DON'T FORGET TO ADD ADDING-TO-LOG LINES
                //{
                //    if((Match70 > 0)&(Match70 <= 2))
                //    {
                //        if(FirstWordFirstString == SecondRemaining[0])
                //        {
                //            Console.ForegroundColor = ConsoleColor.Green;
                //            Console.Write(FirstWordSecondString + " ");
                //            Console.ResetColor();
                //            OutputDifference(FirstString, SecondRemaining);
                //        }
                //        else if (FirstRemaining[0] == FirstWordSecondString)
                //        {
                //            Console.ForegroundColor = ConsoleColor.Red;
                //            Console.Write(FirstWordFirstString + " ");
                //            Console.ResetColor();
                //            OutputDifference(FirstRemaining, SecondString);//////////////////
                //        }
                //    }
                //    else if (Match70 > 2)
                //    {
                //        Console.ForegroundColor = ConsoleColor.Green;
                //        Console.Write(FirstWordSecondString + " ");
                //        Console.ResetColor();
                //        OutputDifference(FirstString, SecondRemaining);
                //    }
                //    else
                //    {
                //        Console.ForegroundColor = ConsoleColor.Red;
                //        Console.Write(FirstWordFirstString + " ");
                //        Console.ResetColor();
                //        OutputDifference(FirstRemaining, SecondString);
                //    }
                //}
            }
        }
        private static bool PresenceChecker(string[] Array1, string[] Array2)
        {
            foreach(string element in Array1)
            {
                if (!(Array2.Contains(element)))
                {
                    return false;
                }
            }
            return true;
        }
        private static void WriteToLog(string LogMessage, bool WriteOrWriteLine) // true = write; false = writeLINE
        {
            using StreamWriter Log = File.AppendText(@"Log.txt");
            {
                if (WriteOrWriteLine == true) Log.Write(LogMessage);
                else Log.WriteLine(LogMessage);
            }
            Log.Close();
        }
    }
}
