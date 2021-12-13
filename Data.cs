using System;
using System.Collections.Generic;
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

        public ISet<string> FindSentencesFirstWords()
        {
            ISet<string> words = new HashSet<string>();
            foreach (Match m in Regex.Matches(Text, @"(([A-Z]|[А-Я])([a-z]|[а-я])+)\b+.*[?]"))
            {
                words.Add(m.Groups[1].Value);
            }
            return words;
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

        internal void GetOfOrStatistics(out int ofc, out int orc)
        {
            ofc = 0; orc = 0;
            foreach (Match m in Regex.Matches(Text, @"\b(of|or)\b"))
            {
                if (m.Value == "of") ofc++;
                else orc++;

            }
        }
    }
}