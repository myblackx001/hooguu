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
using System.Runtime.InteropServices;

namespace AskPriceWin.buy
{
    public partial class AnswerPriceControl : UserControl
    {
        public string username = string.Empty;
        common cmn = new common();
        public AnswerPriceControl()
        {
            InitializeComponent();
            this.listBox1.Hide();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void AnswerPriceControl_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "select * from askpricerecord where status = '" + "询价" + "' and istrue = 0";
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

                        dataGridView1.Columns[8].HeaderText = "业务员";
                        dataGridView1.Columns[9].Visible = false;
                        dataGridView1.Columns[10].Visible = false;
                        dataGridView1.Columns[11].HeaderText = "更新时间";
                        dataGridView1.Columns[12].Visible = false;
                        dataGridView1.Columns[13].Visible = false;
                        dataGridView1.Columns[14].Visible = false;
                        dataGridView1.Columns[15].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("报价初始化："+ex.Message);
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

        private void 报价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                if (this.dataGridView1.CurrentCell.RowIndex != -1)
                {
                    if (this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value != null)
                    {
                        AnswerPriceEdit ape = new AnswerPriceEdit();
                        ape.askpriceID = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.supplier = this.dataGridView1[3, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.model = this.dataGridView1[2, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.price = this.dataGridView1[4, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.num = this.dataGridView1[5, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.type = this.dataGridView1[6, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.remark = this.dataGridView1[13, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.username = username;

                        if (ape.ShowDialog() == DialogResult.Cancel)
                        {
                            init();
                        }
                    }
                }
            }
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            dataGridView1.Width = w - 80;
            dataGridView1.Height = h - button1.Bottom - 80;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string model = textBox1.Text;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "select * from askpricerecord where status = '" +
                                        "询价" + "' and istrue = 0 and model like '%" + model + "%'";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(comm);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = dt;
                        dataGridView1.Columns[0].Width = 80;
                    }
                }
            }
            catch
            {

            }
            
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "update askpricerecord set askstatus = 0";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.ExecuteReader();
                }
            }
        }

        private void AnswerPriceControl_SizeChanged(object sender, EventArgs e)
        {
            int h = this.Height;
            int w = this.Width;
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
                string sql = "select * from askpricerecord where model like '%" + model + "%' and status = '询价' limit 10";
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
                                this.listBox1.Items.Add(ds.Tables[0].Rows[i][2].ToString());
                            }
                        }
                        else
                        {
                            this.listBox1.Hide();
                        }
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


        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                if (this.dataGridView1.CurrentCell.RowIndex != -1)
                {
                    if (this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value != null)
                    {
                        AnswerPriceEdit ape = new AnswerPriceEdit();
                        ape.askpriceID = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.supplier = this.dataGridView1[3, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.model = this.dataGridView1[2, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.price = this.dataGridView1[4, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.num = this.dataGridView1[5, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.type = this.dataGridView1[6, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.remark = this.dataGridView1[13, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        ape.username = username;

                        if (ape.ShowDialog() == DialogResult.Cancel)
                        {
                            init();
                        }
                    }
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.listBox1.Hide();
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string toolTip = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                //toolTip=GetToolTip();//这里写方法设置toolTip
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = toolTip;
            }
        }
    }
}
