using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ElectCell_HMI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (File.Exists(@".\ElectCell-HMI.exe") && File.Exists(@"..\..\..\ElectCell-HMI.exe"))
            {
                File.Copy(@".\ElectCell-HMI.exe", @"..\..\..\ElectCell-HMI.exe", true);
            }



        }
    }
}
