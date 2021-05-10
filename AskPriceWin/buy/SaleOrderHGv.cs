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
using System.Data.SqlClient;

namespace AskPriceWin.buy
{
    public partial class SaleOrderHGv : UserControl
    {
        public string company = string.Empty;
        public string username = string.Empty;
        common cmn = new common();
        public SaleOrderHGv()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string uname = comboBox1.Text;
            if (uname.Length <= 0)
            {
                MessageBox.Show("业务员不能为空！");
                return;
            }
            string oldorderid = textBox2.Text;
            if (oldorderid.Length <= 0)
            {
                MessageBox.Show("原始订单号不能为空！");
                return;
            }
            string warehouseID = string.Empty;
            if (checkBox1.Checked)
            {
                warehouseID = "1";
            }
            else if (checkBox2.Checked)
            {
                warehouseID = "2";
            }
            else if (checkBox4.Checked)
            {
                warehouseID = "9";
            }
            else
            {
                MessageBox.Show("先选择下单库存！");
                return;
            }
            //送市场
            int delivery = 0;
            if (checkBox3.Checked)
            {
                delivery = 1;
            }
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                string buyer = "";
                if (this.dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    buyer = this.dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
                string newmodel = "";
                if (this.dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    newmodel = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                }
                string supplier = this.dataGridView1.Rows[i].Cells[3].Value.ToString();
                string model = this.dataGridView1.Rows[i].Cells[4].Value.ToString();
                string orderid = this.dataGridView1.Rows[i].Cells[5].Value.ToString();
                string remark = this.dataGridView1.Rows[i].Cells[6].Value.ToString();
                string bdate = this.dataGridView1.Rows[i].Cells[7].Value.ToString();
                string num = this.dataGridView1.Rows[i].Cells[8].Value.ToString();
                string extraremark = this.richTextBox1.Text;
                float n = float.Parse(num);

                common cmn = new common();
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "insert into stockuprecord (orderid," +
                                                            "supplier," +
                                                            "model," +
                                                            "orderdate," +
                                                            "num," +
                                                            "username," +
                                                            "remark," +
                                                            "stockstatus," +
                                                            "warehouseID," +
                                                            "extraremark," +
                                                            "buyer," +
                                                            "delivery," +
                                                            "newmodel," +
                                                            "oldorderid," +
                                                            "buyname) values (" +
                                                            "'" + orderid + "_" + company + "'," +
                                                            "'" + supplier + "'," +
                                                            "'" + model + "'," +
                                                            "'" + bdate + "'," +
                                                            "" + System.Math.Abs(n) + "," +
                                                            "'" + uname + "'," +
                                                            "'" + remark + "',1," +
                                                            "'" + warehouseID + "'," +
                                                            "'" + extraremark + "'," +
                                                            "'" + buyer + "'," +
                                                            "" + delivery + "," +
                                                            "'" + newmodel + "'," +
                                                            "'"+oldorderid+"'," +
                                                            "'"+username+"')";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        comm.ExecuteReader();

                    }
                }

            }
            DataTable dt = new DataTable();
            dataGridView1.DataSource = dt;
            textBox1.Text = "";
            MessageBox.Show("下单完成！");
        }

        private void SaleOrderHGv_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "select account from user where power = 2 and company = 2 and depart = '业务员'";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataReader myread = comm.ExecuteReader();
                    while (myread.Read())
                    {
                        string account = myread["account"].ToString();
                        comboBox1.Items.Add(account);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string orderid = textBox1.Text;
            string connString = string.Empty;

            connString = "server=101.132.77.197;database =hooguu2020;uid=sk;pwd=Miss123..";
            //建立连接对象
            SqlConnection Sqlconn = new SqlConnection(connString);
            //打开连接
            Sqlconn.Open();
            //为上面的连接指定Command对象
            SqlCommand thiscommand = Sqlconn.CreateCommand();
            thiscommand.CommandText = "select * from [hooguu2020].[dbo].[View_saleslist] where bnumber = '" + orderid + "'";
            //为指定的command对象执行DataReader

            SqlDataAdapter da = new SqlDataAdapter(thiscommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "销售方";
            dataGridView1.Columns[4].HeaderText = "型号";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].HeaderText = "订单号";
            dataGridView1.Columns[6].HeaderText = "摘要";
            dataGridView1.Columns[6].Width = 300;
            dataGridView1.Columns[7].HeaderText = "下单日期";
            dataGridView1.Columns[8].HeaderText = "数量";
            //关闭连接
            Sqlconn.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void SaleOrderHGv_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            dataGridView1.Width = w - 80;
        }
    }
}
