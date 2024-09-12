using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationDesignPlatform.Forms;

namespace SimulationDesignPlatform.Forms
{
    public partial class Form1 : Form
    {
        private readonly float x;//定义当前窗体的宽度
        private readonly float y;//定义当前窗体的高度
        public Form1()
        {
            InitializeComponent();
            if (Data.user_name != null && Data.user_password != null)
            {
                txtName.Text = Data.user_name;
                txtPassword.Text = Data.user_password;
            }
            #region  初始化控件缩放
            x = Width;
            y = Height;
            setTag(this);
            #endregion
        }

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0) setTag(con);
            }
        }

        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                if (con.Tag != null)
                {
                    var mytag = con.Tag.ToString().Split(';');
                    con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * newx);
                    con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * newy);
                    con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * newx);
                    con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * newy);
                    var currentSize = Convert.ToSingle(mytag[4]) * newy;

                    if (currentSize > 0)
                    {
                        FontFamily fontFamily = new FontFamily(con.Font.Name);
                        con.Font = new Font(fontFamily, currentSize, con.Font.Style, con.Font.Unit);
                    }
                    con.Focus();
                    if (con.Controls.Count > 0) setControls(newx, newy, con);
                }
            }
        }

        private void ReWinformLayout()
        {
            var newx = Width / x;
            var newy = Height / y;
            setControls(newx, newy, this);
        }

        Form2 form2 = new Form2();

        private void button1_Click(object sender, EventArgs e)
        {
            //string username = txtName.Text;
            //String password = txtPassword.Text;
            string username = "admin";
            String password = "123456";
            string path = System.Windows.Forms.Application.StartupPath;
            //Data.exePath = Directory.GetParent(path).FullName;
            Data.exePath = path;
            Data.casePath = Path.Combine(Data.exePath, "case_default");

            if (cb.Checked)
            {
                Data.user_name = username;
                Data.user_password = password;
            }
            if (username == "admin" && password == "123456")
            {
                form2 = new Form2();
                form2.Show();
                this.Hide();
                //this.DialogResult = DialogResult.OK;
                //this.Dispose();
                //this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码不正确");
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //重置窗口布局
            ReWinformLayout();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
