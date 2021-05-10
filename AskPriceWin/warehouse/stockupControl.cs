using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AskPriceWin.warehouse
{
    public partial class stockupControl : UserControl
    {
        public string username = string.Empty;
        public string classify = string.Empty;
        common cmn = new common();
        public stockupControl()
        {
            InitializeComponent();
            this.dataGridView1.AllowUserToAddRows = false;
        }

        private void stockupControl_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            dataGridView1.Width = w - 80;
            dataGridView1.Height = h - 80;

        }

        private void stockupControl_Load(object sender, EventArgs e)
        {
            init("");
            timer1.Enabled = true;
        }
        private delegate void myDelegate(DataTable dt);//定义委托
        private void Grid(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new myDelegate(Grid), new object[] { dt });
            }
            else
            {
                try
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.DataSource = dt;
                    dt = null;
                }
                catch
                {

                }
            }

        }
        private void init(string orderid)
        {
            string sql = "select * from stockuprecord where istrue <> 2 and orderid like '%" + orderid + "%' and " +
                              "warehouseID = " + classify +
                              " group by orderid";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();

                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(comm);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Grid(dt);
                        //dataGridView1.DataSource = dt;
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].HeaderText = "订单号";
                        dataGridView1.Columns[1].Width = 260;
                        dataGridView1.Columns[2].HeaderText = "客户";
                        dataGridView1.Columns[3].Visible = false;
                        dataGridView1.Columns[4].HeaderText = "日期";
                        dataGridView1.Columns[5].Visible = false;
                        dataGridView1.Columns[6].Visible = false;
                        dataGridView1.Columns[7].Width = 600;
                        dataGridView1.Columns[7].HeaderText = "备注";
                        dataGridView1.Columns[8].Visible = false;
                        dataGridView1.Columns[9].Visible = false;
                        dataGridView1.Columns[10].Visible = false;
                        dataGridView1.Columns[11].Visible = false;
                        dataGridView1.Columns[12].Visible = false;
                        dataGridView1.Columns[13].HeaderText = "额外备注";
                        dataGridView1.Columns[13].Width = 600;
                        dataGridView1.Columns[14].Visible = false;
                        dataGridView1.Columns[15].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("备货初始化："+ sql+ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string orderid = textBox1.Text;
            init(orderid);

            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "update stockuprecord set stockstatus = 0";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.ExecuteReader();
                }
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dataGridView1.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void 备货中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                if (this.dataGridView1.CurrentCell.RowIndex != -1)
                {
                    if (this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value != null)
                    {
                        string orderid = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                        {
                            conn.Open();
                            string sql = "update stockuprecord set istrue = 1,orderstatus = 1,stockname = '" + username + "' where orderid = '" + orderid + "'";
                            using (MySqlCommand comm = new MySqlCommand(sql, conn))
                            {
                                comm.ExecuteReader();
                            }
                        }
                        init("");
                    }
                }
            }
            
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                if (this.dataGridView1.CurrentCell.RowIndex != -1)
                {
                    if (this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value != null)
                    {
                        string orderid = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        StockUpView suv = new StockUpView();
                        suv.orderid = orderid;
                        suv.username = username;
                        if (suv.ShowDialog() == DialogResult.Cancel)
                        {
                            init("");
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            init("");
        }

        private void 备货完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                if (this.dataGridView1.CurrentCell.RowIndex != -1)
                {
                    if (this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value != null)
                    {
                        string orderid = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                        {
                            conn.Open();
                            string sql = "update stockuprecord set istrue = 2,orderstatus = 1,stockname = '" + username + "' where orderid = '" + orderid + "'";
                            using (MySqlCommand comm = new MySqlCommand(sql, conn))
                            {
                                comm.ExecuteReader();
                            }
                        }
                        init("");
                    }
                }
            }

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgrSingle = dataGridView1.Rows[e.RowIndex];
            try
            {
                string str = dgrSingle.Cells[9].Value.ToString();
                if (str == "1")
                {
                    dgrSingle.DefaultCellStyle.ForeColor = Color.OrangeRed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
