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
    public partial class AskPriceControl : UserControl
    {
        string regst = string.Empty;
        common cmn = new common();

        public string username = string.Empty;
        public AskPriceControl()
        {
            InitializeComponent();
            this.dataGridView1.AllowUserToAddRows = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                string model = textBox1.Text;
                conn.Open();
                string sql = "select * from model where Model like '%" + model + "%' limit 10";
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

        private void button1_Click(object sender, EventArgs e)
        {
            string model = textBox1.Text;
            string num = textBox2.Text;
            string type = string.Empty;
            if (checkBox2.Checked && checkBox3.Checked)
                type = "现货|期货";
            else if (checkBox3.Checked)
                type = "期货";
            else if (checkBox2.Checked)
                type = "现货";
            string remark = richTextBox1.Text;
            if (model.Length <= 0 )
            {
                MessageBox.Show("型号和数量不能为空");
                return;
            }
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "insert into askpricerecord (" +
                                            "askpriceID," +
                                            "model," +
                                            "num," +
                                            "status," +
                                            "type," +
                                            "opuser," +
                                            "createTime," +
                                            "remark," +
                                            "askstatus) values ('"+
                                            DateTime.Now.ToString("yyyyMMddHHmmss")+username+"','" +
                                             model+ "','" +
                                            num + "','" +
                                            "询价" + "','" +
                                            type + "','" +
                                            username + "','" +
                                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','【" +
                                            username + "】:\r\n"+remark + " \r\n',1)";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.ExecuteReader();
                }
                textBox1.Text = "";
                textBox2.Text = "";
                richTextBox1.Text = "";
                init();
            }
            //保存型号
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "insert into model (Model) values ('"+ model + "')";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.ExecuteReader();
                }
            }
        }

        private void AskPriceControl_Load(object sender, EventArgs e)
        {
            this.listBox1.Hide();
            init();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItems.Count > 0)
            {
                this.textBox1.Text = this.listBox1.SelectedItem.ToString();
                this.listBox1.Hide();
            }
        }

        private void init()
        {
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = string.Empty;
                if (checkBox1.Checked)
                {
                    sql = "select * from askpricerecord where istrue = 0 and opuser = '" + username + "'";
                }
                else
                {
                    sql = "select * from askpricerecord where istrue = 0";
                }

                sql += " order by updateTime desc";
                using (MySqlCommand comm = new MySqlCommand(sql,conn))
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
                    dataGridView1.Columns[6].HeaderText = "状态";
                    dataGridView1.Columns[7].HeaderText = "货期";
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

        private void AskPriceControl_SizeChanged(object sender, EventArgs e)
        {
            int h = this.Height;
            int w = this.Width;
            dataGridView1.Width = w - 80;
            dataGridView1.Height = h - richTextBox1.Bottom - 80;
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

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AskPriceView apv = new AskPriceView();
            apv.askpriceID = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.model = this.dataGridView1[2, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.price = this.dataGridView1[4, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.num = this.dataGridView1[5, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.type = this.dataGridView1[6, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.remark = this.dataGridView1[13, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            apv.username = username;
            if (apv.ShowDialog() == DialogResult.Cancel)
            {
                init();
            }
        }

        private void 完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                if (this.dataGridView1.CurrentCell.RowIndex != -1)
                {
                    if (this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value != null)
                    {
                        string askpriceID = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                        {
                            conn.Open();
                            string sql = "update askpricerecord set istrue = 1 where askpriceID = '" + askpriceID + "'";
                            using (MySqlCommand comm = new MySqlCommand(sql, conn))
                            {
                                comm.ExecuteReader();
                            }
                            init();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string model = textBox1.Text;
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                
                conn.Open();
                string sql = string.Empty;
                if (checkBox1.Checked)
                {
                    sql = "select * from askpricerecord where istrue = 0 and opuser = '" + username + "' and model like '%" + model + "%'";
                }
                else
                {
                    sql = "select * from askpricerecord where istrue = 0 and model like '%" + model + "%'";
                }
                sql += " order by updateTime desc";
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
                }
            }

            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "update askpricerecord set answerstatus = 0";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    comm.ExecuteReader();
                }
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
                        AskPriceView apv = new AskPriceView();
                        apv.askpriceID = this.dataGridView1[1, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        apv.model = this.dataGridView1[2, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        apv.price = this.dataGridView1[4, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        apv.num = this.dataGridView1[5, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        apv.type = this.dataGridView1[6, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        apv.remark = this.dataGridView1[13, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        apv.username = username;
                        if (apv.ShowDialog() == DialogResult.Cancel)
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

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox1.Text = "批量询价";
                listBox1.Hide();
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                
            }
            else
            {
                textBox1.Text = "";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgrSingle = dataGridView1.Rows[e.RowIndex];
            try
            {
                string str = dgrSingle.Cells[7].Value.ToString();
                if (str == "询价")
                {
                    dgrSingle.DefaultCellStyle.ForeColor = Color.SkyBlue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
