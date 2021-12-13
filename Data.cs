using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab5_Sem3_Galin_Mihail
{
    internal class Data
    {
        public string FileName { get; private set; }
        public Match Match { get; private set; }
        public string Text { get; private set; }

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

        internal void ReadFromFile(string fileName)
        {
            if (fileName == "")
            {
                Console.WriteLine("Попытка открытия файла без задания имени");
                return;
            }
            using (StreamReader sr = new StreamReader(fileName))
            {
                Text = sr.ReadToEnd().Replace("\r", "");  //стандартный символ конца строки
                FileName = fileName;
            }
        }
    }
}