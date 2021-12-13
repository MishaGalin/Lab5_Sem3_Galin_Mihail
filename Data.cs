using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab5_Sem3_Galin_Mihail
{
    internal class Data
    {
        public string Text { get; private set; }
        public string FileName { get; private set; }
        public Match Match { get; private set; }

        internal void ReadFromFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                Text = sr.ReadToEnd().Replace("\r", "");  //стандартный символ конца строки
            }
        }

        public void Find(string RegEx)
        {
            if (Text == null)
            {
                Console.WriteLine("Попытка поиска без открытия файла");
                return;
            }
            Match = Regex.Match(Text, RegEx);
        }

        internal void Next()
        {
            Match = Match?.NextMatch();
        }
    }
}