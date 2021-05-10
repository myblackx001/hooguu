using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AskPriceWin.sales;
using MySql.Data.MySqlClient;

namespace AskPriceWin
{
    public partial class mainWindow : Form
    {
        public string username = string.Empty;
        public string depart = string.Empty;
        public string classify = string.Empty;
        public string company = string.Empty;
        public mainWindow()
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            //设置icon显示的图片
            this.setIconImg(0);//加载窗体时托盘显示的图标是下标为0的那张图片
            timer2.Enabled = true;
            timer1.Enabled = true;
        }
        //切换图片的标识
        private bool iconFlag = false;//加载图标切换状态是停止状态

        //系统是否运行
        private bool isRun = false;//加载状态运行是停止

        //设置托盘的图标可以从Image对象转换为Icon对象
        private void setIconImg(int index)
        {
            try
            {
                Image img = this.imageList1.Images[index];
                if (img != null)
                {
                    Bitmap b = new Bitmap(img);
                    if (b != null)
                    {
                        Icon icon = Icon.FromHandle(b.GetHicon());
                        if (icon != null)
                        {
                            this.notifyIcon1.Icon = icon;
                            //Icon.Dispose();
                            b.Dispose();
                            img.Dispose();
                        }
                        
                    }

                }
                
            }
            catch 
            {

            }

        }

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
            catch (Exception)
            {

            }
        }

        private void 关闭其他ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage objtabpage = tabControl1.TabPages[tabControl1.SelectedIndex];
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(objtabpage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            //点击右键弹出操作菜单
            if (e.Button == MouseButtons.Right)
            {
                //弹出操作菜单
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            //清理选项卡
            tabControl1.TabPages.Clear();
            //修改标题
            this.Text = "汇谷内部管理系统                      欢迎回来 " + username + " ~-~";
            //根据权限隐藏菜单
            if (depart == "业务员")
            {
                采购端ToolStripMenuItem.Visible = false;
                
                仓库ToolStripMenuItem.Visible = false;
            }
            else if (depart == "采购员")
            {
                业务端ToolStripMenuItem.Visible = false;
                if (classify == "0")
                仓库ToolStripMenuItem.Visible = false;
            }
            else if (depart == "仓库员")
            {
                业务端ToolStripMenuItem.Visible = false;
                采购端ToolStripMenuItem.Visible = false;
            }

        }

        private void addTabControl(string MainTabControlKey, string MainTabControlName, TabControl objTabControl, UserControl objfrm)
        {
            try
            {
                //避免重复选项卡
                //if (objTabControl.TabPages.ContainsKey(MainTabControlKey.ToString().Trim()) == false)
                {
                    //声明一个选项卡对象
                    TabPage tabPage = new TabPage();
                    tabPage.ImageIndex = 1;
                    //选项卡的名称
                    tabPage.Name = MainTabControlKey;
                    //选项卡的文本
                    tabPage.Text = MainTabControlName;
                    //向选项卡集合添加新选项卡
                    objTabControl.Controls.Add(tabPage);
                    objfrm.Dock = DockStyle.Fill;
                    //子窗体显示
                    objfrm.Show();
                    //子窗体大小设置为选项卡大小
                    objfrm.Size = tabPage.Size;
                    //将子窗体添加到选项卡中
                    tabPage.Controls.Add(objfrm);
                    //设置当前选项卡为新增选项卡
                    objTabControl.SelectTab(MainTabControlKey);
                }
                //else
                //{
                //    //设为当前选中的选项
                //    objTabControl.SelectTab(MainTabControlKey);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加选项卡时出错，请检查是否正确连接数据" + ex.Message.ToString());
            }
        }

        private void mainWindow_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            tabControl1.Height = h - 80;
            tabControl1.Width = w - 40;
        }

        private void 询价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sales.AskPriceControl apc = new sales.AskPriceControl();
            apc.username = username;
            addTabControl("AskPriceControl", "询价", tabControl1, apc);
        }

        private void 报价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buy.AnswerPriceControl apc = new buy.AnswerPriceControl();
            apc.username = username;
            addTabControl("AnswerPriceControl", "报价", tabControl1, apc);
        }

        //客户端查询订单每隔5秒
        private void timer1_Tick(object sender, EventArgs e)
        {
            common cmn = new common();

            if (depart == "采购员")
            {
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "select askstatus from askpricerecord where askstatus = 1";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        MySqlDataReader myread = comm.ExecuteReader();
                        if (myread.Read())
                        {
                            this.isRun = true;//开始闪烁
                        }
                        else
                        {
                            this.isRun = false;
                        }
                    }
                }

                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "select tips from extraorderlist where tips = 1";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        MySqlDataReader myread = comm.ExecuteReader();
                        if (myread.Read())
                        {
                            this.isRun = true;//开始闪烁
                        }
                        else
                        {
                            this.isRun = false;
                        }
                    }
                }
            }
            else if (depart == "业务员")
            {
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "select answerstatus from askpricerecord where answerstatus = 1 and opuser = '"+username+"'";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        MySqlDataReader myread = comm.ExecuteReader();
                        if (myread.Read())
                        {
                            this.isRun = true;//开始闪烁
                        }
                        else
                        {
                            this.isRun = false;
                        }
                    }
                }
            }
            else if (depart == "业务员")
            {
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "select orderstatus from stockuprecord where orderstatus = 1 and username = '" + username + "'";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        MySqlDataReader myread = comm.ExecuteReader();
                        if (myread.Read())
                        {
                            this.isRun = true;//开始闪烁
                        }
                        else
                        {
                            this.isRun = false;
                        }
                    }
                }
            }
            else if (depart == "仓库员" || classify.Length >0 )
            {
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "select stockstatus from stockuprecord where stockstatus = 1";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        MySqlDataReader myread = comm.ExecuteReader();
                        if (myread.Read())
                        {
                            this.isRun = true;//开始闪烁
                        }
                        else
                        {
                            this.isRun = false;
                        }
                    }
                }
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //显示主窗体
            this.Visible = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //关闭闪烁
            this.isRun = false;
            this.Show();//主窗体显示

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!this.isRun)
            {
                return;
            }
            if (iconFlag)
            {
                FlashWin();
                this.setIconImg(1);
                iconFlag = !iconFlag;
             }
            else
            {
                this.setIconImg(2);
                iconFlag = !iconFlag;
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Unicode, EntryPoint = "FlashWindowEx")]
        private static extern void FlashWindowEx(ref FLASHWINFO pwfi);
        public struct FLASHWINFO
        {
            public UInt32 cbSize;//该结构的字节大小
            public IntPtr hwnd;//要闪烁的窗口的句柄，该窗口可以是打开的或最小化的
            public UInt32 dwFlags;//闪烁的状态
            public UInt32 uCount;//闪烁窗口的次数
            public UInt32 dwTimeout;//窗口闪烁的频度，毫秒为单位；若该值为0，则为默认图标的闪烁频度
        }
        public const UInt32 FLASHW_TRAY = 2;
        public const UInt32 FLASHW_TIMERNOFG = 12;
        private void FlashWin()
        {
            FLASHWINFO fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = this.Handle;
            fInfo.dwFlags = FLASHW_TRAY | FLASHW_TIMERNOFG;
            fInfo.uCount = 3;// UInt32.MaxValue;
            fInfo.dwTimeout = 500;
            FlashWindowEx(ref fInfo);
        }

        private void 库存查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 报价查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            query.AnswerPriceQuery apq = new query.AnswerPriceQuery();
            apq.username = username;
            addTabControl("AnswerPriceQuery", "报价查询", tabControl1, apq);
        }


        private void 备货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            warehouse.stockupControl sc = new warehouse.stockupControl();
            sc.username = username;
            sc.classify = classify;
            addTabControl("stockupControl", "备货", tabControl1, sc);
        }

        private void 订单列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sales.OrderList ol = new OrderList();
            ol.username = username;
            ol.company = company;
            addTabControl("OrderList", "订单列表", tabControl1, ol);
        }

        private void 下单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (company == "1")
            {
                sales.SaleOrderUC so = new sales.SaleOrderUC();
                so.username = username;
                so.company = company;
                addTabControl("SaleOrderUC", "下单", tabControl1, so);
            }
            else if (company == "2")
            {
                sales.SaleOrderNoHG son = new SaleOrderNoHG();
                son.username = username;
                son.company = company;
                addTabControl("SaleOrderNoHG", "下单", tabControl1, son);
            }
        }

        private void 备货查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            query.StockUpQuery suq = new query.StockUpQuery();
            suq.username = username;
            addTabControl("StockUpQuery", "备货查询", tabControl1, suq);
        }

        private void 下单ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            buy.SaleOrderHGv sohgv = new buy.SaleOrderHGv();

            addTabControl("SaleOrderHG", "下单", tabControl1, sohgv);
        }

        private void 订单列表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sales.OrderList ol = new OrderList();
            ol.username = username;
            addTabControl("OrderList", "订单列表", tabControl1, ol);
        }

        private void 业务订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buy.SaleOrderHG sohg = new buy.SaleOrderHG();
            sohg.username = username;
            sohg.company = company;
            addTabControl("SaleOrderHG", "业务订单", tabControl1, sohg);
        }
    }
}
