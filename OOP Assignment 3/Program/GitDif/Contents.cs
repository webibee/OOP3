using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GitDif
{
    class Contents : TextFiles // Derived Class (Child)
    {
        private readonly List<string> FirstContentsList;
        private readonly List<string> SecondContentsList;
        public Contents(string[] UserFile) : base(UserFile) 
        {
            FirstContentsList = new List<string>(File.ReadAllLines(FirstFileName.ToString()));
            SecondContentsList = new List<string>(File.ReadAllLines(SecondFileName.ToString()));
        }
        public override List<string> FirstContents()
        {
            return FirstContentsList;
        }
        public override List<string> SecondContents()
        {
            return SecondContentsList;
        }

        public override Dictionary<int, string> FirstFileDictionary()
        {
            Dictionary<int, string> FirstFileDict = new Dictionary<int, string>();
            int DictionaryKeyValue = 1;
            foreach (string line in FirstContentsList)
            {
                FirstFileDict.Add(DictionaryKeyValue, line);
                DictionaryKeyValue++;
            }
            return FirstFileDict;
        }
        public override Dictionary<int, string> SecondFileDictionary()
        {
            Dictionary<int, string> SecondFileDict = new Dictionary<int, string>();
            int DictionaryKeyValue = 1;
            foreach (string line in SecondContentsList)
            {
                SecondFileDict.Add(DictionaryKeyValue, line);
                DictionaryKeyValue++;
            }
            return SecondFileDict;
        }
    }
}
