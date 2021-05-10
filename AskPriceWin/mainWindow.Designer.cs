namespace AskPriceWin
{
    partial class mainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.业务端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.询价ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订单列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.采购端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报价ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.库存查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报价查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备货查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仓库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备货ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭其他ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.下单ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.订单列表ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.业务订单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.业务端ToolStripMenuItem,
            this.采购端ToolStripMenuItem,
            this.查询ToolStripMenuItem,
            this.仓库ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 业务端ToolStripMenuItem
            // 
            this.业务端ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.询价ToolStripMenuItem,
            this.下单ToolStripMenuItem,
            this.订单列表ToolStripMenuItem});
            this.业务端ToolStripMenuItem.Name = "业务端ToolStripMenuItem";
            this.业务端ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.业务端ToolStripMenuItem.Text = "业务端";
            // 
            // 询价ToolStripMenuItem
            // 
            this.询价ToolStripMenuItem.Name = "询价ToolStripMenuItem";
            this.询价ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.询价ToolStripMenuItem.Text = "询价";
            this.询价ToolStripMenuItem.Click += new System.EventHandler(this.询价ToolStripMenuItem_Click);
            // 
            // 下单ToolStripMenuItem
            // 
            this.下单ToolStripMenuItem.Name = "下单ToolStripMenuItem";
            this.下单ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.下单ToolStripMenuItem.Text = "下单";
            this.下单ToolStripMenuItem.Click += new System.EventHandler(this.下单ToolStripMenuItem_Click);
            // 
            // 订单列表ToolStripMenuItem
            // 
            this.订单列表ToolStripMenuItem.Name = "订单列表ToolStripMenuItem";
            this.订单列表ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.订单列表ToolStripMenuItem.Text = "订单列表";
            this.订单列表ToolStripMenuItem.Click += new System.EventHandler(this.订单列表ToolStripMenuItem_Click);
            // 
            // 采购端ToolStripMenuItem
            // 
            this.采购端ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.报价ToolStripMenuItem,
            this.下单ToolStripMenuItem1,
            this.订单列表ToolStripMenuItem1,
            this.业务订单ToolStripMenuItem});
            this.采购端ToolStripMenuItem.Name = "采购端ToolStripMenuItem";
            this.采购端ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.采购端ToolStripMenuItem.Text = "采购端";
            // 
            // 报价ToolStripMenuItem
            // 
            this.报价ToolStripMenuItem.Name = "报价ToolStripMenuItem";
            this.报价ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.报价ToolStripMenuItem.Text = "报价";
            this.报价ToolStripMenuItem.Click += new System.EventHandler(this.报价ToolStripMenuItem_Click);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.库存查询ToolStripMenuItem,
            this.报价查询ToolStripMenuItem,
            this.备货查询ToolStripMenuItem});
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.查询ToolStripMenuItem.Text = "查询";
            // 
            // 库存查询ToolStripMenuItem
            // 
            this.库存查询ToolStripMenuItem.Name = "库存查询ToolStripMenuItem";
            this.库存查询ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.库存查询ToolStripMenuItem.Text = "库存查询";
            this.库存查询ToolStripMenuItem.Click += new System.EventHandler(this.库存查询ToolStripMenuItem_Click);
            // 
            // 报价查询ToolStripMenuItem
            // 
            this.报价查询ToolStripMenuItem.Name = "报价查询ToolStripMenuItem";
            this.报价查询ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.报价查询ToolStripMenuItem.Text = "报价查询";
            this.报价查询ToolStripMenuItem.Click += new System.EventHandler(this.报价查询ToolStripMenuItem_Click);
            // 
            // 备货查询ToolStripMenuItem
            // 
            this.备货查询ToolStripMenuItem.Name = "备货查询ToolStripMenuItem";
            this.备货查询ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.备货查询ToolStripMenuItem.Text = "备货查询";
            this.备货查询ToolStripMenuItem.Click += new System.EventHandler(this.备货查询ToolStripMenuItem_Click);
            // 
            // 仓库ToolStripMenuItem
            // 
            this.仓库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.备货ToolStripMenuItem});
            this.仓库ToolStripMenuItem.Name = "仓库ToolStripMenuItem";
            this.仓库ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.仓库ToolStripMenuItem.Text = "仓库";
            // 
            // 备货ToolStripMenuItem
            // 
            this.备货ToolStripMenuItem.Name = "备货ToolStripMenuItem";
            this.备货ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.备货ToolStripMenuItem.Text = "备货";
            this.备货ToolStripMenuItem.Click += new System.EventHandler(this.备货ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 338);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭ToolStripMenuItem,
            this.关闭其他ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // 关闭其他ToolStripMenuItem
            // 
            this.关闭其他ToolStripMenuItem.Name = "关闭其他ToolStripMenuItem";
            this.关闭其他ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭其他ToolStripMenuItem.Text = "关闭其他";
            this.关闭其他ToolStripMenuItem.Click += new System.EventHandler(this.关闭其他ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "a9ozh-crwfh-001.ico");
            this.imageList1.Images.SetKeyName(1, "az5ce-gpr6j-001.ico");
            this.imageList1.Images.SetKeyName(2, "a9ozh-crwfh-001.ico");
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // 下单ToolStripMenuItem1
            // 
            this.下单ToolStripMenuItem1.Name = "下单ToolStripMenuItem1";
            this.下单ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.下单ToolStripMenuItem1.Text = "下单";
            this.下单ToolStripMenuItem1.Click += new System.EventHandler(this.下单ToolStripMenuItem1_Click);
            // 
            // 订单列表ToolStripMenuItem1
            // 
            this.订单列表ToolStripMenuItem1.Name = "订单列表ToolStripMenuItem1";
            this.订单列表ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.订单列表ToolStripMenuItem1.Text = "订单列表";
            this.订单列表ToolStripMenuItem1.Click += new System.EventHandler(this.订单列表ToolStripMenuItem1_Click);
            // 
            // 业务订单ToolStripMenuItem
            // 
            this.业务订单ToolStripMenuItem.Name = "业务订单ToolStripMenuItem";
            this.业务订单ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.业务订单ToolStripMenuItem.Text = "业务订单";
            this.业务订单ToolStripMenuItem.Click += new System.EventHandler(this.业务订单ToolStripMenuItem_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainWindow";
            this.Text = "汇谷内部管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainWindow_FormClosing);
            this.Load += new System.EventHandler(this.mainWindow_Load);
            this.SizeChanged += new System.EventHandler(this.mainWindow_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 业务端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 询价ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭其他ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 采购端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报价ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 库存查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报价查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仓库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备货ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订单列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备货查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下单ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 订单列表ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 业务订单ToolStripMenuItem;
    }
}