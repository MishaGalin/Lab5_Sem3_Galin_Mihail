using Lab5_Sem3_Galin_Mihail.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab5_Sem3_Galin_Mihail
{
    public partial class MainForm : Form
    {
        private readonly Data data = new Data();

        public MainForm()
        {
            InitializeComponent();
            lstbRegex.Items.Add(@"\b(([A-Z])[a-z]+)\?\s");
            lstbRegex.Items.Add(@"\b\d{4}\b");
            lstbRegex.Items.Add(@"\b(о)([а-я]){5,}");
            lstbRegex.Items.Add(@"\b(о)(\S)+");
            lstbRegex.Items.Add(@"([А-Я]).*([.!?])+");
            lstbRegex.Items.Add(@"\s+\w+\s+");
            lstbRegex.Items.Add(@"\b(of|or)\b");
            lstbRegex.Click += (s, e) =>
              {
                  txtbFindString.Text = lstbRegex.Text;
                  Find(s, e);
              };
        }

        private void DownAnyKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Find(sender, e);
                e.SuppressKeyPress = true; // дальше событие нажатие кнопки игнорируется
            }
        }

        private void Find(object sender, EventArgs e)
        {
            data.Find((string)txtbFindString.Text);
            ShowMatch();
        }

        private void LoadSettings(object sender, EventArgs e)
        {
            data.ReadFromFile(Settings.Default.FileName);
            Console.WriteLine($"File - {data.FileName} - is open");
            rtbFileText.Text = data.Text;
        }

        private void NextMatch(object sender, EventArgs e)
        {
            data.Next();
            ShowMatch();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*", // расширения
                FilterIndex = 1
            };

            DialogResult res = dlg.ShowDialog(); // показываем диалог и ждём ok или отмены
            if (res == DialogResult.OK) // если не нажали отмену
            {
                data.ReadFromFile(dlg.FileName);
                Console.WriteLine($"File - {dlg.FileName} - is open");
                rtbFileText.Text = data.Text;
            }
        }

        private void SaveSettings(object sender, FormClosingEventArgs e)
        {
            Settings.Default.FileName = data.FileName;
            Settings.Default.Save();
        }

        private void ShowMatch()
        {
            Match m = data.Match; // получить m из data, добавить using
            if (m != null && m.Success)
            {
                rtbFileText.SelectionBackColor = Color.White; // сброс подсветки
                rtbFileText.SelectionStart = m.Index; // начало - место, на котором в строке найдено регулярное выражение
                rtbFileText.SelectionLength = m.Value.Length; // длина найденного фрагмента
                rtbFileText.ScrollToCaret(); // прокрутка на выделенное место
                rtbFileText.SelectionBackColor = Color.Yellow; // подсветка

                rtbSearchResult.Text = $"Найдено[{m.Index}]: {m.Value}\n";
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    rtbSearchResult.Text += String.Format("Groups[{0}]={1}\n", i, m.Groups[i]);
                }
            }
        }

        private void OfOrClick(object sender, EventArgs e)
        {
            data.GetOfOrStatistics(out int ofc, out int orc);
            rtbSearchResult.Text = $" of: {ofc}, or: {orc}";
        }

        private void FindFirstWord(object sender, EventArgs e)
        {
            ISet<string> words = data.FindSentencesFirstWords();
            rtbSearchResult.Text = String.Join(", ", words);
        }

        private void FindFirstLetter(object sender, EventArgs e)
        {
            new StatisticsForm(data.FirstLetterCounts()).Show();
        }

        private void SearchFrenchWords(object sender, EventArgs e)
        {
            new StatisticsForm(data.SearchFrenchWords()).Show();
        }
    }
}