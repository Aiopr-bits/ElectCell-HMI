using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationDesignPlatform
{
    public partial class Form_Curve : Form
    {
        public Form_Curve()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Data.data5_check[0] = this.checkBox1.Checked;
            Data.data5_check[1] = this.checkBox2.Checked;
            Data.data5_check[2] = this.checkBox3.Checked;
            Data.data5_check[3] = this.checkBox4.Checked;
            Data.data5_check[4] = this.checkBox5.Checked;
            Data.data5_check[5] = this.checkBox6.Checked;
            Data.data5_check[6] = this.checkBox7.Checked;
            Data.data5_check[7] = this.checkBox8.Checked;
            this.Hide();
        }
    }
}
