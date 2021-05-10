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

namespace AskPriceWin.sales
{
    public partial class OrderList : UserControl
    {
        common cmn = new common();
        public string username = string.Empty;
        public string company = string.Empty;
        public OrderList()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            
        }

        private void OrderList_Load(object sender, EventArgs e)
        {
            init("");
        }

        private void init(string orderid)
        {
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "select oldorderid,orderid,newmodel,model,remark,extraremark,istrue,orderdate from stockuprecord " +
                                                "where istrue <> 9 " +
                                                "and orderid like '%" + orderid + "%' ";
                string str = string.Empty;
                if (company == "2")
                {
                    str = " and username = '"+username+"'";
                    sql += str;
                }
                
                str = "group by orderid order by updatetime desc";
                sql += str;
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    if (company == "2")
                    {
                        dataGridView1.Columns[0].HeaderText = "订单号";
                        dataGridView1.Columns[0].Width = 200;
                        dataGridView1.Columns[1].Visible = false;
                        dataGridView1.Columns[2].Visible = false;
                        dataGridView1.Columns[3].HeaderText = "型号";
                        dataGridView1.Columns[3].Width = 200;
                        dataGridView1.Columns[4].HeaderText = "备注";
                        dataGridView1.Columns[4].Width = 300;
                        dataGridView1.Columns[5].HeaderText = "额外备注";
                        dataGridView1.Columns[5].Width = 300;
                        dataGridView1.Columns[6].Visible = false;
                        dataGridView1.Columns[7].HeaderText = "订单时间";
                        dataGridView1.Columns[7].Width = 200;
                    }
                    else
                    {
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].HeaderText = "订单号";
                        dataGridView1.Columns[1].Width = 200;
                        dataGridView1.Columns[2].Visible = false;
                        dataGridView1.Columns[3].HeaderText = "型号";
                        dataGridView1.Columns[3].Width = 200;
                        dataGridView1.Columns[4].HeaderText = "备注";
                        dataGridView1.Columns[4].Width = 300;
                        dataGridView1.Columns[5].HeaderText = "额外备注";
                        dataGridView1.Columns[5].Width = 300;
                        dataGridView1.Columns[6].Visible = false;
                        dataGridView1.Columns[7].HeaderText = "订单时间";
                        dataGridView1.Columns[7].Width = 200;
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string orderid = textBox1.Text;
            init(orderid);
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgrSingle = dataGridView1.Rows[e.RowIndex];
            try
            {
                string str = dgrSingle.Cells[6].Value.ToString();
                if (str == "1")
                {
                    dgrSingle.DefaultCellStyle.ForeColor = Color.OrangeRed;
                }
                else if (str == "2")
                {
                    dgrSingle.DefaultCellStyle.ForeColor = Color.BlueViolet;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 结束ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string orderid = this.dataGridView1[0, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "update stockuprecord set istrue = 9,orderstatus = 1,stockname = '" + username + "' where orderid = '" + orderid + "'";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.ExecuteReader();
                }
            }
            init("");
        }

        private void OrderList_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            dataGridView1.Width = w - 80;
            dataGridView1.Height = h - 80 - button1.Bottom;
        }
    }
}
