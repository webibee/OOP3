using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GitDif
{
    class Contents : TextFiles // Derived Class (Child)
    {
        private readonly List<string> FirstContentsList = new List<string>(File.ReadAllLines(FirstFileName.ToString()));
        private readonly List<string> SecondContentsList = new List<string>(File.ReadAllLines(SecondFileName.ToString()));
        public Contents(string[] UserFile) : base(UserFile) { }
        public override List<char> FirstIndividualCharacters() // Splitting first text file's contents (strings) into individual characters and storing in new list(char) 
        {
            List<char> Temp_List = new List<char>();
            List<string> Temp_First_Contents = FirstContentsList; //new List<string>(File.ReadAllLines(FirstFileName.ToString()));
            foreach (string str in Temp_First_Contents)
            {
                str.Split();
                foreach (char chars in str)
                {
                    Temp_List.Add(chars);
                }
            }
            return Temp_List;
        }
        public override List<char> SecondIndividualCharacters() // Splitting second text file's contents (strings) into individual characters and storing in new list(char) 
        {
            List<char> Temp_List = new List<char>();
            List<string> Temp_Second_Contents = SecondContentsList;
            foreach (string str in Temp_Second_Contents)
            {
                str.Split();
                foreach (char chars in str)
                {
                    Temp_List.Add(chars);
                }
            }
            return Temp_List;
        }
    }
}
