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
    public partial class AnswerPriceQuery : UserControl
    {
        common cmn = new common();
        public string username = string.Empty;
        public AnswerPriceQuery()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void AnswerPriceQuery_Load(object sender, EventArgs e)
        {
            listBox1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string model = textBox1.Text;
                string sql = "select * from askpricerecord where model like '%"+model+"%' || remark like '%"+model+"%' order by updateTime desc";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].HeaderText = "型号";
                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].HeaderText = "价格";
                    dataGridView1.Columns[5].HeaderText = "数量";
                    dataGridView1.Columns[7].HeaderText = "状态";
                    dataGridView1.Columns[6].HeaderText = "货期";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "采购员";
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[11].HeaderText = "更新时间";
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[13].Visible = false;
                    dataGridView1.Columns[14].Visible = false;
                    dataGridView1.Columns[15].Visible = false;
                }
            }
        }

        private void AnswerPriceQuery_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            dataGridView1.Width = w - 80;
            dataGridView1.Height = h - button1.Bottom - 80;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                string model = textBox1.Text;
                conn.Open();
                string sql = "select model from askpricerecord where model like '%" + model + "%' limit 10";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    da.Fill(ds, "model");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            this.listBox1.Visible = true;
                            this.listBox1.Show();
                            this.listBox1.Items.Clear();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                this.listBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                            }
                        }
                    }
                    else
                    {
                        this.listBox1.Hide();

                    }
                }
                if (model.Length <= 0)
                    this.listBox1.Hide();
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

        private void textBox1_Click(object sender, EventArgs e)
        {
            listBox1.Hide();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            sales.AskPriceView apv = new sales.AskPriceView();
            apv.askpriceID = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.model = this.dataGridView1[2, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.price = this.dataGridView1[4, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.num = this.dataGridView1[5, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.type = this.dataGridView1[6, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.remark = this.dataGridView1[13, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.username = username;
            if (apv.ShowDialog() == DialogResult.Cancel)
            {
                
            }
        }
    }
}
