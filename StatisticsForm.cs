﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_Sem3_Galin_Mihail
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm(IDictionary<string, int> stats)
        {
            InitializeComponent();
            foreach (KeyValuePair<string, int> p in stats)
            {
                dataGridView1.Rows.Add(new object[] { p.Key, p.Value });
            }
        }
    }
}
