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
    public partial class StartPage : Form
    {
        public MainWindow mainWindow;
        public StartPage()
        {
            InitializeComponent();

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            BeautifyControls(this);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public void BeautifyControls(Control parent)
        {
            label1.BackColor = Color.FromArgb(70, 130, 180);
            label2.ForeColor = Color.FromArgb(0, 0, 0);
            label3.ForeColor = Color.FromArgb(0, 0, 0);
            label4.ForeColor = Color.FromArgb(0, 0, 0);
            label6.ForeColor = Color.FromArgb(0, 0, 0);
            button1.BackColor = Color.FromArgb(70, 130, 180);
            panel1.BackColor = Color.FromArgb(70, 130, 180);
        }
    }
}
