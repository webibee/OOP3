using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GitDif
{
    class TextFiles // Base Class (Parent)
    {
        public static string FirstFileName;
        public static string SecondFileName;
        private static readonly List<char> EmptyList = new List<char>("Empty");
        public TextFiles(string[] User_String) //Class constructor - setting the file names to class-dependent properties/"names"
        {
            FirstFileName = User_String[1];
            SecondFileName = User_String[2];
        }
        public TextFiles()
        {
            Console.WriteLine("Contents needed to compare");
        }
        public virtual List<char> FirstIndividualCharacters()
        {
            return EmptyList;
        }
        public virtual List<char> SecondIndividualCharacters()
        {
            return EmptyList;
        }
    }
}
