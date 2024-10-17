using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectCell_HMI
{
    public partial class ProcessDrawingPage : UserControl
    {
        private MainWindow mainWindow;

        public ProcessDrawingPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.mainWindow.TimerTicked += MainWindow_TimerTicked;
        }

        private void MainWindow_TimerTicked(object sender, EventArgs e)
        {
            if (Data.result.result == null || Data.result.result.Count == 0)
            {
                return;
            }
            this.label1.Text = ((double)Data.result.result[Data.result.result.Count - 1][0]).ToString();
            this.label2.Text = ((double)Data.result.result[Data.result.result.Count - 1][1]).ToString();
            this.label3.Text = ((double)Data.result.result[Data.result.result.Count - 1][2]).ToString();
            this.label4.Text = ((double)Data.result.result[Data.result.result.Count - 1][3]).ToString();
        }
    }
}
