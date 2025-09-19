namespace ElectCell_HMI
{
    partial class MainWindow
    {

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开案例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.保存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.另存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.求解计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载仿真结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载仿真结果ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem1,
            this.求解计算ToolStripMenuItem,
            this.加载仿真结果ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1211, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem1
            // 
            this.文件ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开案例ToolStripMenuItem,
            this.toolStripSeparator1,
            this.保存ToolStripMenuItem1,
            this.另存ToolStripMenuItem1});
            this.文件ToolStripMenuItem1.Name = "文件ToolStripMenuItem1";
            this.文件ToolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem1.Text = "文件";
            // 
            // 打开案例ToolStripMenuItem
            // 
            this.打开案例ToolStripMenuItem.Name = "打开案例ToolStripMenuItem";
            this.打开案例ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.打开案例ToolStripMenuItem.Text = "打开案例";
            this.打开案例ToolStripMenuItem.Click += new System.EventHandler(this.打开案例ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // 保存ToolStripMenuItem1
            // 
            this.保存ToolStripMenuItem1.Name = "保存ToolStripMenuItem1";
            this.保存ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.保存ToolStripMenuItem1.Text = "保存配置参数";
            this.保存ToolStripMenuItem1.Click += new System.EventHandler(this.保存ToolStripMenuItem1_Click);
            // 
            // 另存ToolStripMenuItem1
            // 
            this.另存ToolStripMenuItem1.Name = "另存ToolStripMenuItem1";
            this.另存ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.另存ToolStripMenuItem1.Text = "另存配置参数";
            this.另存ToolStripMenuItem1.Click += new System.EventHandler(this.另存ToolStripMenuItem1_Click);
            // 
            // 求解计算ToolStripMenuItem
            // 
            this.求解计算ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始计算ToolStripMenuItem,
            this.停止计算ToolStripMenuItem});
            this.求解计算ToolStripMenuItem.Name = "求解计算ToolStripMenuItem";
            this.求解计算ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.求解计算ToolStripMenuItem.Text = "仿真计算";
            // 
            // 开始计算ToolStripMenuItem
            // 
            this.开始计算ToolStripMenuItem.Name = "开始计算ToolStripMenuItem";
            this.开始计算ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.开始计算ToolStripMenuItem.Text = "开始计算";
            this.开始计算ToolStripMenuItem.Click += new System.EventHandler(this.开始计算ToolStripMenuItem_Click);
            // 
            // 停止计算ToolStripMenuItem
            // 
            this.停止计算ToolStripMenuItem.Name = "停止计算ToolStripMenuItem";
            this.停止计算ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.停止计算ToolStripMenuItem.Text = "停止计算";
            this.停止计算ToolStripMenuItem.Click += new System.EventHandler(this.停止计算ToolStripMenuItem_Click);
            // 
            // 加载仿真结果ToolStripMenuItem
            // 
            this.加载仿真结果ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载仿真结果ToolStripMenuItem1,
            this.toolStripSeparator3,
            this.bToolStripMenuItem});
            this.加载仿真结果ToolStripMenuItem.Name = "加载仿真结果ToolStripMenuItem";
            this.加载仿真结果ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.加载仿真结果ToolStripMenuItem.Text = "仿真结果";
            this.加载仿真结果ToolStripMenuItem.Click += new System.EventHandler(this.加载仿真结果ToolStripMenuItem_Click);
            // 
            // 加载仿真结果ToolStripMenuItem1
            // 
            this.加载仿真结果ToolStripMenuItem1.Name = "加载仿真结果ToolStripMenuItem1";
            this.加载仿真结果ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.加载仿真结果ToolStripMenuItem1.Text = "加载仿真结果";
            this.加载仿真结果ToolStripMenuItem1.Click += new System.EventHandler(this.加载仿真结果ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // bToolStripMenuItem
            // 
            this.bToolStripMenuItem.Name = "bToolStripMenuItem";
            this.bToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bToolStripMenuItem.Text = "导出仿真结果";
            this.bToolStripMenuItem.Click += new System.EventHandler(this.bToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 684);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1211, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1211, 659);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(214, 653);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 706);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电解水制氢仿真系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.ToolStripMenuItem 求解计算ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        public string path;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.ToolStripMenuItem 开始计算ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 停止计算ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem 打开案例ToolStripMenuItem;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem 另存ToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem 加载仿真结果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载仿真结果ToolStripMenuItem1;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

