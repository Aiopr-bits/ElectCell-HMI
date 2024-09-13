using SimulationDesignPlatform.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationDesignPlatform.Forms
{
	public partial class Form2 : Form
	{
		private UserControl1 ucd;
		private UserControl2 ucd2;
		private UserControl4 ucd4;
		private UserControl5 ucd5;
		private UserControl6 ucd6;
		private UserControl7 ucd7;
		private UserControl8 ucd8;
		private UserControl9 ucd9;
		private UserControl10 ucd10;
		private UserControl11 ucd11;
		private UserControl12 ucd12;
		private UserControl13 ucd13;
		private UserControl14 ucd14;
		private UserControl15 ucd15;
		private UserControl16 ucd16;
		private UserControl17 ucd17;
		private UserControl18 ucd18;
		private UserControl19 ucd19;
		private UserControl20 ucd20;
		private UserControl21 ucd21;

		public Form2()
		{
			InitializeComponent();
			showShouye();
			Rectangle ScreenArea = Screen.GetWorkingArea(this);
			if (ScreenArea.Height < this.Height)
			{
				this.Height = ScreenArea.Height;
			}
		}

		private void showShouye()
		{
			splitContainer4.Panel2.Controls.Clear();
			ucd = new UserControl1();
			ucd.Dock = DockStyle.Fill;
			ucd.Parent = this.splitContainer4.Panel2;
			splitContainer4.Panel2.Controls.Add(ucd);
			comboBox1.Visible = false;
			button2.Visible = false;
			button1.Visible = false;
			button3.Visible = false;
			button4.Visible = false;
			this.comboBox1.Visible = false;
			//label2.Text = "主页面图表数据加载完毕";
            label2.Text = "主页面初始化完毕";
            label3.Text = "主页面";

			// 树默认展开。20240321，由M添加
			Expand(treeView1.Nodes);
			void Expand(TreeNodeCollection nodes)
			{
				foreach (TreeNode node in nodes)
				{
					node.Expand();
					if (node.Nodes != null || node.Nodes.Count > 0)
					{
						Expand(node.Nodes);
					}
				}
			}
		}

		private void HighLightNode()
		{
			// 设置当前选中节点高亮。20240319，由M添加
			DefaultNode(treeView1.Nodes);
			treeView1.SelectedNode.BackColor = Color.SeaGreen;
			treeView1.SelectedNode.ForeColor = Color.White;
			void DefaultNode(TreeNodeCollection nodes)
			{
				foreach (TreeNode node in nodes)
				{
					if (node.Nodes is not null)
						DefaultNode(node.Nodes);
					node.BackColor = Color.White;
					node.ForeColor = Color.Black;
				}
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			HighLightNode();
			switch (treeView1.SelectedNode.Text)
			{
				case "主页面":
					splitContainer4.Panel2.Controls.Clear();
					ucd = new UserControl1();
					ucd.Dock = DockStyle.Fill;
					ucd.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "主页面图表数据加载完毕";
					label3.Text = "主页面";
					break;
				case "仿真模型参数汇总":
					splitContainer4.Panel2.Controls.Clear();
					ucd2 = new UserControl2();
					ucd2.Dock = DockStyle.Fill;
					ucd2.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd2);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "仿真模型参数汇总数据加载完毕";
					label3.Text = "仿真模型参数汇总";
					break;
				case "控制参数配置":
                    splitContainer4.Panel2.Controls.Clear();
                    ucd4 = new UserControl4();
					ucd4.Dock = DockStyle.Fill;
					ucd4.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd4);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "控制参数配置数据加载完毕";
					label3.Text = "控制参数配置";
					break;
				case "几何参数配置":
					splitContainer4.Panel2.Controls.Clear();
					ucd5 = new UserControl5();
					ucd5.Dock = DockStyle.Fill;
					ucd5.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd5);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "几何参数配置数据加载完毕";
					label3.Text = "几何参数配置";
					break;
				case "flow参数配置":
					splitContainer4.Panel2.Controls.Clear();
					ucd6 = new UserControl6();
					ucd6.Dock = DockStyle.Fill;
					ucd6.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd6);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "flow参数配置数据加载完毕";
					label3.Text = "flow参数配置";
					break;
				case "ps参数配置":
					splitContainer4.Panel2.Controls.Clear();
					ucd7 = new UserControl7();
					ucd7.Dock = DockStyle.Fill;
					ucd7.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd7);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "ps参数配置数据加载完毕";
					label3.Text = "ps参数配置";
					break;
				case "工艺参数配置":
					splitContainer4.Panel2.Controls.Clear();
					ucd8 = new UserControl8();
					ucd8.Dock = DockStyle.Fill;
					ucd8.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd8);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "工艺参数配置数据加载完毕";
					label3.Text = "工艺参数配置";
					break;
				case "部件参数配置":
					splitContainer4.Panel2.Controls.Clear();
					ucd9 = new UserControl9();
					ucd9.Dock = DockStyle.Fill;
					ucd9.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd9);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "部件参数配置数据加载完毕";
					label3.Text = "部件参数配置";
					break;
				case "仿真结果":
					splitContainer4.Panel2.Controls.Clear();
					ucd10 = new UserControl10();
					ucd10.Dock = DockStyle.Fill;
					ucd10.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd10);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = true;
					button4.Visible = false;
                    label2.Text = "仿真结果数据加载完毕";
					label3.Text = "仿真结果";
					break;
				case "信号路由":
					splitContainer4.Panel2.Controls.Clear();
					ucd11 = new UserControl11();
					ucd11.Dock = DockStyle.Fill;
					ucd11.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd11);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "信号路由数据加载完毕";
					label3.Text = "信号路由";
					break;
				case "变量清单":
					splitContainer4.Panel2.Controls.Clear();
					ucd12 = new UserControl12();
					ucd12.Dock = DockStyle.Fill;
					ucd12.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd12);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "变量清单数据加载完毕";
					label3.Text = "变量清单";
					break;
				case "故障注入":
					splitContainer4.Panel2.Controls.Clear();
					ucd13 = new UserControl13();
					ucd13.Dock = DockStyle.Fill;
					ucd13.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd13);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "故障注入数据加载完毕";
					label3.Text = "故障注入";
					break;
				case "自动测试":
					splitContainer4.Panel2.Controls.Clear();
					ucd14 = new UserControl14();
					ucd14.Dock = DockStyle.Fill;
					ucd14.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd14);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "自动测试数据加载完毕";
					label3.Text = "自动测试";
					break;
				case "工艺流程":
					splitContainer4.Panel2.Controls.Clear();
					ucd15 = new UserControl15();
					ucd15.Dock = DockStyle.Fill;
					ucd15.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd15);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = true;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "工艺流程图表数据加载完毕";
					label3.Text = "工艺流程";
					break;
				case "趋势监视":
					splitContainer4.Panel2.Controls.Clear();
					//ucd16 = new UserControl16();
					//ucd16.Dock = DockStyle.Fill;
					//ucd16.Parent = this.splitContainer4.Panel2;
					//splitContainer4.Panel2.Controls.Add(ucd16);
					comboBox1.Visible = false;
					button2.Visible = true;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    Data.data5_check = new bool[8];
                    label2.Text = "趋势监视图表数据加载完毕";
					label3.Text = "趋势监视";
                    splitContainer4.Panel2.Controls.Clear();
                    //button2.Enabled = false;
                    //comboBox1.Enabled = false;
                    break;
				case "数据列表":
					splitContainer4.Panel2.Controls.Clear();
					ucd20 = new UserControl20();
					ucd20.Dock = DockStyle.Fill;
					ucd20.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd20);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = false;
                    label2.Text = "数据列表数据加载完毕";
					label3.Text = "数据列表";
					break;
				case "数据回放":
					splitContainer4.Panel2.Controls.Clear();
					ucd21 = new UserControl21();
					ucd21.Dock = DockStyle.Fill;
					ucd21.Parent = this.splitContainer4.Panel2;
					splitContainer4.Panel2.Controls.Add(ucd21);
					comboBox1.Visible = false;
					button2.Visible = false;
					button1.Visible = false;
					button3.Visible = false;
					button4.Visible = true;
                    label2.Text = "数据回放图表数据加载完毕";
					label3.Text = "数据回放";
					break;
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

			if (Data.caseUsePath == "" || Data.caseUsePath == null)
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}

			ucd16 = new UserControl16();
			//ucd17 = new UserControl17();
			ucd18 = new UserControl18();
			//ucd19 = new UserControl19();

			// 获取选择的选项
			string selectedOption = comboBox1.SelectedItem.ToString();

			// 更新标签文本
			comboBox1.Text = selectedOption;

			// 执行其他操作
			if (selectedOption == "2-图表")
			{

				splitContainer4.Panel2.Controls.Clear();
				ucd16 = new UserControl16();
				ucd16.Dock = DockStyle.Fill;
				ucd16.Parent = this.splitContainer4.Panel2;
				splitContainer4.Panel2.Controls.Add(ucd16);
				ucd16.Visible = true;
				//ucd17.Visible = false;
				ucd18.Visible = false;
				//ucd19.Visible = false;
			}
			//else if (selectedOption == "3-图表")
			//{
			//	splitContainer4.Panel2.Controls.Clear();
			//	//ucd17 = new UserControl17();
			//	ucd17.Dock = DockStyle.Fill;
			//	ucd17.Parent = this.splitContainer4.Panel2;
			//	splitContainer4.Panel2.Controls.Add(ucd17);
			//	ucd16.Visible = false;
			//	ucd17.Visible = false;
			//	ucd18.Visible = true;
			//	ucd19.Visible = false;

			//}
			else if (selectedOption == "4-图表")
			{
				splitContainer4.Panel2.Controls.Clear();
				//ucd18 = new UserControl18();
				ucd18.Dock = DockStyle.Fill;
				ucd18.Parent = this.splitContainer4.Panel2;
				splitContainer4.Panel2.Controls.Add(ucd18);
			}
			//else if (selectedOption == "6-图表")
			//{
			//	splitContainer4.Panel2.Controls.Clear();
			//	//ucd19 = new UserControl19();
			//	ucd19.Dock = DockStyle.Fill;
			//	ucd19.Parent = this.splitContainer4.Panel2;
			//	splitContainer4.Panel2.Controls.Add(ucd19);
			//	ucd16.Visible = false;
			//	ucd17.Visible = false;
			//	ucd18.Visible = false;
			//	ucd19.Visible = true;
			//}
		}

		private void button2_Click(object sender, EventArgs e)
		{
            using (Form4 fs = new Form4())
            {
                fs.ShowDialog();
                this.refresh16();
            }
            int num = 0;
            for (int i = 0; i < Data.data5_check.Length; i++)
            {
                if (Data.data5_check[i] == true)
                { num++; }
            }
            if (num == 2 )
                this.comboBox1.SelectedIndex = 1;
            if (num == 4)
                this.comboBox1.SelectedIndex = 1;
        }

		public void refresh16()
		{
			//splitContainer4.Panel2.Controls.Clear();
			//ucd16 = new UserControl16();
			//ucd16.Dock = DockStyle.Fill;
			//ucd16.Parent = this.splitContainer4.Panel2;
			//splitContainer4.Panel2.Controls.Add(ucd16);
			comboBox1.Visible = true;
			button2.Visible = true;
			button1.Visible = false;
			button3.Visible = false;
			button4.Visible = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (Form4 fs = new Form4())
			{
				fs.ShowDialog();
				this.refresh15();
			}
		}

		public void refresh15()
		{
			splitContainer4.Panel2.Controls.Clear();
			ucd15 = new UserControl15();
			ucd15.Dock = DockStyle.Fill;
			ucd15.Parent = this.splitContainer4.Panel2;
			splitContainer4.Panel2.Controls.Add(ucd15);
			comboBox1.Visible = false;
			button2.Visible = false;
			button1.Visible = true;
			button3.Visible = false;
			button4.Visible = false;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			using (Form5 fs = new Form5())
			{
				fs.ShowDialog();
				this.refresh21();
			}
		}

		public void refresh21()
		{
			splitContainer4.Panel2.Controls.Clear();
			ucd21 = new UserControl21();
			ucd21.Dock = DockStyle.Fill;
			ucd21.Parent = this.splitContainer4.Panel2;
			splitContainer4.Panel2.Controls.Add(ucd21);
			comboBox1.Visible = false;
			button2.Visible = false;
			button1.Visible = false;
			button3.Visible = false;
			button4.Visible = true;
		}

		private void button5_Click(object sender, EventArgs e)
		{
			if (Data.caseUsePath == "" || Data.caseUsePath == null)
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = " csv files(*.csv)|*.csv";
			//设置默认⽂件类型显⽰顺序
			saveFileDialog1.FilterIndex = 2;
			//保存对话框是否记忆上次打开的目录
			saveFileDialog1.RestoreDirectory = true;

			//点了保存按钮进⼊
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				//获得⽂件路径
				Data.saveFile = saveFileDialog1.FileName.ToString();

				Data.GUI2CSV(@Data.saveFile);

				MessageBox.Show("仿真模型参数数据导出成功！");
			}
		}

		//public static void GUI2CSV(string fn)
		//{
		//	// output gui.input
		//	using (System.IO.StreamWriter file = new System.IO.StreamWriter(fn, false, Encoding.Default))
		//	{
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("# 控制参数,,,,,,,,,,,,,,,");
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("start_time,end_time,delta_t,,,,,,,,,");
		//		file.WriteLine(Data.start_time.ToString() + ',' + Data.end_time.ToString() + ',' + Data.delta_t.ToString() + ',');
		//		file.WriteLine("cal_current,cal_valve,cal_pump,cal_balance_pipe,cal_mini_1,cal_mini_2,use_ff_static,IsMixed_circleType,cal_superSat_fickTrans,ec_pump_independent,,");
		//		file.WriteLine(Data.cal_current.ToString() + ',' + Data.cal_valve.ToString() + ',' + Data.cal_pump.ToString() + ',' + Data.cal_balance_pipe.ToString() + ',' + Data.cal_mini_1.ToString() + ','
		//			+ Data.cal_mini_2.ToString() + ',' + Data.use_ff_static.ToString() + ',' + Data.IsMixed_circleType.ToString() + ',' + Data.cal_superSat_fickTrans.ToString() + ',' + Data.ec_pump_independent.ToString() + ',');
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("# 几何参数,,,,,,,,,,,,,,,");
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("L_ca2se,L_an2se,D_sc,l_sc,,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.L_ca2se.ToString() + ',' + Data.L_an2se.ToString() + ',' + Data.D_sc.ToString() + ',' + Data.l_sc.ToString() + ',');
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("# flow和ps参数,,,,,,,,,,,,,,,");
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("flow,,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.n_flow.ToString() + ',');
		//		file.WriteLine("num,x_h2,x_o2,x_h2o,Di,L,v_t,,,,,,,,,,,,");
		//		for (int i = 0; i < Data.n_flow; i++)
		//		{
		//			file.WriteLine(Data.flow[i].num.ToString() + ',' + Data.flow[i].x_h2.ToString() + ',' + Data.flow[i].x_o2.ToString() + ',' + Data.flow[i].x_h2o.ToString() + ','
		//				+ Data.flow[i].Di.ToString() + ',' + Data.flow[i].L.ToString() + ',' + Data.flow[i].v_t.ToString() + ',');
		//		}
		//		file.WriteLine("ps,,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.n_ps.ToString() + ',');
		//		file.WriteLine("num,n,v,p,l_l,l_g,x_h2,x_o2,x_h2o,,,,,,,,,,");
		//		for (int i = 0; i < Data.n_ps; i++)
		//		{
		//			file.WriteLine(Data.ps[i].num.ToString() + ',' + Data.ps[i].n.ToString() + ',' + Data.ps[i].v.ToString() + ',' + Data.ps[i].p.ToString() + ',' + Data.ps[i].l_l.ToString() + ','
		//				 + Data.ps[i].l_g.ToString() + ',' + Data.ps[i].x_h2.ToString() + ',' + Data.ps[i].x_o2.ToString() + ',' + Data.ps[i].x_h2o.ToString() + ',');
		//		}
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("# 工艺参数,,,,,,,,,,,,,,,");
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("sigma_e_1,sigma_h2_r1,sigma_h2o_r1,sigma_e_2,sigma_h2o_r2,sigma_o2_r2,eta_F,F,n_cell,a_cell,A_mem,,,,,,,,,");
		//		file.WriteLine(Data.sigma_e_1.ToString() + ',' + Data.sigma_h2_r1.ToString() + ',' + Data.sigma_h2o_r1.ToString() + ',' + Data.sigma_e_2.ToString() + ',' + Data.sigma_h2o_r2.ToString() + ',' +
		//			Data.sigma_o2_r2.ToString() + ',' + Data.eta_F.ToString() + ',' + Data.F.ToString() + ',' + Data.n_cell.ToString() + ',' + Data.a_cell.ToString() + ',' + Data.A_mem.ToString() + ',');
		//		file.WriteLine("thickness_mem,porosity_mem,tortuosity_mem,wt_KOHsln,k,D_h2,D_o2,k_x_h2,k_x_o2,eps_h2_Darcy,eps_o2_Darcy,,,,,,,,,");
		//		file.WriteLine(Data.thickness_mem.ToString() + ',' + Data.porosity_mem.ToString() + ',' + Data.tortuosity_mem.ToString() + ',' + Data.wt_KOHsln.ToString() + ',' + Data.k.ToString() + ',' +
		//			Data.D_h2.ToString() + ',' + Data.D_o2.ToString() + ',' + Data.k_x_h2.ToString() + ',' + Data.k_x_o2.ToString() + ',' + Data.eps_h2_Darcy.ToString() + ',' + Data.eps_o2_Darcy.ToString() + ',');
		//		file.WriteLine("tao_b,FC_flash,R,T,eta,M_h2,M_o2,M_koh,M_h2o,rho_h2o,rho_h2,,,,,,,,,");
		//		file.WriteLine(Data.tao_b.ToString() + ',' + Data.FC_flash.ToString() + ',' + Data.R.ToString() + ',' + Data.T.ToString() + ',' + Data.eta.ToString() + ',' +
		//			Data.M_h2.ToString() + ',' + Data.M_o2.ToString() + ',' + Data.M_koh.ToString() + ',' + Data.M_h2o.ToString() + ',' + Data.rho_h2o.ToString() + ',' + Data.rho_h2.ToString() + ',');
		//		file.WriteLine("rho_o2,rho_sln_koh,g,Re24_0,mu,Area_hx,massFlowRate_cw,cv1,cv2,P_cathode_sep_out,P_anode_sep_out,,,,,,,,,");
		//		file.WriteLine(Data.rho_o2.ToString() + ',' + Data.rho_sln_koh.ToString() + ',' + Data.g.ToString() + ',' + Data.Re24_0.ToString() + ',' + Data.mu.ToString() + ',' +
		//			Data.Area_hx.ToString() + ',' + Data.massFlowRate_cw.ToString() + ',' + Data.cv1.ToString() + ',' + Data.cv2.ToString() + ',' + Data.P_cathode_sep_out.ToString() + ',' + Data.P_anode_sep_out.ToString() + ',');
		//		file.WriteLine("P_env,,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.P_env.ToString() + ',');
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("# 部件参数,,,,,,,,,,,,,,,");
		//		file.WriteLine("###########################,,,,,,,,,,,,,,,");
		//		file.WriteLine("Part_one,,,,,,,,,,,,,,,");
		//		file.WriteLine("electrolyzer,,,,,,,,,,,,,,,");
		//		file.WriteLine("个数,,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.n_ele.ToString() + ',');
		//		for (int i = 0; i < Data.n_ele; i++)
		//		{
		//			file.WriteLine("I_current,i_flow,i_ps,,,,,,,,,,,,,");
		//			file.WriteLine(Data.ele[i].I_current.ToString() + ',' + Data.ele[i].i_flow.ToString() + ',' + Data.ele[i].i_ps.ToString() + ',');
		//			file.WriteLine("flow号,,,,,,,,,,,,,,,");
		//			string item_m = "";
		//			for (int j = 0; j < Data.ele[i].i_flow; j++)
		//			{
		//				item_m = item_m + Data.ele[i].flow[j].ToString() + ",";
		//			}
		//			file.WriteLine(item_m);
		//			file.WriteLine("ps号,,,,,,,,,,,,,,,");
		//			string item_n = "";
		//			for (int l = 0; l < Data.ele[i].i_ps; l++)
		//			{
		//				item_n = item_n + Data.ele[i].ps[l].ToString() + ",";
		//			}
		//			file.WriteLine(item_n);
		//		}

		//		file.WriteLine("############,,,,,,,,,,,,,,,");
		//		file.WriteLine("Part_two,,,,,,,,,,,,,,,");
		//		file.WriteLine("Cathode_separator,,,,,,,,,,,,,,,");
		//		file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.Cathode_separator.i_flow.ToString() + ',' + Data.Cathode_separator.i_ps.ToString() + ',');
		//		file.WriteLine("flow号,,,,,,,,,,,,,,,");
		//		string item_f = "";
		//		for (int j = 0; j < Data.Cathode_separator.i_flow; j++)
		//		{
		//			item_f = item_f + Data.Cathode_separator.flow[j].ToString() + ",";
		//		}
		//		file.WriteLine(item_f);
		//		file.WriteLine("ps号,,,,,,,,,,,,,,,");
		//		string item_p = "";
		//		for (int l = 0; l < Data.Cathode_separator.i_ps; l++)
		//		{
		//			item_p = item_p + Data.Cathode_separator.ps[l].ToString() + ",";
		//		}
		//		file.WriteLine(item_p);

				
		//		//file.WriteLine("Cathode_separator,,,,,,,,,,,,,,,");
		//		//file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
		//		//file.WriteLine(Data.Cathode_separator.i_flow.ToString() + ',' + Data.Cathode_separator.i_ps.ToString() + ',');
		//		//file.WriteLine("flow号,,,,,,,,,,,,,,,");
		//		//item_f = "";
		//		//for (int j = 0; j < Data.Cathode_separator.i_flow; j++)
		//		//{
		//		//	item_f = item_f + Data.Cathode_separator.flow[j].ToString() + ",";
		//		//}
		//		//file.WriteLine(item_f);
		//		//file.WriteLine("ps号,,,,,,,,,,,,,,,");
		//		//item_p = "";
		//		//for (int l = 0; l < Data.Cathode_separator.i_ps; l++)
		//		//{
		//		//	item_p = item_p + Data.Cathode_separator.ps[l].ToString() + ",";
		//		//}
		//		//file.WriteLine(item_p);
				

		//		file.WriteLine("Anode_separator,,,,,,,,,,,,,,,");
		//		file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.Anode_separator.i_flow.ToString() + ',' + Data.Anode_separator.i_ps.ToString() + ',');
		//		file.WriteLine("flow号,,,,,,,,,,,,,,,");
		//		item_f = "";
		//		for (int j = 0; j < Data.Anode_separator.i_flow; j++)
		//		{
		//			item_f = item_f + Data.Anode_separator.flow[j].ToString() + ",";
		//		}
		//		file.WriteLine(item_f);
		//		file.WriteLine("ps号,,,,,,,,,,,,,,,");
		//		item_p = "";
		//		for (int l = 0; l < Data.Anode_separator.i_ps; l++)
		//		{
		//			item_p = item_p + Data.Anode_separator.ps[l].ToString() + ",";
		//		}
		//		file.WriteLine(item_p);

		//		file.WriteLine("############,,,,,,,,,,,,,,,");
		//		file.WriteLine("Part_three,,,,,,,,,,,,,,,");
		//		file.WriteLine("Balance_line,,,,,,,,,,,,,,,");
		//		file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.Balance_line.i_flow.ToString() + ',' + Data.Balance_line.i_ps.ToString() + ',');
		//		file.WriteLine("flow号,,,,,,,,,,,,,,,");
		//		item_f = "";
		//		for (int j = 0; j < Data.Balance_line.i_flow; j++)
		//		{
		//			item_f = item_f + Data.Balance_line.flow[j].ToString() + ",";
		//		}
		//		file.WriteLine(item_f);
		//		file.WriteLine("ps号,,,,,,,,,,,,,,,");
		//		item_p = "";
		//		for (int l = 0; l < Data.Balance_line.i_ps; l++)
		//		{
		//			item_p = item_p + Data.Balance_line.ps[l].ToString() + ",";
		//		}
		//		file.WriteLine(item_p);

		//		file.WriteLine("Cathode_valve,,,,,,,,,,,,,,,");
		//		file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.Cathode_valve.i_flow.ToString() + ',' + Data.Cathode_valve.i_ps.ToString() + ',');
		//		file.WriteLine("flow号,,,,,,,,,,,,,,,");
		//		item_f = "";
		//		for (int j = 0; j < Data.Cathode_valve.i_flow; j++)
		//		{
		//			item_f = item_f + Data.Cathode_valve.flow[j].ToString() + ",";
		//		}
		//		file.WriteLine(item_f);
		//		file.WriteLine("ps号,,,,,,,,,,,,,,,");
		//		item_p = "";
		//		for (int l = 0; l < Data.Cathode_valve.i_ps; l++)
		//		{
		//			item_p = item_p + Data.Cathode_valve.ps[l].ToString() + ",";
		//		}
		//		file.WriteLine(item_p);

		//		file.WriteLine("Anode_valve,,,,,,,,,,,,,,,");
		//		file.WriteLine("i_flow,i_ps,,,,,,,,,,,,,,");
		//		file.WriteLine(Data.Anode_valve.i_flow.ToString() + ',' + Data.Anode_valve.i_ps.ToString() + ',');
		//		file.WriteLine("flow号,,,,,,,,,,,,,,,");
		//		item_f = "";
		//		for (int j = 0; j < Data.Anode_valve.i_flow; j++)
		//		{
		//			item_f = item_f + Data.Anode_valve.flow[j].ToString() + ",";
		//		}
		//		file.WriteLine(item_f);
		//		file.WriteLine("ps号,,,,,,,,,,,,,,,");
		//		item_p = "";
		//		for (int l = 0; l < Data.Anode_valve.i_ps; l++)
		//		{
		//			item_p = item_p + Data.Anode_valve.ps[l].ToString() + ",";
		//		}
		//		file.WriteLine(item_p);
		//	}
		//}

		private void Form2_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			if (Data.caseUsePath == "" || Data.caseUsePath == null)
			{
				MessageBox.Show("请先指定工作目录！");
				return;
			}

			Process proc = null;
			try
			{
				proc = new Process();
				proc.StartInfo.WorkingDirectory = Data.exePath;
				proc.StartInfo.FileName = "aeSLN.exe";
				proc.StartInfo.CreateNoWindow = true;
				proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				proc.Start();
				proc.WaitForExit();
				MessageBox.Show("计算完成！");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace.ToString());
			}
		}

		private void CopyFolder(string sourceFolder, string destFolder)
		{
			if (!Directory.Exists(destFolder))
			{
				Directory.CreateDirectory(destFolder);
			}

			string[] files = Directory.GetFiles(sourceFolder);
			foreach (string file in files)
			{
				string name = Path.GetFileName(file);
				string dest = Path.Combine(destFolder, name);
				//Console.WriteLine("name = {0}, dest = {1} ", name, dest);
				if (name == "data_input.csv")
				{
					File.Copy(file, dest);
				}
			}

			string[] folders = Directory.GetDirectories(sourceFolder);
			foreach (string folder in folders)
			{
				string name = Path.GetFileName(folder);
				string dest = Path.Combine(destFolder, name);
				if (name == "output.data")
				{
					CopyFolder(folder, dest);
				}
			}
		}

		private void Form2_Resize(object sender, EventArgs e)
		{
			label1.Parent = splitContainer1.Panel1;
			label1.Location = new Point((splitContainer1.Panel1.Width - label1.Width) / 2, (splitContainer1.Panel1.Height - label1.Height) / 2);
		}

		private void CSV2Data(string fn)
		{
			bool flag = true;
			//string str1;
			using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(fn)))
			{
				using (StreamReader sR1 = new StreamReader(ms, Encoding.Default))
				{

					//flow初始化，保证flow有值
					for (int i = 0; i < Data.n_flow_max; i++)
					{
						Data.flow[i] = new FlowData();
					}

					//flow fault被勾选的数据初始化，保证flow有值
					for (int i = 0; i < Data.n_flow_max; i++)
					{
						Data.flow_f[i] = new FlowData();
					}

					//fault-flow初始化，保证fault有值
					for (int i = 0; i < Data.n_faultflow_max; i++)
					{
						Data.faultflow[i] = new FaultFlowData();    //故障注入                
					}

					//部件值初始化，保证部件有值
					for (int i = 0; i < Data.n_ps_max; i++)
					{
						Data.ps[i] = new PsData();
					}

					//ps fault被勾选的数据初始化，保证有值
					for (int i = 0; i < Data.n_ps_max; i++)
					{
						Data.ps_f[i] = new PsData();
					}

					//fault-ps初始化，保证fault有值
					for (int i = 0; i < Data.n_faultps_max; i++)
					{
						Data.faultps[i] = new FaultPsData();    //故障注入                
					}

					//
					for (int i = 0; i < Data.n_node_max; i++)
					{
						Data.ele[i] = new NodeData();
					}

					//-------------------------控制参数------------------------
					//读取csv文件
					string nextLine = "";

					nextLine = sR1.ReadLine(); //# 控制参数
					nextLine = sR1.ReadLine(); //# 控制参数
					nextLine = sR1.ReadLine(); //# 控制参数
					nextLine = sR1.ReadLine(); //# 控制参数
					nextLine = sR1.ReadLine(); //# 控制参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && double.TryParse(tmp[0], out Data.start_time);
						flag = flag && double.TryParse(tmp[1], out Data.end_time);
						flag = flag && double.TryParse(tmp[2], out Data.delta_t);
						Console.WriteLine("start_time = {0} ,end_time = {1}, delta_t = {2} ", Data.start_time, Data.end_time, Data.delta_t);
					}
					nextLine = sR1.ReadLine(); //# 控制参数
					nextLine = sR1.ReadLine(); //# 控制参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && bool.TryParse(tmp[0], out Data.cal_current);
						flag = flag && bool.TryParse(tmp[1], out Data.cal_valve);
						flag = flag && bool.TryParse(tmp[2], out Data.cal_pump);
						flag = flag && bool.TryParse(tmp[3], out Data.cal_balance_pipe);
						flag = flag && bool.TryParse(tmp[4], out Data.cal_mini_1);
						flag = flag && bool.TryParse(tmp[5], out Data.cal_mini_2);
						flag = flag && bool.TryParse(tmp[6], out Data.use_ff_static);
						flag = flag && bool.TryParse(tmp[7], out Data.IsMixed_circleType);
					}
                    nextLine = sR1.ReadLine(); //# 控制参数
                    nextLine = sR1.ReadLine(); //# 控制参数
                    {
                        string[] tmp = nextLine.Split(',');
                        flag = flag && bool.TryParse(tmp[0], out Data.cal_superSat_fickTrans);
                        flag = flag && bool.TryParse(tmp[1], out Data.cal_ShellTube_heatExchanger);
                        flag = flag && bool.TryParse(tmp[2], out Data.cal_Ele_heater);
                    }
                    nextLine = sR1.ReadLine(); //# 几何参数
					nextLine = sR1.ReadLine(); //# 几何参数
					nextLine = sR1.ReadLine(); //# 几何参数
					nextLine = sR1.ReadLine(); //# 几何参数
					nextLine = sR1.ReadLine(); //# 几何参数
					{
						string[] tmp = nextLine.Split(',');
                        flag = flag && double.TryParse(tmp[0], out Data.L_ca2se);
						flag = flag && double.TryParse(tmp[1], out Data.L_an2se);
						flag = flag && double.TryParse(tmp[2], out Data.D_sc);
						flag = flag && double.TryParse(tmp[3], out Data.l_sc);
                        flag = flag && double.TryParse(tmp[4], out Data.thickness_cat);
                        flag = flag && double.TryParse(tmp[5], out Data.thickness_ano);
                        flag = flag && double.TryParse(tmp[6], out Data.distance_am);
                        flag = flag && double.TryParse(tmp[7], out Data.distance_cm);
                    }
                    nextLine = sR1.ReadLine(); //# 几何参数
                    nextLine = sR1.ReadLine(); //# 几何参数
                    {
                        string[] tmp = nextLine.Split(',');
                        flag = flag && double.TryParse(tmp[0], out Data.Volume_hotside);
                        flag = flag && double.TryParse(tmp[1], out Data.Volume_codeside);
                        flag = flag && double.TryParse(tmp[2], out Data.di_stack);
                        flag = flag && double.TryParse(tmp[3], out Data.Area_sep);
                        flag = flag && double.TryParse(tmp[4], out Data.Area_stack);
                        flag = flag && double.TryParse(tmp[5], out Data.C_tsep);
                        flag = flag && double.TryParse(tmp[6], out Data.C_tk);
                    }
                    nextLine = sR1.ReadLine(); //# flow和ps参数
					nextLine = sR1.ReadLine(); //# flow和ps参数
					nextLine = sR1.ReadLine(); //# flow和ps参数
					nextLine = sR1.ReadLine(); //# flow和ps参数
					nextLine = sR1.ReadLine(); //# flow和ps参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && int.TryParse(tmp[0], out Data.n_flow);
						Console.WriteLine("n_flow = {0} ", Data.n_flow);
					}
					nextLine = sR1.ReadLine(); //# flow参数
					for (int i = 0; i < Data.n_flow; i++)
					{
						nextLine = sR1.ReadLine();
						{
							string[] tmp = nextLine.Split(',');
							Data.flow[i].num = int.Parse(tmp[0]);
							Data.flow[i].x_h2 = double.Parse(tmp[1]);
							Data.flow[i].x_o2 = double.Parse(tmp[2]);
							Data.flow[i].x_h2o = double.Parse(tmp[3]);
							Data.flow[i].Di = double.Parse(tmp[4]);
							Data.flow[i].L = double.Parse(tmp[5]);
						}
					}
					nextLine = sR1.ReadLine(); //# ps参数
					nextLine = sR1.ReadLine(); //# ps参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && int.TryParse(tmp[0], out Data.n_ps);
						Console.WriteLine("n_ps = {0} ", Data.n_ps);
					}
					nextLine = sR1.ReadLine(); //# ps参数
					for (int i = 0; i < Data.n_ps; i++)
					{
						nextLine = sR1.ReadLine();
						{
							string[] tmp = nextLine.Split(',');
							Data.ps[i].num = int.Parse(tmp[0]);
							Data.ps[i].n = double.Parse(tmp[1]);
							Data.ps[i].v = double.Parse(tmp[2]);
							Data.ps[i].p = double.Parse(tmp[3]);
							Data.ps[i].l_l = double.Parse(tmp[4]);
							Data.ps[i].l_g = double.Parse(tmp[5]);
							Data.ps[i].x_h2 = double.Parse(tmp[6]);
							Data.ps[i].x_o2 = double.Parse(tmp[7]);
							Data.ps[i].x_h2o = double.Parse(tmp[8]);
							Console.WriteLine("num = {0} ,n = {1}, v = {2}, p = {3}, l_l = {4}, l_g = {5}, " +
							"x_h2 = {6}, x_o2 = {7}, x_h2o = {8}",
							Data.ps[i].num, Data.ps[i].n, Data.ps[i].v, Data.ps[i].p, Data.ps[i].l_l, Data.ps[i].l_g, Data.ps[i].x_h2,
							Data.ps[i].x_o2, Data.ps[i].x_h2o);
						}
					}
					nextLine = sR1.ReadLine(); //# 工艺参数
					nextLine = sR1.ReadLine(); //# 工艺参数
					nextLine = sR1.ReadLine(); //# 工艺参数
					nextLine = sR1.ReadLine(); //# 工艺参数
					nextLine = sR1.ReadLine(); //# 工艺参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && double.TryParse(tmp[0], out Data.sigma_e_1);
						flag = flag && double.TryParse(tmp[1], out Data.sigma_h2_r1);
						flag = flag && double.TryParse(tmp[2], out Data.sigma_h2o_r1);
						flag = flag && double.TryParse(tmp[3], out Data.sigma_e_2);
						flag = flag && double.TryParse(tmp[4], out Data.sigma_h2o_r2);
						flag = flag && double.TryParse(tmp[5], out Data.sigma_o2_r2);
						flag = flag && double.TryParse(tmp[6], out Data.eta_F);
						flag = flag && double.TryParse(tmp[7], out Data.F);
						flag = flag && double.TryParse(tmp[8], out Data.n_cell);
						flag = flag && double.TryParse(tmp[9], out Data.a_cell);
						flag = flag && double.TryParse(tmp[10], out Data.A_mem);
						Console.WriteLine("sigma_e_1 = {0} ,sigma_h2_r1 = {1}, sigma_h2o_r1 = {2}, sigma_e_2 = {3}, sigma_h2o_r2 = {4}, sigma_o2_r2 = {5}, " +
							"eta_F = {6}, F = {7}, n_cell = {8},  a_cell = {9}, A_mem = {10}",
							Data.sigma_e_1, Data.sigma_h2_r1, Data.sigma_h2o_r1, Data.sigma_e_2, Data.sigma_h2o_r2, Data.sigma_o2_r2, Data.eta_F,
							Data.F, Data.n_cell, Data.a_cell, Data.A_mem);
					}
					nextLine = sR1.ReadLine(); //# 工艺参数
					nextLine = sR1.ReadLine(); //# 工艺参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && double.TryParse(tmp[0], out Data.thickness_mem);
						flag = flag && double.TryParse(tmp[1], out Data.porosity_mem);
						flag = flag && double.TryParse(tmp[2], out Data.tortuosity_mem);
						flag = flag && double.TryParse(tmp[3], out Data.wt_KOHsln);
						flag = flag && double.TryParse(tmp[4], out Data.k);
						flag = flag && double.TryParse(tmp[5], out Data.D_h2);
						flag = flag && double.TryParse(tmp[6], out Data.D_o2);
						flag = flag && double.TryParse(tmp[7], out Data.k_x_h2);
						flag = flag && double.TryParse(tmp[8], out Data.k_x_o2);
						flag = flag && double.TryParse(tmp[9], out Data.eps_h2_Darcy);
						flag = flag && double.TryParse(tmp[10], out Data.eps_o2_Darcy);
						Console.WriteLine("thickness_mem = {0} ,porosity_mem = {1}, tortuosity_mem = {2}, wt_KOHsln = {3}, k = {4}, D_h2 = {5}, " +
							"D_o2 = {6}, k_x_h2 = {7}, k_x_o2 = {8},  eps_h2_Darcy = {9}, eps_o2_Darcy = {10}",
							Data.thickness_mem, Data.porosity_mem, Data.tortuosity_mem, Data.wt_KOHsln, Data.k, Data.D_h2, Data.D_o2,
							Data.k_x_h2, Data.k_x_o2, Data.eps_h2_Darcy, Data.eps_o2_Darcy);
					}
					nextLine = sR1.ReadLine(); //# 工艺参数
					nextLine = sR1.ReadLine(); //# 工艺参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && double.TryParse(tmp[0], out Data.tao_b);
						flag = flag && double.TryParse(tmp[1], out Data.FC_flash);
						flag = flag && double.TryParse(tmp[2], out Data.R);
						flag = flag && double.TryParse(tmp[3], out Data.T);
						flag = flag && double.TryParse(tmp[4], out Data.eta);
						flag = flag && double.TryParse(tmp[5], out Data.M_h2);
						flag = flag && double.TryParse(tmp[6], out Data.M_o2);
						flag = flag && double.TryParse(tmp[7], out Data.M_koh);
						flag = flag && double.TryParse(tmp[8], out Data.M_h2o);
						flag = flag && double.TryParse(tmp[9], out Data.rho_h2o);
						flag = flag && double.TryParse(tmp[10], out Data.rho_h2);
						Console.WriteLine("tao_b = {0} ,FC_flash = {1}, R = {2}, T = {3}, eta = {4}, M_h2 = {5}, " +
							"M_o2 = {6}, M_koh = {7}, M_h2o = {8},  rho_h2o = {9}, rho_h2 = {10}",
							Data.tao_b, Data.FC_flash, Data.R, Data.T, Data.eta, Data.M_h2, Data.M_o2,
							Data.M_koh, Data.M_h2o, Data.rho_h2o, Data.rho_h2);
					}
					nextLine = sR1.ReadLine(); //# 工艺参数
					nextLine = sR1.ReadLine(); //# 工艺参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && double.TryParse(tmp[0], out Data.rho_o2);
						flag = flag && double.TryParse(tmp[1], out Data.rho_sln_koh);
						flag = flag && double.TryParse(tmp[2], out Data.g);
						flag = flag && double.TryParse(tmp[3], out Data.Re24_0);
						flag = flag && double.TryParse(tmp[4], out Data.mu);
						flag = flag && double.TryParse(tmp[5], out Data.Area_hx);
						flag = flag && double.TryParse(tmp[6], out Data.massFlowRate_cw);
						flag = flag && double.TryParse(tmp[7], out Data.cv1);
						flag = flag && double.TryParse(tmp[8], out Data.cv2);
						flag = flag && double.TryParse(tmp[9], out Data.P_cathode_sep_out);
						flag = flag && double.TryParse(tmp[10], out Data.P_anode_sep_out);
						Console.WriteLine("rho_o2 = {0} ,rho_sln_koh = {1}, g = {2}, Re24_0 = {3}, mu = {4}, Area_hx = {5}, " +
							"massFlowRate_cw = {6}, cv1 = {7}, cv2 = {8},  P_cathode_sep_out = {9}, P_anode_sep_out = {10}",
							Data.rho_o2, Data.rho_sln_koh, Data.g, Data.Re24_0, Data.mu, Data.Area_hx, Data.massFlowRate_cw,
							Data.cv1, Data.cv2, Data.P_cathode_sep_out, Data.P_anode_sep_out);
					}
					nextLine = sR1.ReadLine(); //# 工艺参数
					nextLine = sR1.ReadLine(); //# 工艺参数
					{
						string[] tmp = nextLine.Split(',');
						flag = flag && double.TryParse(tmp[0], out Data.P_env);
						Console.WriteLine("P_env = {0} ", Data.P_env);
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 电解槽部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.n_ele = int.Parse(tmp[0]);
						Console.WriteLine("n_ele = {0} ", Data.n_ele);
						Data.n_node = 3;
					}
					for (int i = 0; i < Data.n_ele; i++)
					{
						Data.ele[i].node_name = "electrolyzer";
						nextLine = sR1.ReadLine();
						nextLine = sR1.ReadLine();
						{
							string[] tmp = nextLine.Split(',');
							Data.ele[i].I_current = double.Parse(tmp[0]);
							Data.ele[i].i_flow = int.Parse(tmp[1]);
							Data.ele[i].i_ps = int.Parse(tmp[2]);
							Console.WriteLine("I_current = {0}, i_flow = {1}, i_ps = {2} ", Data.ele[i].I_current, Data.ele[i].i_flow, Data.ele[i].i_ps);
						}
						nextLine = sR1.ReadLine();
						nextLine = sR1.ReadLine();
						{
							string[] tmp = nextLine.Split(',');
							Data.ele[i].flow = new int[Data.ele[i].i_flow];
							for (int j = 0; j < Data.ele[i].i_flow; j++)
							{
								Data.ele[i].flow[j] = int.Parse(tmp[j]);
							}
						}
						nextLine = sR1.ReadLine();
						nextLine = sR1.ReadLine();
						{
							string[] tmp = nextLine.Split(',');
							Data.ele[i].ps = new int[Data.ele[i].i_ps];
							for (int j = 0; j < Data.ele[i].i_ps; j++)
							{
								Data.ele[i].ps[j] = int.Parse(tmp[j]);
							}
						}
					}

					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 阴极分离器部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Cathode_separator.i_flow = int.Parse(tmp[0]);
						Data.Cathode_separator.i_ps = int.Parse(tmp[1]);
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Cathode_separator.flow = new int[Data.Cathode_separator.i_flow];
						for (int j = 0; j < Data.Cathode_separator.i_flow; j++)
						{
							Data.Cathode_separator.flow[j] = int.Parse(tmp[j]);
						}
					}
					nextLine = sR1.ReadLine();
					nextLine = sR1.ReadLine();
					{
						string[] tmp = nextLine.Split(',');
						Data.Cathode_separator.ps = new int[Data.Cathode_separator.i_ps];
						for (int j = 0; j < Data.Cathode_separator.i_ps; j++)
						{
							Data.Cathode_separator.ps[j] = int.Parse(tmp[j]);
						}
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 阳极分离器部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Anode_separator.i_flow = int.Parse(tmp[0]);
						Data.Anode_separator.i_ps = int.Parse(tmp[1]);
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Anode_separator.flow = new int[Data.Anode_separator.i_flow];
						for (int j = 0; j < Data.Anode_separator.i_flow; j++)
						{
							Data.Anode_separator.flow[j] = int.Parse(tmp[j]);
						}
					}
					nextLine = sR1.ReadLine();
					nextLine = sR1.ReadLine();
					{
						string[] tmp = nextLine.Split(',');
						Data.Anode_separator.ps = new int[Data.Anode_separator.i_ps];
						for (int j = 0; j < Data.Anode_separator.i_ps; j++)
						{
							Data.Anode_separator.ps[j] = int.Parse(tmp[j]);
						}
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 平衡管线部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Balance_line.i_flow = int.Parse(tmp[0]);
						Data.Balance_line.i_ps = int.Parse(tmp[1]);
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Balance_line.flow = new int[Data.Balance_line.i_flow];
						for (int j = 0; j < Data.Balance_line.i_flow; j++)
						{
							//Data.Balance_line.flow[j] = int.Parse(tmp[j]);

                            if (tmp.Length <= Data.Balance_line.i_flow)
							{
								if (j < tmp.Length)
								{
									Data.Balance_line.flow[j] = int.Parse(tmp[j].Trim()==""?"0":tmp[j].Trim());
								}
								else
								{ Data.Balance_line.flow[j] = 0; }
							}
							else
							{
								Data.Balance_line.flow[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                            }
						}
					}
					nextLine = sR1.ReadLine();
					nextLine = sR1.ReadLine();
					{
						string[] tmp = nextLine.Split(',');
						Data.Balance_line.ps = new int[Data.Balance_line.i_ps];
						for (int j = 0; j < Data.Balance_line.i_ps; j++)
						{
							//Data.Balance_line.ps[j] = int.Parse(tmp[j]);
                            if (tmp.Length <= Data.Balance_line.i_ps)
                            {
                                if (j < tmp.Length)
                                {
                                    Data.Balance_line.ps[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                                }
                                else
                                { Data.Balance_line.ps[j] = 0; }
                            }
                            else
                            {
                                Data.Balance_line.ps[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                            }
                        }
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 阴极阀门部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Cathode_valve.i_flow = int.Parse(tmp[0]);
						Data.Cathode_valve.i_ps = int.Parse(tmp[1]);
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Cathode_valve.flow = new int[Data.Cathode_valve.i_flow];
						for (int j = 0; j < Data.Cathode_valve.i_flow; j++)
                        {
							//Data.Cathode_valve.flow[j] = int.Parse(tmp[j]);
                            if (tmp.Length <= Data.Cathode_valve.i_flow)
                            {
                                if (j < tmp.Length)
                                {
                                    Data.Cathode_valve.flow[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                                }
                                else
                                { Data.Cathode_valve.flow[j] = 0; }
                            }
                            else
                            {
                                Data.Cathode_valve.flow[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                            }
                        }
					}
					nextLine = sR1.ReadLine();
					nextLine = sR1.ReadLine();
					{
						string[] tmp = nextLine.Split(',');
						Data.Cathode_valve.ps = new int[Data.Cathode_valve.i_ps];
						for (int j = 0; j < Data.Cathode_valve.i_ps; j++)
						{
							//Data.Cathode_valve.ps[j] = int.Parse(tmp[j]);
                            if (tmp.Length <= Data.Cathode_valve.i_ps)
                            {
                                if (j < tmp.Length)
                                {
                                    Data.Cathode_valve.ps[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                                }
                                else
                                { Data.Cathode_valve.ps[j] = 0; }
                            }
                            else
                            {
                                Data.Cathode_valve.ps[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                            }
                        }
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 阳极阀门部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Anode_valve.i_flow = int.Parse(tmp[0]);
						Data.Anode_valve.i_ps = int.Parse(tmp[1]);
					}
					nextLine = sR1.ReadLine(); //# 部件参数
					nextLine = sR1.ReadLine(); //# 部件参数
					{
						string[] tmp = nextLine.Split(',');
						Data.Anode_valve.flow = new int[Data.Anode_valve.i_flow];
						for (int j = 0; j < Data.Anode_valve.i_flow; j++)
						{
							//Data.Anode_valve.flow[j] = int.Parse(tmp[j]);
                            if (tmp.Length <= Data.Anode_valve.i_flow)
                            {
                                if (j < tmp.Length)
                                {
                                    Data.Anode_valve.flow[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                                }
                                else
                                { Data.Anode_valve.flow[j] = 0; }
                            }
                            else
                            {
                                Data.Anode_valve.flow[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                            }
                        }
					}
					nextLine = sR1.ReadLine();
					nextLine = sR1.ReadLine();
					{
						string[] tmp = nextLine.Split(',');
						Data.Anode_valve.ps = new int[Data.Anode_valve.i_ps];
						for (int j = 0; j < Data.Anode_valve.i_ps; j++)
						{
							//Data.Anode_valve.ps[j] = int.Parse(tmp[j]);
                            if (tmp.Length <= Data.Anode_valve.i_ps)
                            {
                                if (j < tmp.Length)
                                {
                                    Data.Anode_valve.ps[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                                }
                                else
                                { Data.Anode_valve.ps[j] = 0; }
                            }
                            else
                            {
                                Data.Anode_valve.ps[j] = int.Parse(tmp[j].Trim() == "" ? "0" : tmp[j].Trim());
                            }
                        }
					}

					int a = 0;
					for (int i = 0; i < Data.n_flow; i++)
					{
						Data.faultflow[a].num = Data.flow[i].num;
						Data.faultflow[a].x_h2 = Data.flow[i].x_h2;
						Data.faultflow[a].x_o2 = Data.flow[i].x_o2;
						Data.faultflow[a].x_h2o = Data.flow[i].x_h2o;
						Data.faultflow[a].L = Data.flow[i].L;
						Data.faultflow[a].is_fault = false;
						Data.faultflow[a].is_result = false;
						a++;
					}

					int b = 0;
					for (int i = 0; i < Data.n_ps; i++)
					{
						Data.faultps[b].num = Data.ps[i].num;
						Data.faultps[b].n = Data.ps[i].n;
						Data.faultps[b].v = Data.ps[i].v;
						Data.faultps[b].p = Data.ps[i].p;
						Data.faultps[b].l_l = Data.ps[i].l_l;
						Data.faultps[b].l_g = Data.ps[i].l_g;
						Data.faultps[b].x_h2 = Data.ps[i].x_h2;
						Data.faultps[b].x_o2 = Data.ps[i].x_o2;
						Data.faultps[b].x_h2o = Data.ps[i].x_h2o;
						Data.faultps[b].is_fault = false;
						Data.faultps[b].is_result = false;
						b++;
					}

					sR1.Close();
					MessageBox.Show("数据导入成功！");
				}
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
            //zhangax 2024-04-24 修改Bug

            // 运行用户加载上次成功载入的工作目录路径（20240320，由M添加）
            string selectedFolder = string.Empty;

			// 检查历史工作路径是否存在
			string historypath = Path.Combine(Data.exePath, "history.csv");
			if ((Data.caseUsePath == null || Data.caseUsePath == string.Empty) && File.Exists(historypath) &&
				MessageBox.Show("是否加载历史工作目录？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				selectedFolder = File.ReadAllText(historypath);
			}
			else
			{
				// 用户放弃加载历史工作目录或未加载成功时令用户重新指定工作目录
				using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
				{
					if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					{
						selectedFolder = folderBrowserDialog.SelectedPath;
					}
					else return;
				}
			}
			string case_name;
			string datainput_path = Path.Combine(selectedFolder, "data_input.csv");
			if (Directory.Exists(selectedFolder))
			{
				// 获取case1文件夹的绝对路径
				string case1Path = selectedFolder;

				int lastBackslashIndex = case1Path.LastIndexOf('\\');
				if (lastBackslashIndex != -1)
				{
					case1Path = case1Path.Substring(lastBackslashIndex + 1);
				}

				// 将绝对路径写入文件
				string filePath = Path.Combine(Data.exePath, "case_path.csv");
				File.WriteAllText(filePath, selectedFolder);
				case_name = case1Path;
				Data.case_name = case1Path;
				Data.caseUsePath = selectedFolder;
				//判断选中的工况文件夹下面是否有data_input文件
				string targetFileName = "data_input.csv";

				// 获取case1文件夹下的所有文件路径
				string[] files = Directory.GetFiles(selectedFolder);

				// 遍历所有文件，检查是否存在目标文件
				bool fileExists = false;
				foreach (string file in files)
				{
					if (Path.GetFileName(file) == targetFileName)
					{
						fileExists = true;
						break;
					}
				}

				if (fileExists)
				{
					MessageBox.Show("工作目录指定成功！指定的工况为：" + case_name);

					// 储存为历史工作路径，方便下次启动（20240320，由M添加）
					File.WriteAllText(historypath, selectedFolder);
				}
				else
				{
					string selectedPath = selectedFolder;
					// 判断是否包含中文
					bool containsChinese = Regex.IsMatch(selectedPath, @"[\u4e00-\u9fa5]");
					if (containsChinese == true)
					{
						MessageBox.Show("文件夹路径不能包含中文！");
						return;
					}

					// 判断是否包含空格
					bool containsSpace = selectedPath.Contains(" ");
					if (containsSpace == true)
					{
						MessageBox.Show("文件夹路径不能包含空格！");
						return;
					}

					// 拷贝文件夹
					string sourceFolderPath = Data.casePath;
					string destinationFolderPath = selectedPath;
					CopyFolder(sourceFolderPath, destinationFolderPath);
					MessageBox.Show("新建工况成功！工作目录指定为：" + case_name);
				}
			}
			else
			{
				MessageBox.Show("文件夹路径不存在！");
				return;
			}

			// 导入仿真参数数据  
			Data.fzxt_name = case_name;

			// 读取data_input.csv文件的内容
			//try
			//{
				string fileContent = File.ReadAllText(datainput_path);
				CSV2Data(fileContent);
            //}
            //catch (Exception myException)
            //{
            //    MessageBox.Show("文件是打开状态，请检查后重试！", "提示");
            //}
            //ReadCsv(datainput_path);
            label2.Text = "工作目录指定完毕";
        }

		private void button3_Click(object sender, EventArgs e)
		{
			using (Form6 fs = new Form6())
			{
				fs.ShowDialog();
				this.refresh10();
			}
		}

		public void refresh10()
		{
			splitContainer4.Panel2.Controls.Clear();
			ucd10 = new UserControl10();
			ucd10.Dock = DockStyle.Fill;
			ucd10.Parent = this.splitContainer4.Panel2;
			splitContainer4.Panel2.Controls.Add(ucd10);
			comboBox1.Visible = false;
			button2.Visible = false;
			button1.Visible = false;
			button3.Visible = true;
			button4.Visible = false;
		}

		private DataSet ReadCsv(string filepath)
		{
			if (Path.GetExtension(filepath).ToLower() == ".csv" && File.Exists(filepath))
			{
				Regex rSharps = new(@"(#+(?!\s))", RegexOptions.Compiled);
				Regex rTableName = new(@"(?<=#)\s(\w*\p{IsCJKUnifiedIdeographs}+)", RegexOptions.Compiled);
				Regex rHeader = new(@"(?<!#\s)(?:\b[A-Za-z_]\w*)", RegexOptions.Compiled);
				using StreamReader sr = new(filepath);
				string dataStr = sr.ReadToEnd();
				string[] dataLines = dataStr.Split('\n');
				Console.WriteLine("Lines of input file: {0}", dataLines.Length);
				DataSet dataSet = new();
				for (int k = 0; k < dataLines.Length; k++)
				{
					// ##分隔行
					if (rSharps.IsMatch(dataLines[k]))
					{
						// # 标题行
						if (rTableName.IsMatch(dataLines[k + 1]))
						{
							DataTable table = new()
							{
								// 获取数据表标题
								TableName = rTableName.Matches(dataLines[++k])[0].Groups[1].Value
							};
							Console.WriteLine("--Table:{0}--", table.TableName);
							// 越过##分隔行
							k += 2;
							// 检查列表头
							if (rHeader.IsMatch(dataLines[k]))
							{
								MatchCollection Headers = rHeader.Matches(dataLines[k]);
								foreach (Match header in Headers)
								{
									table.Columns.Add(header.Value, typeof(double));
									Console.WriteLine(header.Value);
								}
							}
						}
						else
						{

						}
					}
				}
				sr.Close();
				return dataSet;
			}
			else
			{
				return null;
			}
		}

    }
}
