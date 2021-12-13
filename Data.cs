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
    }
}