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

namespace AskPriceWin.buy
{
    public partial class SaleOrderHG : UserControl
    {
        common cmn = new common();
        public string username = string.Empty;
        public string company = string.Empty;
        public SaleOrderHG()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void SaleOrderHG_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            this.dataGridView1.Width = w - 80;
            this.dataGridView1.Height = h - 80 - button2.Bottom;
        }

        private void init(string orderid)
        {
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "select model,orderid,supplier,orderdate,num,username,remark,isorder,warehouse" +
                                                "  from extraorderlist where orderid like '%" +
                                                orderid + "%' group by orderid order by updatetime desc";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "型号";
                    dataGridView1.Columns[0].Width = 200;
                    dataGridView1.Columns[1].HeaderText = "订单号";
                    dataGridView1.Columns[1].Width = 200;
                    dataGridView1.Columns[2].HeaderText = "客户";
                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[3].HeaderText = "订单日期";
                    dataGridView1.Columns[3].Width = 200;
                    dataGridView1.Columns[4].HeaderText = "数量";
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].HeaderText = "业务员";
                    dataGridView1.Columns[5].Width = 200;
                    dataGridView1.Columns[6].HeaderText = "备注";
                    dataGridView1.Columns[6].Width = 400;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[8].HeaderText = "仓库";
                    dataGridView1.Columns[8].Width = 300;
                }
            }
        }

        private void cleartips()
        {
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "update extraorderlist set tips = 0 where tips = 1";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.ExecuteReader();
                }
            }
        }

        private void SaleOrderHG_Load(object sender, EventArgs e)
        {
            init("");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string orderid = textBox1.Text.Trim();
            cleartips();
            init(orderid);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                if (this.dataGridView1.CurrentCell.RowIndex != -1)
                {
                    if (this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value != null)
                    {
                        SaleOrderHGdlg sohg = new SaleOrderHGdlg();
                        sohg.oldorderid = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        sohg.username = this.dataGridView1[5, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        sohg.company = company;
                        sohg.remark = this.dataGridView1[6, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        if (sohg.ShowDialog() == DialogResult.Cancel)
                        {
                            init("");
                        }
                    }
                }
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgrSingle = dataGridView1.Rows[e.RowIndex];
            try
            {
                string str = dgrSingle.Cells[7].Value.ToString();
                if (str == "1")
                {
                    dgrSingle.DefaultCellStyle.ForeColor = Color.SkyBlue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
