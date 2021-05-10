using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AskPriceWin.sales
{
    public partial class SaleOrder : Form
    {
        public string username = string.Empty;
        public string company = string.Empty;
        public SaleOrder()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;//在打印时隐藏按钮
            this.printDialog1.Document = this.printDocument1;
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
            button1.Visible = true;//打印完成回复按钮显示
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string orderid = textBox1.Text;
            string connString = string.Empty;
            if (company == "1")
            {
                connString = "server=101.132.77.197;database =hooguu2020;uid=sk;pwd=Miss123..";
            }
            else if (company == "2")
            {
                connString = "server=101.132.77.197;database =JL2020;uid=sk;pwd=Miss123..";
            }
            //建立连接对象
            SqlConnection Sqlconn = new SqlConnection(connString);
            //打开连接
            Sqlconn.Open();
            //为上面的连接指定Command对象
            SqlCommand thiscommand = Sqlconn.CreateCommand();
            thiscommand.CommandText = "select * from [hooguu2020].[dbo].[View_saleslist] where bnumber = '"+ orderid + "'";
            //为指定的command对象执行DataReader

            SqlDataAdapter da = new SqlDataAdapter(thiscommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //关闭连接
            Sqlconn.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string warehouseID = string.Empty;
            if (checkBox1.Checked)
            {
                warehouseID = "1";
            }
            else if (checkBox2.Checked)
            {
                warehouseID = "2";
            }
            else
            {
                MessageBox.Show("先选择下单库存！");
                return;
            }
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
                    string sql = "insert into stockuprecord (orderid," +
                                                            "supplier," +
                                                            "model," +
                                                            "orderdate," +
                                                            "num," +
                                                            "username," +
                                                            "remark," +
                                                            "stockstatus," +
                                                            "warehouseID," +
                                                            "extraremark" +
                                                            ") values (" +
                                                            "'" +orderid+"'," +
                                                            "'"+supplier+"'," +
                                                            "'"+model+"'," +
                                                            "'"+bdate+"'," +
                                                            ""+ System.Math.Abs(n) + "," +
                                                            "'"+username+"'," +
                                                            "'"+remark+"',1," +
                                                            "'"+ warehouseID + "'," +
                                                            "'"+ extraremark + "')";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        comm.ExecuteReader();
                    }
                }
            }
            this.Close();
        }
    }
}
