using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectCell_HMI.Forms
{
    public partial class ExportedResults : Form
    {
        public ExportedResults()
        {
            InitializeComponent();
            button2.Click += (s, e) =>
            {
                bool allChecked = true;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (!checkedListBox1.GetItemChecked(i))
                    {
                        allChecked = false;
                        break;
                    }
                }
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, !allChecked);
                }
            };
        }
    }
}
