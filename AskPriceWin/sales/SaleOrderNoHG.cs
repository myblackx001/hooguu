using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace AskPriceWin.sales
{
    public partial class SaleOrderNoHG : UserControl
    {
        public string company = string.Empty;
        public string username = string.Empty;
        public SaleOrderNoHG()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void SaleOrderNoHG_Load(object sender, EventArgs e)
        {

        }

        private void SaleOrderNoHG_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            this.dataGridView1.Width = w - 80;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string orderid = textBox1.Text;
            string connString = string.Empty;
            connString = "server=101.132.77.197;database =JL2020;uid=sk;pwd=Miss123..";
            //建立连接对象
            SqlConnection Sqlconn = new SqlConnection(connString);
            //打开连接
            Sqlconn.Open();
            //为上面的连接指定Command对象
            SqlCommand thiscommand = Sqlconn.CreateCommand();
            thiscommand.CommandText = "select * from [JL2020].[dbo].[View_saleslist] where bnumber = '" + orderid + "'";
            //为指定的command对象执行DataReader

            SqlDataAdapter da = new SqlDataAdapter(thiscommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "销售方";
            dataGridView1.Columns[2].HeaderText = "型号";
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].HeaderText = "订单号";
            dataGridView1.Columns[4].HeaderText = "摘要";
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[5].HeaderText = "下单日期";
            dataGridView1.Columns[6].HeaderText = "数量";
            //关闭连接
            Sqlconn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                string supplier = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                string model = this.dataGridView1.Rows[i].Cells[2].Value.ToString();
                string orderid = this.dataGridView1.Rows[i].Cells[3].Value.ToString();
                string remark = this.dataGridView1.Rows[i].Cells[4].Value.ToString();
                string bdate = this.dataGridView1.Rows[i].Cells[5].Value.ToString();
                string num = this.dataGridView1.Rows[i].Cells[6].Value.ToString();
                string extraremark = this.richTextBox1.Text;
                float n = float.Parse(num);

                common cmn = new common();
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "insert into extraorderlist (orderid," +
                                                            "supplier," +
                                                            "model," +
                                                            "orderdate," +
                                                            "num," +
                                                            "username," +
                                                            "remark," +
                                                            "warehouse," +
                                                            "tips) values (" +
                                                            "'" + orderid + "_" + company + "'," +
                                                            "'" + supplier + "'," +
                                                            "'" + model + "'," +
                                                            "'" + bdate + "'," +
                                                            "" + System.Math.Abs(n) + "," +
                                                            "'" + username + "'," +
                                                            "'" + remark + "'," +
                                                            "' '," +
                                                            "1)";
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
    }
}
