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

namespace AskPriceWin.query
{
    public partial class StockUpQuery : UserControl
    {
        public string username = string.Empty;
        common cmn = new common();
        public StockUpQuery()
        {
            InitializeComponent();
            this.dataGridView1.AllowUserToAddRows = false;
        }

        private void StockUpQuery_Load(object sender, EventArgs e)
        {
            init("");
        }

        private void StockUpQuery_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            dataGridView1.Width = w - 80;
            dataGridView1.Height = h - button1.Bottom - 80;
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
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                orderid = textBox1.Text;
                string sql = "select * from stockuprecord where orderid like '%" + orderid + "%' ";
                string str = string.Empty;

                if (checkBox1.Checked && !checkBox2.Checked)
                {
                    str = " and warehouseID = 1";
                    sql += str;
                }
                else if (!checkBox1.Checked && checkBox2.Checked)
                {
                    str = " and warehouseID = 2";
                    sql += str;
                }
                str = " order by updateTime desc";
                sql += str;

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

        private void button1_Click(object sender, EventArgs e)
        {
            string orderid = textBox1.Text;

            init(orderid);
        }
    }
}
