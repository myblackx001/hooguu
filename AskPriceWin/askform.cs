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

namespace AskPriceWin
{
    public partial class askform : Form
    {
        string strcon = "server = 101.132.77.197; " +
                        "user id = wg; password = Miss123; " +
                        "database=shukong;port=3306;Charset=utf8";
        public string username = string.Empty;
        public askform()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.dataGridView1.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string model = textBox1.Text;
            string num = textBox2.Text;
            string type = string.Empty;
            if (radioButton1.Checked)
                type = "期货";
            else if (radioButton2.Checked)
                type = "现货";
            string remark = textBox3.Text;
            if (model.Length <= 0 ||
                num.Length <= 0)
            {
                MessageBox.Show("型号和数量不能为空");
                return;
            }
            using (MySqlConnection conn = new MySqlConnection(strcon)) 
            {
                conn.Open();
                string sql = "insert into askpricerecord (" +
                                            "model," +
                                            "num," +
                                            "status," +
                                            "type," +
                                            "opuser," +
                                            "createTime," +
                                            "remark) values ('"+
                                            model + "','"+
                                            num+"','"+
                                            "询价"+"','"+
                                            type+"','"+
                                            username+"','"+
                                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"','"+
                                            remark + "')";
                using (MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.ExecuteReader();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listBox1.Hide();
        }


        private void askform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void querystock()
        {
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strcon))
            {
                string model = textBox1.Text;
                conn.Open();
                string sql = "select * from ware where name like '%"+model+"%'";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    da.Fill(ds,"ware");

                    //ds.Tables["ware"].Columns["ID"].ColumnName = "序号";
                    
                    dataGridView1.DataSource = ds.Tables["ware"];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            querystock();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strcon))
            {
                string model = textBox1.Text;
                conn.Open();
                string sql = "select * from ware where name like '%" + model + "%' limit 8";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    da.Fill(ds, "ware");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            this.listBox1.Visible = true;
                            this.listBox1.Show();
                            this.listBox1.Items.Clear();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                this.listBox1.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                            }
                        }
                    }
                }
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItems.Count > 0)
            {
                this.textBox1.Text = this.listBox1.SelectedItem.ToString();
                this.listBox1.Hide();
            }
        }

        private void askform_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            dataGridView1.Width = w - 50;
            dataGridView1.Height = h - button1.Bottom - 50;
        }
    }
}
