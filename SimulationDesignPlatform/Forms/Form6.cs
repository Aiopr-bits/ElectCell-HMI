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
	public partial class Form6 : Form
	{
		private readonly float x;//定义当前窗体的宽度
		private readonly float y;//定义当前窗体的高度
		public Form6()
		{
			InitializeComponent();
			GetDatabase();

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

		private void Form6_Resize(object sender, EventArgs e)
		{
			//重置窗口布局
			ReWinformLayout();
		}

		//重写了该方法逻辑。20240401，由M修改
		private void GetDatabase()
		{
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
			// 读取缓存复选信息
			if (Data.data17_check != null)
			{
				for (int i = 0; i < myCheckedListBox1.Items.Count; i++)
				{
					myCheckedListBox1.Items[i].Checked = Data.data17_check[i];
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (Data.data17_check == null)
			{
				Data.data17_check = new bool[myCheckedListBox1.Items.Count];
			}
			for (int i = 0; i < Data.data17_check.Length; i++)
			{
				Data.data17_check[i] = myCheckedListBox1.Items[i].Checked;
			}
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}

	// 自定义的CheckListBox类，实现不同复选框勾选后其余复选框限制选择的功能
	public class MyCheckedListBox : Panel
	{
		public MyCheckedListBox() { }

		public List<CheckBox> Items { get; } = new List<CheckBox>();

		public int[] Indices { get; set; }

		public bool[] Checked
		{
			get
			{
				bool[] _checked = new bool[Items.Count];
				for (int i = 0; i < Items.Count; i++)
				{
					_checked[i] = Items[i].Checked;
				}
				return _checked;
			}
			set
			{
				for (int i = 0; i < Items.Count && i < value.Length; i++)
				{
					Items[i].Checked = value[i];
				}
			}
		}

		public void SetItems(int count)
		{
			for (int i = 0; i < count; i++)
			{
				Items.Add(new CheckBox());
				Items[i].CheckedChanged += MyCheckedListBox_CheckedChanged;
			}
			Controls.AddRange(Items.ToArray());
			Arrange();
		}

		public void SetTexts(string[] texts)
		{
			if (texts?.Length != 0)
			{
				for (int i = 0; i < Items.Count || i < texts.Length; i++)
				{
					Items[i].Text = texts[i];
				}
			}
		}

		private void MyCheckedListBox_CheckedChanged(object sender, EventArgs e)
		{
			int checkedIndex = -1;
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i].Checked)
				{
					checkedIndex = i;
					break;
				}
			}
			if (checkedIndex < 0 || Indices?.Length == 0)
			{
				foreach (CheckBox checkBox in Items)
				{
					checkBox.Enabled = true;
				}
			}
			else
			{
				for (int k = 1; k < Indices.Length; k++)
				{
					if (checkedIndex < Indices[k])
					{
						for (int j = 0; j < Items.Count; j++)
						{
							if (j < Indices[k - 1] || j >= Indices[k])
							{
								Items[j].Enabled = false;
							}
						}
						break;
					}
				}
			}
		}

		private void Arrange()
		{
			AutoScroll = true;
			const int height = 20;
			const int left = 4;
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].Height = height;
				Items[i].Location = new Point(left, height * i);
			}
		}
	}
}
