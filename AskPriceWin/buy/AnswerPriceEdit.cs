using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AskPriceWin.buy
{
    public partial class AnswerPriceEdit : Form
    {
        public string model = string.Empty;
        public string price = string.Empty;
        public string num = string.Empty;
        public string remark = string.Empty;
        public string username = string.Empty;
        public string askpriceID = string.Empty;
        public string supplier = string.Empty;
        public string type = string.Empty;

        public AnswerPriceEdit()
        {
            InitializeComponent();
            this.listBox1.Hide();
            this.WindowState = FormWindowState.Maximized;
        }

        private void AnswerPriceEdit_Load(object sender, EventArgs e)
        {
            textBox1.Text = model;
            textBox2.Text = price;
            textBox3.Text = num;
            richTextBox2.Text = remark;
            textBox4.Text = supplier;
            textBox5.Text = type;
            loadpic();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dialogResult = MessageBox.Show("确认报价", "报价", messButton);
            if (dialogResult == DialogResult.OK)
            {
                common cmn = new common();

                using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                {
                    conn.Open();
                    string sql = "update askpricerecord set price = '" + textBox2.Text + "'," +
                                                                    "num = '" + textBox3.Text + "'," +
                                                                    "supplier = '" + textBox4.Text + "'," +
                                                                    "remark = concat(remark, '\r\n 【" + username + "】:\r\n" + richTextBox1.Text + "\r\n')," +
                                                                    "answeruser = '" + username + "'," +
                                                                    "status = '报价'," +
                                                                    "model = '" + textBox1.Text + "'," +
                                                                    "type = '" + textBox5.Text + "'," +
                                                                    "answerstatus = 1 where askpriceID = '" + askpriceID + "'";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        comm.ExecuteReader();
                    }
                }

                //保存图片
                if (pictureBox1.Image != null)
                {
                    byte[] imageBytes = GetImageBytes(pictureBox1.Image);
                    using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
                    {
                        conn.Open();
                        string sql = "insert into askpricepic (askpriceID,pic) values ('" + askpriceID + "',@image)";
                        using (MySqlCommand comm = new MySqlCommand(sql, conn))
                        {
                            MySqlParameter param = new MySqlParameter("@image", MySqlDbType.VarBinary, imageBytes.Length);
                            param.Value = imageBytes;
                            comm.Parameters.Add(param);
                            comm.ExecuteReader();
                        }
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            common cmn = new common();
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                string name = textBox4.Text;
                conn.Open();
                string sql = "select * from newsuppliers where name like '%" + name + "%' limit 10";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comm);
                    da.Fill(ds, "newsuppliers");
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
                    else
                    {
                        this.listBox1.Hide();
                    }
                }
                if (name.Length <= 0)
                    this.listBox1.Hide();
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItems.Count > 0)
            {
                this.textBox4.Text = this.listBox1.SelectedItem.ToString();
                this.listBox1.Hide();
            }
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            this.listBox1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Bitmap))
            {
                pictureBox1.Image = (Bitmap)iData.GetData(DataFormats.Bitmap);
            }
        }

        private byte[] GetImageBytes(Image image)
        {
            MemoryStream mstream = new MemoryStream();
            image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byteData, 0, byteData.Length);
            mstream.Dispose();
            mstream.Close();
            return byteData;
        }

        private void loadpic()
        {
            common cmn = new common();
            using (MySqlConnection conn = new MySqlConnection(cmn.strcon))
            {
                conn.Open();
                string sql = "select ID from askpricepic where askpriceID = '" + askpriceID + "'";
                using (MySqlCommand comm = new MySqlCommand(sql, conn))
                {
                    MySqlDataReader myread = comm.ExecuteReader();
                    while (myread.Read())
                    {
                        string ID = myread["ID"].ToString();
                        listBox1.Items.Add(ID);
                    }
                }
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string ID = this.listBox1.SelectedItem.ToString();
            sales.AskPricePicView appv = new sales.AskPricePicView();
            appv.ID = ID;
            appv.Show();
        }

        private void AnswerPriceEdit_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            richTextBox1.Height = h - 80 - button1.Bottom;
            richTextBox2.Height = h - 80 - button1.Bottom;
            pictureBox1.Height = h - 80 - button1.Bottom;
            pictureBox1.Width = w - 80 - richTextBox2.Right;
        }
    }
}
