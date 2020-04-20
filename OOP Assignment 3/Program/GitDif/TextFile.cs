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
        private static readonly List<string> EmptyList = new List<string>() { "Empty" };
        public TextFiles(string[] User_String) //Class constructor - setting the file names to class-dependent properties/"names"
        {
            FirstFileName = User_String[1];
            SecondFileName = User_String[2];
        }
        public virtual List<string> FirstContents()
        {
            return EmptyList;
        }
        public virtual List<string> SecondContents()
        {
            return EmptyList;
        }
        public virtual Dictionary<int, string> FirstFileDictionary()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>() { { 1, "Empty" } };
            return dict; 
        }
        public virtual Dictionary<int, string> SecondFileDictionary()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>() { { 1, "Empty" } };
            return dict;
        }
    }
}
