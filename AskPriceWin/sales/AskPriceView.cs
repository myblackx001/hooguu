using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AskPriceWin.sales
{
    public partial class AskPriceView : Form
    {
        public string username = string.Empty;
        public string askpriceID = string.Empty;
        public string model = string.Empty;
        public string price = string.Empty;
        public string num = string.Empty;
        public string remark = string.Empty;
        public string type = string.Empty;
        public AskPriceView()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AskPriceView_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = model;
            this.textBox2.Text = price;
            this.textBox3.Text = num;
            this.richTextBox2.Text = remark;
            this.textBox4.Text = type;
            //获取图片列表
            loadpic();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            common cmn = new common();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "update askpricerecord set askstatus = 1," +
                                                        "status = '询价'," +
                                                        "price = '"+price+"'," +
                                                        "num = '"+num+"'," +
                                                        "remark = concat(remark,'\r\n 【"+username+"】:"+ 
                                                        richTextBox1.Text+" 价格："+
                                                        price+ "数量："+num +"\r\n ') " +
                                                        "where askpriceID = '"+askpriceID+"'";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.ExecuteReader();
                }
            }
            this.Close();
        }

        private void loadpic()
        {
            common cmn = new common();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "select ID from askpricepic where askpriceID = '"+askpriceID+"'";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataReader myread =  comm.ExecuteReader();
                    while (myread.Read())
                    {
                        string ID = myread["ID"].ToString();
                        listBox1.Items.Add(ID);
                    }
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string ID = this.listBox1.SelectedItem.ToString();
            AskPricePicView appv = new AskPricePicView();
            appv.ID = ID;
            appv.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaleOrder so = new SaleOrder();
            so.username = username;
            so.Show();
        }

        private void AskPriceView_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            richTextBox1.Height = h - 80 - button1.Bottom;
            richTextBox2.Height = h- 80 - button1.Bottom;
        }
    }
}
