using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationDesignPlatform.Forms
{
	public partial class Form4 : Form
	{
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度
		private Form_Curve fs;

        public Form4()
		{
			InitializeComponent();
			GetDatabase();
			fs = new Form_Curve();
            #region  初始化控件缩放
            x = Width;
			y = Height;
			setTag(this);
			#endregion
		}

		private void GetDatabase()
		{
			// 设置属性
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.ReadOnly = true;

			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

			// 双缓冲
			Type dgvType = dataGridView1.GetType();
			PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			pi.SetValue(dataGridView1, true, null);

			// 绑定数据源
			dataGridView1.DataSource = Data.ResultDataTable;

			// 创建自定义复选框列表
			myCheckedListBox1.SetItems(dataGridView1.ColumnCount - 1);
			myCheckedListBox1.Indices = new int[] { 0, 9, 11, 13, 29, 35 };
			List<string> texts = new List<string>();
			foreach (DataGridViewTextBoxColumn column in dataGridView1.Columns)
			{
				texts.Add(column.HeaderText);
			}
			texts.RemoveAt(0);
			myCheckedListBox1.SetTexts(texts.ToArray());
			myCheckedListBox1.Enabled = false;

			// 设置时间阈值初值
			textBox1.Text = Data.start_time.ToString();
			textBox2.Text = Data.end_time.ToString();

			//初始选择状态恢复为false
            for (int i = 0; i < Data.data5_check.Length; i++)
            {
				Data.data5_check[i] = false;
            }
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

		// 修改逻辑，排除bug（20240408，由M修改）
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 获取选择的选项
			string selectedOption = comboBox1.SelectedItem.ToString();

			// 读取缓存复选信息
			bool[] checkState = null;
			switch (selectedOption)
			{
				case "电解电流":
					checkState = Data.data7_check;
					myCheckedListBox1.Enabled = true;
					break;
				case "温度":
					checkState = Data.data8_check;
					myCheckedListBox1.Enabled = true;
					break;
				case "氧中氢":
					checkState = Data.data9_check;
					myCheckedListBox1.Enabled = true;
					break;
				case "氢中氧":
					checkState = Data.data10_check;
					myCheckedListBox1.Enabled = true;
					break;
                case "阴极压力":
                    checkState = Data.data1_check;
                    myCheckedListBox1.Enabled = true;
                    break;
                case "阴极分离器氢气含量":
                    checkState = Data.data2_check;
                    myCheckedListBox1.Enabled = true;
                    break;
                case "阳极压力":
                    checkState = Data.data3_check;
                    myCheckedListBox1.Enabled = true;
                    break;
                case "阳极分离器氧气含量":
                    checkState = Data.data4_check;
                    myCheckedListBox1.Enabled = true;
                    break;
                default:
					foreach (CheckBox item in myCheckedListBox1.Items)
					{
						item.Checked = false;
					}
					myCheckedListBox1.Enabled = false;
					break;
			}
			if (checkState != null)
			{
				for (int i = 0; i < myCheckedListBox1.Items.Count; i++)
				{
					myCheckedListBox1.Items[i].Checked = checkState[i];
				}
			}
			else
			{
				foreach (CheckBox item in myCheckedListBox1.Items)
				{
					item.Checked = false;
				}
			}
		}

		private string Save()
		{
			double left, right;
			try
			{
				left = Math.Min(Data.end_time, Math.Max(Data.start_time, double.Parse(textBox1.Text)));
				right = Math.Min(Data.end_time, Math.Max(Data.start_time, double.Parse(textBox2.Text)));
				if (left > right)
				{
					if (left == Data.end_time)
					{
						left = Data.start_time;
					}
					else
					{
						right = Data.end_time;
					}
				}
			}
			catch (ArgumentNullException)
			{
				MessageBox.Show("请设置时间阈值！", "Warning",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return null;
			}
			catch (FormatException)
			{
				if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
				{
					MessageBox.Show("请设置时间阈值！", "Warning",
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					MessageBox.Show("无法将输入时间阈值转换为浮点数，请修改后重试！", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				return null;
			}
			catch (OverflowException)
			{
				MessageBox.Show("时间阈值溢出，请修改后重试！", "Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
			string selectedOption = comboBox1.SelectedItem?.ToString();
			if (selectedOption == null)
			{
				MessageBox.Show("请选择图表！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return string.Empty;
			}
			bool[] temp = new bool[0];
			ref bool[] checkState = ref temp;
			switch (selectedOption)
			{
				case "电解电流":
					checkState = ref Data.data7_check;
					Data.data7_left = left;
					Data.data7_right = right;
					break;
				case "温度":
					checkState = ref Data.data8_check;
					Data.data8_left = left;
					Data.data8_right = right;
					break;
				case "氧中氢":
					checkState = ref Data.data9_check;
					Data.data9_left = left;
					Data.data9_right = right;
					break;
				case "氢中氧":
					checkState = ref Data.data10_check;
					Data.data10_left = left;
					Data.data10_right = right;
					break;
                case "阴极压力":
                    checkState = ref Data.data1_check;
                    Data.data1_left = left;
                    Data.data1_right = right;
                    break;
                case "阴极分离器氢气含量":
                    checkState = ref Data.data2_check;
                    Data.data2_left = left;
                    Data.data2_right = right;
                    break;
                case "阳极压力":
                    checkState = ref Data.data3_check;
                    Data.data3_left = left;
                    Data.data3_right = right;
                    break;
                case "阳极分离器氧气含量":
                    checkState = ref Data.data10_check;
                    Data.data4_left = left;
                    Data.data4_right = right;
                    break;
            }
			if (checkState == null)
			{
				checkState = new bool[myCheckedListBox1.Items.Count];
			}
			for (int i = 0; i < checkState.Length; i++)
			{
				checkState[i] = myCheckedListBox1.Items[i].Checked;
			}
			return selectedOption;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string saveState = Save();
			if (!string.IsNullOrEmpty(saveState))
			{
				MessageBox.Show(string.Format("{0}图表保存成功！", saveState), "Info",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

            //switch (saveState)
            //{
            //    case "电解电流":
            //        Data.data5_check[0] = true;
            //        break;
            //    case "温度":
            //        Data.data5_check[1] = true;
            //        break;
            //    case "氧中氢":
            //        Data.data5_check[2] = true;
            //        break;
            //    case "氢中氧":
            //        Data.data5_check[3] = true;
            //        break;
            //    case "阴极压力":
            //        Data.data5_check[4] = true;
            //        break;
            //    case "阴极分离器氢气含量":
            //        Data.data5_check[5] = true;
            //        break;
            //    case "阳极压力":
            //        Data.data5_check[6] = true;
            //        break;
            //    case "阳极分离器氧气含量":
            //        Data.data5_check[7] = true;
            //        break;
            //}
        }

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			//Save();
			int num = 0;
			for (int i = 0;i< Data.data5_check.Length;i++)
			{
				if (Data.data5_check[i] == true)
				{  num++; }
			}
			if (num != 2 && num != 4)
				MessageBox.Show("请设置2条或者4条曲线","提示");
			else
				this.Close();
		}

		private void Form4_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			fs.ShowDialog();
		}
    }
}
